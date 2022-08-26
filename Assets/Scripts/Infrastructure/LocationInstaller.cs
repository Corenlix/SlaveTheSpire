using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace Infrastructure
{
    public class LocationInstaller : MonoInstaller
    {
        [SerializeField] private EventSystem _eventSystem;
        [SerializeField] private GraphicRaycaster _graphicRaycaster;
        [SerializeField] private Canvas _canvas;
        
        public override void InstallBindings()
        {
            Container.Bind<EventSystem>().FromInstance(_eventSystem);
            Container.Bind<GraphicRaycaster>().FromInstance(_graphicRaycaster);
            Container.Bind<Canvas>().FromInstance(_canvas);
            Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();
            Container.Bind<IStaticDataService>().To<StaticDataService>().AsSingle();
            Container.Bind<IGameFactory>().To<GameFactory>().AsSingle();
            Container.Bind<Game>().AsSingle().NonLazy();
        }
    }
}