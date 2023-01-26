using Leopotam.Ecs;
using UnityEngine;
using alicewithalex.Game.Components;
using alicewithalex.Game.Data;

namespace alicewithalex.Game.Systems
{
    public class EnemyDetectionSystem : StateSystem<GameplayState>
    {
        private readonly EcsFilter<UnityView, Crosshair> _crosshair;
        private readonly DetectionConfig _detectionConfig;

        private Camera _camera;
        private Ray _ray;
        private RaycastHit _hit;
        private bool _hitThisFrame;

        protected override void OnStateEnter()
        {
            base.OnStateEnter();

            _camera = SceneContainer.Instance.Container.Get<Camera>();

            Object.Instantiate(_detectionConfig.Crosshair, null)
                .Init(_worldHandler).Get<CleanupTag>();
        }

        protected override void OnStateUpdate()
        {
            base.OnStateUpdate();

            _ray = _camera.ScreenPointToRay(Input.mousePosition);

            _hitThisFrame = Physics.Raycast(_ray, out _hit, float.MaxValue,
                _detectionConfig.Mask, QueryTriggerInteraction.Collide);

            foreach (var i in _crosshair)
            {
                if (_hitThisFrame)
                {
                    if (!_crosshair.Get1(i).GameObject.activeSelf)
                        _crosshair.Get1(i).GameObject.SetActive(true);

                    _crosshair.Get2(i).SpriteRenderer.color =
                        GetColorByLayerName(LayerMask.LayerToName(
                        _hit.collider.gameObject.layer), out var enemy);

                    _crosshair.Get1(i).Transform.position = _hit.point;
                    _crosshair.Get2(i).Enemy = enemy;
                    _crosshair.GetEntity(i).Del<DisabledTag>();
                }
                else
                {
                    _crosshair.Get1(i).GameObject.SetActive(false);
                    _crosshair.GetEntity(i).Get<DisabledTag>();
                }
            }
        }

        private Color GetColorByLayerName(string layerName, out bool enemy)
        {
            enemy = false;
            if (layerName.Equals("Ground"))
            {
                return _detectionConfig.NormalColor;
            }
            else if (layerName.Equals("Enemy"))
            {
                enemy = true;
                return _detectionConfig.DetectedColor;
            }

            return Color.clear;
        }
    }
}