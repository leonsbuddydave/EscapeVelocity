using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace XNAPractice
{
    // May rename this to just "Collider" - I don't think we'll be doing any other kind of colliding
    public class Collider
    {
        // Which category this collider fits into
        public int CollisionCategory = 0;

        // Which groups this collider will mesh with
        public int CollisionGroups = 0;

        // The Sprite that owns this collider
        private Sprite owner;

        // The polygon that defines this collider
        private Polygon poly;

        public Collider(Sprite owner)
        {
            ConstructorHelper(owner, null);
        }

        public Collider(Sprite owner, Polygon poly)
        {
            ConstructorHelper(owner, poly);
        }

		public Collider(Sprite owner, Vector2[] points)
		{
			Polygon poly = new Polygon(points);
			ConstructorHelper(owner, poly);
		}

        private void ConstructorHelper(Sprite owner, Polygon poly)
        {
            this.owner = owner;
            this.poly = poly;
        }

		public Polygon CalculateWorldPolygon()
		{
			Vector2[] worldPoints = new Vector2[ poly.getPointCount() ];
			Vector2 ownerPosition = owner.GetRealPosition();

			int c = poly.getPointCount();
			for (int i = 0; i < c; i++)
			{
				worldPoints[i] = poly[i] + ownerPosition;
			}

			return new Polygon(worldPoints);
		}

		public Polygon GetPolygon()
		{
			return poly;
		}

		public Sprite GetOwner()
		{
			return owner;
		}

        // Uses a plain radial check to see if this object has a chance
        // of colliding with another one
		// (broad phase)
        public bool WithinRangeOf(Collider him)
        {
            bool collides = false;

			Vector2 deltaPosition = owner.getWorldCenter() - him.owner.getWorldCenter();
			float radii = poly.GetRadius() + him.GetPolygon().GetRadius();

			float leftSide = (deltaPosition.X * deltaPosition.X) + (deltaPosition.Y * deltaPosition.Y);

			float rightSide = radii * radii;

            if (leftSide < rightSide)
                collides = true;

            return collides;
        }

        // Here we will implement the Separating Axis Theorem to
        // determine if these two shapes REALLY collided
        public static CollisionResult Collision(Collider one, Collider two)
        {
			
            Polygon pOne = one.CalculateWorldPolygon();
            Polygon pTwo = two.CalculateWorldPolygon();

			CollisionResult result = new CollisionResult(false, false);

            // If either of these polygons are total crap, burn down the banana stand
			if (pOne.getPointCount() < 3 || pTwo.getPointCount() < 3)
				return result;

			Vector2[] axes1 = pOne.GetProjectionAxes(one.owner);
			Vector2[] axes2 = pTwo.GetProjectionAxes(two.owner);

			for (int i = 0; i < axes1.Length; i++)
			{
				// Get us a goddamn axis
				Vector2 axis = axes1[i];

				// Project both shapes onto this motherfucker
				Vector2 p1 = pOne.ProjectOntoAxis(axis, one.owner);
				Vector2 p2 = pTwo.ProjectOntoAxis(axis, two.owner);

				if (!Boner.VectorOverlap(p1, p2))
					return result;
			}
			// Iterate over the second set of axes
			for (int i = 0; i < axes2.Length; i++)
			{
				Vector2 axis = axes2[i];

				// Project both shapes onto the axis
				Vector2 p1 = pOne.ProjectOntoAxis(axis, one.owner);
				Vector2 p2 = pTwo.ProjectOntoAxis(axis, two.owner);

				if (!Boner.VectorOverlap(p1, p2))
					return result;
			}

			// If we reach this point, we have full-on intersection between polygons
			// fuck yeah

			// right here we'll check to see if either one even cares

			if ((one.CollisionGroups & two.CollisionCategory) == two.CollisionCategory)
				result.one = true;
			if ((two.CollisionGroups & one.CollisionCategory) == one.CollisionCategory)
				result.two = true;

			return result;
        }
    }
}
