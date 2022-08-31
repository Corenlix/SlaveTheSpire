using Infrastructure.Assets;
using Infrastructure.Factories;
using Infrastructure.GameState;
using Infrastructure.StaticData;
using Utilities;
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
            Container.Bind<IEnemyActionsFactory>().To<EnemyActionsFactory>().AsSingle();
            Container.Bind<IGameFactory>().To<GameFactory>().AsSingle();
            Container.Bind<IEnemiesHolder>().To<EnemiesHolder>().AsSingle();
            Container.Bind<IPlayerHolder>().To<PlayerHolder>().AsSingle();
            Container.Bind<FinderUnderCursor>().AsSingle();
            Container.Bind<LocationInstaller>().AsSingle();
            Container.Bind<UIHolder>().AsSingle();
            Container.Bind<LocationHolder>().AsSingle();
            Container.Bind<IDeckHolder>().To<DeckHolder>().AsSingle();
            Container.Bind<GameStateMachine>().AsSingle().NonLazy();
        }
    }
}