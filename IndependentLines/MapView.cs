using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace IndependentLineDrawer
{
    public partial class MapView : Form
    {
        private Point _startPoint, _endPoint;
        private List<List<Point>> _lineList = new List<List<Point>>();

        public MapView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Draw lines to a panel.
        /// I used the example below for picking a proper Control and event handlers for drawing the lines:
        /// https://stackoverflow.com/questions/3571413/click-two-new-points-and-draw-a-line-between-those-two-points-using-mouse-event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panelMapView_Paint(object sender, PaintEventArgs e)
        {
            using (var p = new Pen(Color.Blue, 4))
            {
                foreach (List<Point> line in _lineList)
                {
                    for (int x = 0; x < line.Count - 1; x++)
                    {
                        e.Graphics.DrawLine(p, line[x], line[x + 1]);
                    }
                }
            }
        }

        /// <summary>
        /// Handles the user click event
        /// </summary>
        private void panelMapView_MouseDown(object sender, MouseEventArgs e)
        {
            if (_startPoint.X == 0)
            {
                _startPoint.X = e.X;
                _startPoint.Y = e.Y;
            }
            else
            {
                _endPoint.X = e.X;
                _endPoint.Y = e.Y;

                AddNewIndependentLine();

                // make the lines to be drawn
                panelMapView.Invalidate();

                // clear the start point
                _startPoint.X = 0;
            }
        }

        /// <summary>
        /// Creates a pathfinder that gets a line to be drawn
        /// </summary>
        private void AddNewIndependentLine()
        {
            bool[,] map =
                SearchParameters.InitializeMap(panelMapView.Size.Width, panelMapView.Size.Height, _lineList);
            var searchParameters = new SearchParameters(_startPoint, _endPoint, map);
            var pathFinder = new PathFinder(searchParameters);

            List<Point> path = pathFinder.FindPath();
            _lineList.Add(path);
        }

    }
}
