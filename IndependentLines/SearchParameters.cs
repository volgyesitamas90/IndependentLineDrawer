using System.Collections.Generic;
using System.Drawing;

namespace IndependentLineDrawer
{
    /// <summary>
    /// Defines the parameters which will be used to find a path across a section of the map
    /// </summary>
    public class SearchParameters
    {

        public Point StartLocation { get; set; }

        public Point EndLocation { get; set; }
        
        public bool[,] Map { get; set; }

        public SearchParameters(Point startLocation, Point endLocation, bool[,] map)
        {
            StartLocation = startLocation;
            EndLocation = endLocation;
            Map = map;
        }

        /// <summary>
        /// Initializes the search parameters
        /// </summary>
        /// <returns></returns>
        public static bool[,] InitializeMap(int width, int height, List<List<Point>> lineList)
        {
            bool[,] map = CreateEmptyMap(width, height);

            map = AddLinesToMap(lineList, map);

          return map;
        }

        /// <summary>
        /// Creates an empty map
        /// </summary>
        private static bool[,] CreateEmptyMap(int width, int height)
        {
            var map = new bool[width, height];
            for (int y = 0; y < height - 1; y++)
            for (int x = 0; x < width - 1; x++)
                map[x, y] = true;
            return map;
        }

        /// <summary>
        /// adds lines to the map
        /// </summary>
        private static bool[,] AddLinesToMap(List<List<Point>> lineList, bool[,] map)
        {
            foreach (List<Point> line in lineList)
            {
                foreach (Point point in line)
                {
                    map[point.X, point.Y] = false;
                }
            }
            return map;
        }
    }
}
