using Leopotam.Ecs;
using alicewithalex.Game.Components;
using alicewithalex.Game.Data;
using UnityEngine;

namespace alicewithalex.Game.Systems
{
    public class PlayerShootingSystem : StateSystem<GameplayState>
    {
        private readonly EcsFilter<Player, Weapon, Stats>.Exclude<ShootDelayTask>
            _player;

        private readonly EcsFilter<ShootDelayTask> _delay;

        private readonly EcsFilter<UnityView, Crosshair>.Exclude<DisabledTag> _crosshair;

        private readonly EcsFilter<UnityView, Projectile, PoolTag> _pool;
        private readonly EcsFilter<UnityView, Projectile, ReturnToPoolTask> _projectiles;

        private readonly ProjectileConfig _projectileConfig;
        private readonly TimeService _timeService;

        protected bool CanShootAutomatic => _projectileConfig.Automatic &&
                Input.GetKey(_projectileConfig.ShootKey);

        protected bool CanShootSingle => !_projectileConfig.Automatic &&
                Input.GetKeyDown(_projectileConfig.ShootKey);


        protected override void OnStateUpdate()
        {
            base.OnStateUpdate();

            HandleShooting();
            HandlePooling();
            HandleDelay();
        }

        private void HandleShooting()
        {
            if (_player.IsEmpty()) return;

            if (CanShootAutomatic || CanShootSingle)
                Shoot();
        }

        private void HandlePooling()
        {
            foreach (var i in _projectiles)
            {
                if (!_projectiles.Get3(i).Tick(_timeService.DeltaTime))
                    continue;

                _projectiles.GetEntity(i).Get<PoolSignal>();
            }
        }

        private void HandleDelay()
        {
            foreach (var i in _delay)
            {
                if (!_delay.Get1(i).Tick(_timeService.DeltaTime))
                    continue;

                _delay.GetEntity(i).Del<ShootDelayTask>();
            }
        }

        private void Shoot()
        {
            foreach (var i in _player)
            {
                Vector3 direction = _player.Get2(i).ShootOrigin.forward;

                foreach (var j in _crosshair)
                {
                    if (!_crosshair.Get2(j).Enemy) continue;

                    direction = (_crosshair.Get1(j).Transform.position -
                        _player.Get2(i).ShootOrigin.position).normalized;
                }

                CreateProjectile(_player.Get2(i).ShootOrigin.position,
                    direction, _projectileConfig.ShootForce,
                    _player.Get3(i).Attack);

                _player.GetEntity(i).Get<ShootDelayTask>() =
                    new ShootDelayTask(_projectileConfig.ShootDelay);
            }
        }


        private void CreateProjectile(Vector3 position,
            Vector3 direction, float force, float damage)
        {
            Rigidbody rigidbody = null;

            if (!_pool.IsEmpty())
            {
                foreach (var i in _pool)
                {
                    _pool.Get1(i).GameObject.SetActive(true);
                    _pool.GetEntity(i).Del<PoolTag>();
                    _pool.GetEntity(i).Get<ReturnToPoolTask>() =
                         new ReturnToPoolTask(_projectileConfig.Lifetime);

                    ref var projectile = ref _pool.Get2(i);
                    projectile.PreviousPosition = position;

                    rigidbody = projectile.Rigidbody;
                    rigidbody.position = position;
                    rigidbody.transform.forward = direction;

                    projectile.Toggle(true);
                    projectile.Damage = damage;

                    break;
                }
            }

            if (!rigidbody)
            {
                var entity = Object.Instantiate(_projectileConfig.Projectile,
                    position, Quaternion.Euler(direction), null).Init(_worldHandler);

                entity.Get<ReturnToPoolTask>() =
                    new ReturnToPoolTask(_projectileConfig.Lifetime);

                ref var projectile = ref entity.Get<Projectile>();
                projectile.PreviousPosition = position;

                rigidbody = projectile.Rigidbody;

                projectile.Toggle(true);
                projectile.Damage = damage;
            }

            rigidbody.AddForce(direction * force, ForceMode.Impulse);
        }
    }
}