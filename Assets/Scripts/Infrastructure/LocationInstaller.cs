using Infrastructure.GameState;
using Zenject;

namespace Infrastructure
{
    public class LocationInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();
            Container.Bind<IStaticDataService>().To<StaticDataService>().AsSingle();
            Container.Bind<ICardTargetSelectorFactory>().To<CardTargetSelectorFactory>().AsSingle();
            Container.Bind<IGameFactory>().To<GameFactory>().AsSingle();
            Container.Bind<GameContainer>().AsSingle();
            Container.Bind<GameStateMachine>().AsSingle().NonLazy();
        }
    }
}