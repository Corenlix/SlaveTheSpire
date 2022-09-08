using Infrastructure.Factories;
using Infrastructure.Progress;
using Infrastructure.SaveLoad;
using Map;
using UnityEngine.SceneManagement;

namespace Infrastructure.GameState
{
    public class SelectLevelState : IState
    {
        private readonly IProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;
        private readonly IGameFactory _gameFactory;

        public SelectLevelState(IGameFactory gameFactory, IProgressService progressService, ISaveLoadService saveLoadService)
        {
            _gameFactory = gameFactory;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }
        
        public void Enter()
        {
            MapManager map = _gameFactory.SpawnMap();
            map.Load(_saveLoadService);
            _progressService.AddClient(map);
            map.EnteredNode += OnEnteredNode;
        }
        
        private void OnEnteredNode(NodeType nodeType)
        {
            switch (nodeType)
            {
                case NodeType.MinorEnemy:
                    LoadEnemyNode();
                    break;
            }
        }

        private void LoadEnemyNode()
        {
            _saveLoadService.SetValue(GameStage.Battle, SaveLoadKey.GameStage);
            _progressService.Save();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void Exit()
        {
            
        }
    }
}