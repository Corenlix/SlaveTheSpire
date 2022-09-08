using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using UnityEngine;
using Utilities;

namespace Map
{
    public class MapGenerator
    {
        private readonly MapConfig _config;

        private readonly List<NodeType> _randomNodes = new() 
            {NodeType.Mystery, NodeType.Store, NodeType.Treasure, NodeType.MinorEnemy, NodeType.RestSite};

        private static List<float> _layerDistances;
        private static List<List<Point>> _paths;
        // ALL nodes by layer:
        private readonly List<List<Node>> _nodes = new();

        public MapGenerator(MapConfig config)
        {
            _config = config;
        }
        
        public Map GetMap()
        {
            _nodes.Clear();

            GenerateLayerDistances();

            for (var i = 0; i < _config.layers.Count; i++)
                PlaceLayer(i);

            GeneratePaths();

            RandomizeNodePositions();

            SetUpConnections();

            RemoveCrossConnections();

            // select all the nodes with connections:
            var nodesList = _nodes.SelectMany(n => n).Where(n => n.incoming.Count > 0 || n.outgoing.Count > 0).ToList();

            // pick a random name of the boss level for this map:
            var bossNodeName = _config.nodeBlueprints.Where(b => b.nodeType == NodeType.Boss).ToList().Random().name;
            return new Map(_config.name, bossNodeName, nodesList, new List<Point>());
        }

        private void GenerateLayerDistances()
        {
            _layerDistances = new List<float>();
            foreach (var layer in _config.layers)
                _layerDistances.Add(layer.distanceFromPreviousLayer.GetValue());
        }

        private static float GetDistanceToLayer(int layerIndex)
        {
            if (layerIndex < 0 || layerIndex > _layerDistances.Count) return 0f;

            return _layerDistances.Take(layerIndex + 1).Sum();
        }

        private void PlaceLayer(int layerIndex)
        {
            var layer = _config.layers[layerIndex];
            var nodesOnThisLayer = new List<Node>();

            // offset of this layer to make all the nodes centered:
            var offset = layer.nodesApartDistance * _config.GridWidth / 2f;

            for (var i = 0; i < _config.GridWidth; i++)
            {
                var nodeType = Random.Range(0f, 1f) < layer.randomizeNodes ? GetRandomNode() : layer.nodeType;
                var blueprintName = _config.nodeBlueprints.Where(b => b.nodeType == nodeType).ToList().Random().name;
                var node = new Node(nodeType, blueprintName, new Point(i, layerIndex))
                {
                    position = new System.Numerics.Vector2(-offset + i * layer.nodesApartDistance, GetDistanceToLayer(layerIndex))
                };
                nodesOnThisLayer.Add(node);
            }

            _nodes.Add(nodesOnThisLayer);
        }

        private void RandomizeNodePositions()
        {
            for (var index = 0; index < _nodes.Count; index++)
            {
                var list = _nodes[index];
                var layer = _config.layers[index];
                var distToNextLayer = index + 1 >= _layerDistances.Count
                    ? 0f
                    : _layerDistances[index + 1];
                var distToPreviousLayer = _layerDistances[index];

                foreach (var node in list)
                {
                    var xRnd = Random.Range(-1f, 1f);
                    var yRnd = Random.Range(-1f, 1f);

                    var x = xRnd * layer.nodesApartDistance / 2f;
                    var y = yRnd < 0 ? distToPreviousLayer * yRnd / 2f : distToNextLayer * yRnd / 2f;

                    node.position += new System.Numerics.Vector2(x, y) * layer.randomizePosition;
                }
            }
        }

        private void SetUpConnections()
        {
            foreach (var path in _paths)
            {
                for (var i = 0; i < path.Count; i++)
                {
                    var node = GetNode(path[i]);

                    if (i > 0)
                    {
                        // previous because the path is flipped
                        var nextNode = GetNode(path[i - 1]);
                        nextNode.AddIncoming(node.point);
                        node.AddOutgoing(nextNode.point);
                    }

                    if (i < path.Count - 1)
                    {
                        var previousNode = GetNode(path[i + 1]);
                        previousNode.AddOutgoing(node.point);
                        node.AddIncoming(previousNode.point);
                    }
                }
            }
        }

        private void RemoveCrossConnections()
        {
            for (var i = 0; i < _config.GridWidth - 1; i++)
                for (var j = 0; j < _config.layers.Count - 1; j++)
                {
                    var node = GetNode(new Point(i, j));
                    if (node == null || node.HasNoConnections()) continue;
                    var right = GetNode(new Point(i + 1, j));
                    if (right == null || right.HasNoConnections()) continue;
                    var top = GetNode(new Point(i, j + 1));
                    if (top == null || top.HasNoConnections()) continue;
                    var topRight = GetNode(new Point(i + 1, j + 1));
                    if (topRight == null || topRight.HasNoConnections()) continue;

                    // Debug.Log("Inspecting node for connections: " + node.point);
                    if (!node.outgoing.Any(element => element.Equals(topRight.point))) continue;
                    if (!right.outgoing.Any(element => element.Equals(top.point))) continue;

                    // Debug.Log("Found a cross node: " + node.point);

                    // we managed to find a cross node:
                    // 1) add direct connections:
                    node.AddOutgoing(top.point);
                    top.AddIncoming(node.point);

                    right.AddOutgoing(topRight.point);
                    topRight.AddIncoming(right.point);

                    var rnd = Random.Range(0f, 1f);
                    if (rnd < 0.2f)
                    {
                        // remove both cross connections:
                        // a) 
                        node.RemoveOutgoing(topRight.point);
                        topRight.RemoveIncoming(node.point);
                        // b) 
                        right.RemoveOutgoing(top.point);
                        top.RemoveIncoming(right.point);
                    }
                    else if (rnd < 0.6f)
                    {
                        // a) 
                        node.RemoveOutgoing(topRight.point);
                        topRight.RemoveIncoming(node.point);
                    }
                    else
                    {
                        // b) 
                        right.RemoveOutgoing(top.point);
                        top.RemoveIncoming(right.point);
                    }
                }
        }

        private Node GetNode(Point p)
        {
            if (p.Y >= _nodes.Count) return null;
            if (p.X >= _nodes[p.Y].Count) return null;

            return _nodes[p.Y][p.X];
        }

        private Point GetFinalNode()
        {
            var y = _config.layers.Count - 1;
            if (_config.GridWidth % 2 == 1)
                return new Point(_config.GridWidth / 2, y);

            return Random.Range(0, 2) == 0
                ? new Point(_config.GridWidth / 2, y)
                : new Point(_config.GridWidth / 2 - 1, y);
        }

        private void GeneratePaths()
        {
            var finalNode = GetFinalNode();
            _paths = new List<List<Point>>();
            var numOfStartingNodes = _config.numOfStartingNodes.GetValue();
            var numOfPreBossNodes = _config.numOfPreBossNodes.GetValue();

            var candidateXs = new List<int>();
            for (var i = 0; i < _config.GridWidth; i++)
                candidateXs.Add(i);

            candidateXs.Shuffle();
            var preBossXs = candidateXs.Take(numOfPreBossNodes);
            var preBossPoints = (from x in preBossXs select new Point(x, finalNode.Y - 1)).ToList();
            var attempts = 0;

            // start by generating paths from each of the preBossPoints to the 1st layer:
            foreach (var point in preBossPoints)
            {
                var path = Path(point, 0, _config.GridWidth);
                path.Insert(0, finalNode);
                _paths.Add(path);
                attempts++;
            }

            while (!PathsLeadToAtLeastNDifferentPoints(_paths, numOfStartingNodes) && attempts < 100)
            {
                var randomPreBossPoint = preBossPoints[UnityEngine.Random.Range(0, preBossPoints.Count)];
                var path = Path(randomPreBossPoint, 0, _config.GridWidth);
                path.Insert(0, finalNode);
                _paths.Add(path);
                attempts++;
            }

            Debug.Log("Attempts to generate paths: " + attempts);
        }

        private static bool PathsLeadToAtLeastNDifferentPoints(IEnumerable<List<Point>> paths, int n)
        {
            return (from path in paths select path[path.Count - 1].X).Distinct().Count() >= n;
        }

        private static List<Point> Path(Point from, int toY, int width, bool firstStepUnconstrained = false)
        {
            if (from.Y == toY)
            {
                Debug.LogError("Points are on same layers, return");
                return null;
            }

            // making one y step in this direction with each move
            var direction = from.Y > toY ? -1 : 1;

            var path = new List<Point> { from };
            while (path[^1].Y != toY)
            {
                var lastPoint = path[^1];
                var candidateXs = new List<int>();
                if (firstStepUnconstrained && lastPoint.Equals(from))
                {
                    for (var i = 0; i < width; i++)
                        candidateXs.Add(i);
                }
                else
                {
                    // forward
                    candidateXs.Add(lastPoint.X);
                    // left
                    if (lastPoint.X - 1 >= 0) candidateXs.Add(lastPoint.X - 1);
                    // right
                    if (lastPoint.X + 1 < width) candidateXs.Add(lastPoint.X + 1);
                }

                var nextPoint = new Point(candidateXs[Random.Range(0, candidateXs.Count)], lastPoint.Y + direction);
                path.Add(nextPoint);
            }

            return path;
        }

        private NodeType GetRandomNode()
        {
            return _randomNodes[Random.Range(0, _randomNodes.Count)];
        }
    }
}