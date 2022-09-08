using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    [CreateAssetMenu]
    public class MapConfig : ScriptableObject
    {
        public List<NodeBlueprint> nodeBlueprints;
        public int GridWidth => Mathf.Max(numOfPreBossNodes.max, numOfStartingNodes.max);

      
        public IntMinMax numOfPreBossNodes;
        
        public IntMinMax numOfStartingNodes;

        public List<MapLayer> layers;
    }
}