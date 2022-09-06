using Entities;
using FluentAssertions;
using Infrastructure.Assets;
using Infrastructure.Factories;
using Infrastructure.GameState;
using Infrastructure.Progress;
using Infrastructure.SaveLoad;
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
            Container.Bind<IPrefabFactory>().To<PrefabFactory>().AsSingle();
            Container.Bind<IVisualEffectFactory>().To<VisualEffectFactory>().AsSingle();
            Container.Bind<IEnemiesHolder>().To<EnemiesHolder>().AsSingle();
            Container.Bind<IPlayersHolder>().To<PlayersHolder>().AsSingle();
            Container.Bind<ITurnResolver>().To<TurnResolver>().AsSingle();
            Container.Bind<ISaveLoadService>().To<SaveLoadService>().AsSingle();
            Container.Bind<IProgressService>().To<ProgressService>().AsSingle();
            Container.Bind<IGameFactory>().To<GameFactory>().AsSingle();
            Container.Bind<FinderUnderCursor>().AsSingle();
            Container.Bind<UIHolder>().AsSingle();
            Container.Bind<LocationHolder>().AsSingle();
            Container.Bind<GameStateMachine>().AsSingle().NonLazy();
        }
    }
}