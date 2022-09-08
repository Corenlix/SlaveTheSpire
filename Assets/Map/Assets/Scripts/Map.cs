using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Map
{
    public class Map
    {
        private List<Node> nodes;
        private List<Point> path;
        private string bossNodeName;
        private string configName; // similar to the act name in Slay the Spire

        public List<Point> Path => path.ToList();
        public List<Node> Nodes => nodes.ToList();
        public string ConfigName => configName;

        public Map(string configName, string bossNodeName, List<Node> nodes, List<Point> path)
        {
            this.configName = configName;
            this.bossNodeName = bossNodeName;
            this.nodes = nodes;
            this.path = path;
        }

        private Node GetBossNode()
        {
            return nodes.FirstOrDefault(n => n.nodeType == NodeType.Boss);
        }

        public float DistanceBetweenFirstAndLastLayers()
        {
            var bossNode = GetBossNode();
            var firstLayerNode = nodes.FirstOrDefault(n => n.point.Y == 0);

            if (bossNode == null || firstLayerNode == null)
                return 0f;

            return bossNode.position.Y - firstLayerNode.position.Y;
        }

        public void AppendPath(Point point)
        {
            path.Add(point);
        }

        public Node GetNode(Point point)
        {
            return nodes.FirstOrDefault(n => n.point.Equals(point));
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}