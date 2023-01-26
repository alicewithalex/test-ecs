using Leopotam.Ecs;
using UnityEngine;
using alicewithalex.Extensions.Data;
using alicewithalex.Extensions.Components;
using alicewithalex.FancyDebug;

namespace alicewithalex.Extensions.Views
{
    [RequireComponent(typeof(Rigidbody))]
    public class TriggerEnterListener : ViewComponent
    {
        [SerializeField] private string _interactionTag;

        private bool _anyInteraction;
        protected EcsEntity Self { get; private set; }

        public override void Init(IEcsWorldHandler ecsWorldHandler, EcsEntity entity)
        {
            Self = entity;
            _anyInteraction = string.IsNullOrEmpty(_interactionTag);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!IsInteractable(other) || !other.gameObject.TryGetEntity(
                out var entity)) return;

            OnTriggerEnterEntity(other, entity);
        }

        protected virtual void OnTriggerEnterEntity(Collider otherCollider,
            EcsEntity otherEntity)
        {
            Self.Get<TriggerEnter>() = new TriggerEnter()
            {
                OtherEntity = otherEntity,
                OtherCollider = otherCollider
            };
        }

        private bool IsInteractable(Collider other)
        {
            return _anyInteraction || other.CompareTag(_interactionTag);
        }

        private void Reset()
        {
            if (!TryGetComponent<Collider>(out var collider))
            {
                FDebug.Log($"#Warning:##Object don't have any collider. " +
                    $"You must assign it manually to make listener work.#",
                    FColor.Red, FColor.Yellow);
            }
            else
            {
                collider.isTrigger = true;
            }

            GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}