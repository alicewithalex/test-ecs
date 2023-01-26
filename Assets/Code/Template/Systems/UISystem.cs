using Leopotam.Ecs;

namespace alicewithalex
{
    public abstract class UISystem<ScreenT> : IEcsRunSystem
        where ScreenT : UIScreen
    {
        protected readonly UIHub _uiHub;
        protected readonly EcsWorld _ecsWorld;

        protected abstract LayerType LayerType { get; }

        private UILayer _layer;
        protected UILayer Layer
        {
            get
            {
                if (_layer == null)
                {
                    _layer = _uiHub.GetLayer(LayerType);
                }

                return _layer;
            }
        }


        private ScreenT _screen;
        protected ScreenT Screen
        {
            get
            {
                if (_screen == null)
                {
                    _screen = Layer.GetScreen<ScreenT>();
                }

                return _screen;
            }
        }

        public abstract void Run();

    }
}