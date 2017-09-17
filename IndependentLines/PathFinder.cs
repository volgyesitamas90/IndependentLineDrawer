using System.Collections.Generic;
using System.Drawing;

namespace IndependentLineDrawer
{
    /// <summary>
    ///  I analyzed the following solution to understand the implementation of the A* algorithm
    /// 1. the heuristic distance calculation,
    /// 2. the stateful node for tracking the path,
    /// https://www.codeproject.com/Articles/15307/A-algorithm-implementation-in-C
    /// </summary>
    public class PathFinder
    {
        private int _width;
        private int _height;
        private Node[,] _nodes;
        private readonly Node _startNode;
        private readonly Node _endNode;
        private readonly SearchParameters _searchParameters;

        /// <summary>
        /// Create a new instance of PathFinder
        /// </summary>
        /// <param name="searchParameters"></param>
        public PathFinder(SearchParameters searchParameters)
        {
            _searchParameters = searchParameters;
            InitializeNodes(searchParameters.Map);
            _startNode = _nodes[searchParameters.StartLocation.X, searchParameters.StartLocation.Y];
            _startNode.State = NodeState.Open;
            _endNode = _nodes[searchParameters.EndLocation.X, searchParameters.EndLocation.Y];
        }

        /// <summary>
        /// Attempts to find a path from the start location to the end location based on the supplied SearchParameters
        /// </summary>
        /// <returns>A List of Points representing the path. If no path was found, the returned list is empty.</returns>
        public List<Point> FindPath()
        {
            // The start node is the first entry in the 'open' list
            var path = new List<Point>();
            bool success = Search(_startNode);
            // If a path was found, follow the parents from the end node to build a list of locations
            if (success)
            {
                BuildPath(path);
            }

            return path;
        }
        /// <summary>
        /// follow the parents from the end node to build a list of locations
        /// </summary>
        /// <param name="path"></param>
        private void BuildPath(List<Point> path)
        {
            var node = _endNode;
            while (node.ParentNode != null)
            {
                path.Add(node.Location);
                node = node.ParentNode;
            }

            // Reverse the list so it's in the correct order when returned
            path.Reverse();
        }

        /// <summary>
        /// Builds the node grid from a simple grid of booleans indicating areas which are and aren't walkable
        /// </summary>
        /// <param name="map">A boolean representation of a grid in which true = walkable and false = not walkable</param>
        private void InitializeNodes(bool[,] map)
        {
            _width = map.GetLength(0);
            _height = map.GetLength(1);
            _nodes = new Node[_width, _height];
            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    _nodes[x, y] = new Node(x, y, map[x, y], _searchParameters.EndLocation);
                }
            }
        }

        /// <summary>
        /// Attempts to find a path to the destination node using <paramref name="currentNode"/> as the starting location
        /// </summary>
        /// <param name="currentNode">The node from which to find a path</param>
        /// <returns>True if a path to the destination has been found, otherwise false</returns>
        private bool Search(Node currentNode)
        {
            // Set the current node to Closed since it cannot be traversed more than once
            currentNode.State = NodeState.Closed;
            List<Node> nextNodes = GetAdjacentWalkableNodes(currentNode);

            // Sort by F-value so that the shortest possible routes are considered first
            nextNodes.Sort((node1, node2) => node1.F.CompareTo(node2.F));
            foreach (var nextNode in nextNodes)
            {
                // Check whether the end node has been reached
                if (nextNode.Location == _endNode.Location)
                {
                    return true;
                }
                // If not, check the next set of nodes
                if (Search(nextNode)) // Note: Recurses back into Search(Node)
                    return true;
            }

            // The method returns false if this path leads to be a dead end
            return false;
        }

        /// <summary>
        /// Returns any nodes that are adjacent to <paramref name="fromNode"/> and may be considered to form the next step in the path
        /// </summary>
        /// <param name="fromNode">The node from which to return the next possible nodes in the path</param>
        /// <returns>A list of next possible nodes in the path</returns>
        private List<Node> GetAdjacentWalkableNodes(Node fromNode)
        {
            var walkableNodes = new List<Node>();
            IEnumerable<Point> nextLocations = GetAdjacentLocations(fromNode.Location);

            foreach (var location in nextLocations)
            {
                int x = location.X;
                int y = location.Y;

                // Stay within the grid's boundaries
                if (x < 0 || x >= _width -1 || y < 0 || y >= _height -1)
                    continue;

                Node node = _nodes[x, y];
                // Ignore non-walkable nodes
                if (!node.IsWalkable)
                    continue;

                // Ignore already-closed nodes
                switch (node.State)
                {
                    case NodeState.Closed:
                        continue;
                    case NodeState.Open:
                        float traversalCost = Node.GetTraversalCost(node.Location, node.ParentNode.Location);
                        float gTemp = fromNode.G + traversalCost;
                        if (gTemp < node.G)
                        {
                            node.ParentNode = fromNode;
                            walkableNodes.Add(node);
                        }
                        break;
                    default:
                        // If it's untested, set the parent and flag it as 'Open' for consideration
                        node.ParentNode = fromNode;
                        node.State = NodeState.Open;
                        walkableNodes.Add(node);
                        break;
                }

                // Already-open nodes are only added to the list if their G-value is lower going via this route.
            }

            return walkableNodes;
        }

        /// <summary>
        /// Returns the eight locations immediately adjacent (orthogonally and diagonally) to <paramref name="fromLocation"/>
        /// </summary>
        /// <param name="fromLocation">The location from which to return all adjacent points</param>
        /// <returns>The locations as an IEnumerable of Points</returns>
        private static IEnumerable<Point> GetAdjacentLocations(Point fromLocation)
        {
            return new Point[]
            {
                new Point(fromLocation.X-1, fromLocation.Y  ),
                new Point(fromLocation.X,   fromLocation.Y+1),
                new Point(fromLocation.X+1, fromLocation.Y  ),
                new Point(fromLocation.X,   fromLocation.Y-1)
            };
        }
    }
}
