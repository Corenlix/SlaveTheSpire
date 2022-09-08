using System;
using System.Linq;
using Infrastructure.Progress;
using Infrastructure.SaveLoad;
using UnityEngine;
using Newtonsoft.Json;

namespace Map
{
    public class MapManager : MonoBehaviour, IProgressClient
    {
        public event Action<NodeType> EnteredNode;
        
        public MapConfig config;
        public MapView view;
        [SerializeField] private MapPlayerTracker _mapPlayerTracker;

        public Map CurrentMap { get; private set; }

        private void OnEnable()
        {
            _mapPlayerTracker.EnteredNode += OnEnteredNode;
        }

        private void OnEnteredNode(NodeType obj)
        {
           EnteredNode?.Invoke(obj);
        }
        
        private void GenerateNewMap()
        {
            var mapGenerator = new MapGenerator(config);
            Map map = mapGenerator.GetMap();
            CurrentMap = map;
            view.ShowMap(map);
        }

        private void OnDisable()
        {
            _mapPlayerTracker.EnteredNode -= OnEnteredNode;
        }

        public void Save(ISaveLoadService saveLoadService)
        {
            saveLoadService.SetValue(CurrentMap, SaveLoadKey.Map);
        }

        public void Load(ISaveLoadService saveLoadService)
        {
            CurrentMap = saveLoadService.GetValue(SaveLoadKey.Map, new MapGenerator(config).GetMap());
            view.ShowMap(CurrentMap);
        }
    }
}
