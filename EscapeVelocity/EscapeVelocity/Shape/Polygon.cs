using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNAPractice
{
    public class Polygon
    {
        public Point[] points;

        public Polygon(Point[] points)
        {
            this.points = points;
        }

        public int getPointCount()
        {
            return points.Length;
        }

        public Point this[int i]
        {
            get
            {
                return points[i];
            }
            set
            {
                points[i] = value;
            }
        }
    }
}
