using Leopotam.Ecs;
using UnityEngine;
using alicewithalex.Game.Data;
using alicewithalex.Game.Components;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using Object = UnityEngine.Object;

namespace alicewithalex.Game.Systems
{
    public class EnemySpawnerSystem : StateSystem<GameplayState>
    {
        const int CAM_RAYS_AMOUNT = 32;

        #region Auto-Injected Data

        private readonly EcsFilter<UnityView, Level> _level;

        private readonly EcsFilter<Enemy>.Exclude<PoolTag> _enemies;
        private readonly EcsFilter<UnityView, Enemy, PoolTag> _enemiesPool;

        private readonly TimeService _timeService;
        private readonly EnemySpawnerConfig _enemySpawnerConfig;
        private readonly EnemiesFactory _enemiesFactory;

        #endregion

        #region Runtime Data

        private Camera _camera;

        private Plane _plane;
        private Ray _ray;
        private float _distance;

        private BoxCollider _bounds;

        private float _spawnTimer;
        private List<Vector3> _spawnPositionsTmp = new List<Vector3>();



        #endregion

        public EnemySpawnerSystem()
        {
            _enemiesFactory = SceneContainer.Instance.Container
                .Get<EnemiesFactory>();
        }

        protected override void OnStateEnter()
        {
            base.OnStateEnter();

            _camera = SceneContainer.Instance.Container.Get<Camera>();

            foreach (var i in _level)
            {
                _bounds = _level.Get2(i).Bounds;
                _plane = new Plane(Vector3.up, _level.Get1(i)
                    .Transform.position);
            }

            _spawnTimer = 0f;
        }

        protected override void OnStateUpdate()
        {
            base.OnStateUpdate();

            if (_enemies.GetEntitiesCount() >=
                _enemySpawnerConfig.MaxEnemyAmount ||
                (_spawnTimer -= _timeService.DeltaTime) > 0)
                return;

            _spawnTimer = _enemySpawnerConfig.SpawnDelay;
            var entity = CreateEnemy(_enemiesFactory.CreateRandom(
                out var type), type);
            var agent = entity.Get<Enemy>().Agent;

            agent.Warp(GetSpawnPosition(agent.radius));
            agent.transform.localEulerAngles = Vector3.up *
                Random.Range(0f, 360f);
        }

        private EcsEntity CreateEnemy(ViewElement view, EnemyType type)
        {
            EcsEntity entity = EcsEntity.Null;

            if (_enemiesPool.GetEntitiesCount() > 0)
            {
                foreach (var i in _enemiesPool)
                {
                    if (_enemiesPool.Get2(i).EnemyType == type)
                    {
                        _enemiesPool.Get1(i).GameObject.SetActive(true);
                        _enemiesPool.Get2(i).Collider.enabled = false;

                        entity = _enemiesPool.GetEntity(i);
                        entity.Del<PoolTag>();

                        break;
                    }
                }
            }

            if (!entity.IsAlive())
            {
                entity = Object.Instantiate(view).Init(_worldHandler);
                entity.Get<AssignStatsSignal>();
            }

            entity.Get<ActivateColliderSignal>();

            return entity;
        }

        private Vector3 GetSpawnPosition(float agentRadius)
        {
            if (!GetPointOnPlane(new Vector3(0.5f, 0.5f), out var center))
                return center;

            GetPointOnPlane(new Vector3(0f, 0f), out var min);
            GetPointOnPlane(new Vector3(1f, 1f), out var max);

            float radius = 0f;
            Vector3 direction = Vector3.zero;
            if ((max - center).sqrMagnitude > (min - center).sqrMagnitude)
            {
                radius = (max - center).magnitude +
                    2 * (agentRadius + _enemySpawnerConfig.ExtraSpawnRadius);
                direction = (max - center).normalized;
            }
            else
            {
                radius = (min - center).magnitude +
                    2 * (agentRadius + _enemySpawnerConfig.ExtraSpawnRadius);
                direction = (min - center).normalized;
            }

            float deltaAngle = 360f / CAM_RAYS_AMOUNT;
            Vector3 point = Vector3.zero;

            for (int i = 0; i < CAM_RAYS_AMOUNT; i++)
            {
                point = center + radius * (Quaternion.Euler(
                    Vector3.up * deltaAngle * i) * Vector3.forward);

                if (!InBounds(point)) continue;

                _spawnPositionsTmp.Add(point);
            }


            point = _spawnPositionsTmp[Random.Range(0,
                _spawnPositionsTmp.Count)];

            direction = (point - center).normalized;
            point -= direction * (agentRadius + _enemySpawnerConfig.ExtraSpawnRadius);

            //Debug.DrawLine(center, point, Color.red, 3f);

            Vector2 distance = GetDistanceToBounds(point);
            radius = Mathf.Min(distance.x, distance.y, agentRadius + _enemySpawnerConfig.ExtraSpawnRadius);

            Vector2 offset = Random.insideUnitCircle * radius;
            point.x += offset.x;
            point.z += offset.y;

            _spawnPositionsTmp.Clear();

            return point;
        }

        private bool InBounds(Vector3 pos)
        {
            pos = _bounds.transform.InverseTransformPoint(pos);

            float halfSize = _bounds.size.x * 0.5f;

            if (Mathf.Abs(pos.x) > halfSize)
            {
                return false;
            }

            halfSize = _bounds.size.z * 0.5f;

            if (Mathf.Abs(pos.z) > halfSize)
            {
                return false;
            }

            return true;
        }

        private Vector2 GetDistanceToBounds(Vector3 pos)
        {
            pos = _bounds.transform.InverseTransformPoint(pos);

            pos.x = _bounds.size.x * 0.5f - Mathf.Abs(pos.x);
            pos.z = _bounds.size.z * 0.5f - Mathf.Abs(pos.z);

            if (pos.x < 0)
                pos.x = 0f;

            if (pos.z < 0)
                pos.z = 0f;

            pos = _bounds.transform.TransformVector(pos);

            return new Vector2(pos.x, pos.z);
        }

        private bool GetPointOnPlane(Vector3 pos, out Vector3 outPos)
        {
            outPos = Vector3.zero;

            _ray = _camera.ViewportPointToRay(pos);
            if (!_plane.Raycast(_ray, out _distance)) return false;

            outPos = _ray.GetPoint(_distance);

            return true;
        }
    }
}