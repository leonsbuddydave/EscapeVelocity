using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace XNAPractice
{
    public class Polygon
    {
		// Local points
        private Vector2[] points;

		private float approximateRadius = 0;

        public Polygon(Vector2[] points)
        {
            this.points = points;

			ApproximateRadius();
        }

        public int getPointCount()
        {
            return points.Length;
        }

		public float GetRadius()
		{
			return approximateRadius;
		}

		public Vector2[] GetPoints()
		{
			return points;
		}

		// Finds out the maximum distance between points on this polygon
		// to get a radius that contains it entirely
		private void ApproximateRadius()
		{
			float minX = float.MaxValue,
				  minY = float.MaxValue,
				  maxX = float.MinValue,
				  maxY = float.MinValue;

			int i = 0;
			int t = getPointCount();
			while (i < t)
			{
				Vector2 p = this[i];

				if (p.X < minY)
					minX = p.X;

				if (p.X > maxX)
					maxX = p.X;

				if (p.Y < minY)
					minY = p.Y;

				if (p.Y > maxY)
					maxY = p.Y;

				i++;
			}

			float distX = maxX - minX;
			float distY = maxY - minY;

			approximateRadius = (distX > distY ? distX : distY) / 2;
		}

		// Retrieve the projection axes for this polygon
		public Vector2[] GetProjectionAxes(Entity owner)
		{
			Vector2[] axes = new Vector2[points.Length];

			Vector2 ownerPosition = owner.GetRealPosition();

			for (int i = 0; i < points.Length; i++)
			{
				// Current vertex
				Vector2 p1 = points[i];

				// Next vertex
				Vector2 p2 = points[ i + 1 == points.Length ? 0 : i + 1 ];

				// Subtract the two to get the edge vector
				Vector2 edge = p1 - p2;

				// Get the perpendicular vector (doesn't matter which one)
				Vector2 normal = Boner.GetPerpendicularVector(edge);

				axes[i] = normal;
			}

			return axes;
		}

		// Project this shape onto an axis
		public Vector2 ProjectOntoAxis(Vector2 axis, Entity owner)
		{
			Vector2 ownerPosition = owner.GetRealPosition();

			float min = Boner.Dot(axis, points[0]);
			float max = min;

			for (int i = 1; i < points.Length; i++)
			{
				// We'd need to normalize this axis to get ACCURATE projections
				// we don't need that right now, though
				float p = Boner.Dot(axis, points[i]);

				if (p < min)
					min = p;
				else if (p > max)
					max = p;
			}

			return new Vector2(min, max);
		}

		// Indexer for points
        public Vector2 this[int i]
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
