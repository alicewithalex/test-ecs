using Leopotam.Ecs;

namespace alicewithalex
{
    public abstract class UIStateSystem<StateT, ScreenT> : StateSystem<StateT>
        where StateT : struct where ScreenT : UIScreen
    {
        protected readonly UIHub _uiHub;

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
    }

    public class DefaultUIScreen : UIScreen { }

}