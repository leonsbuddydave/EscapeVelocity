using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNAPractice
{
    // May rename this to just "Collider" - I don't think we'll be doing any other kind of colliding
    public class PolygonCollider
    {
        // Which category this collider fits into
        public int CollisionCategory = 0;

        // Which groups this collider will mesh with
        public int CollisionGroups = 0;

        // The Sprite that owns this collider
        private Sprite owner;

        // The polygon that defines this collider
        private Polygon poly;

        // Radius of this collider's broad-phase collision circle
        private float broadPhaseRadius = 0;

        public PolygonCollider(Sprite owner)
        {
            ConstructorHelper(owner, null);
        }

        public PolygonCollider(Sprite owner, Polygon poly)
        {
            ConstructorHelper(owner, poly);
        }

        private void ConstructorHelper(Sprite owner, Polygon poly)
        {
            this.owner = owner;

            this.poly = poly;

            // Generate broad-phase radius
            broadPhaseRadius = ApproximateRadiusFromPolygon();
        }

        // Finds out the maximum distance between points on this polygon
        // to get a radius that contains it entirely
        private float ApproximateRadiusFromPolygon()
        {
            float minX = float.MaxValue,
                  minY = float.MaxValue,
                  maxX = float.MinValue,
                  maxY = float.MinValue;

            float rad = 0;

            if (poly == null)
                return 0;

            int i = 0;
            int t = poly.getPointCount();
            while (i < t)
            {
                Point p = poly[i];

                if (p.x < minX)
                    minX = p.x;

                if (p.x > maxX)
                    maxX = p.x;

                if (p.y < minY)
                    minY = p.y;

                if (p.y > maxY)
                    maxY = p.y;

                i++;
            }

            float distX = maxX - minX;
            float distY = maxY - minY;

            rad = (distX > distY ? distX : distY) / 2;

            return rad;
        }

        // Returns the approximated radius of this object
        public float getBroadPhaseRadius()
        {
            return broadPhaseRadius;
        }

        // Uses a plain radial check to see if this object has a chance
        // of colliding with another one
        public bool SimpleCollidesWith(PolygonCollider him)
        {
            bool collides = false;

            if (Boner.Length(owner.getWorldCenter(), him.owner.getWorldCenter()) < broadPhaseRadius + him.broadPhaseRadius)
                collides = true;

            return collides;
        }

        // Here we will implement the Separating Axis Theorem to
        // determine if these two shapes REALLY collided
        // right now it's using some point-in-polygon bullshit, fuck it
        public bool ComplexCollidesWith(PolygonCollider him)
        {
            PolygonCollider me = this;

            Polygon myPoly = me.poly;
            Polygon hisPoly = him.poly;

            bool inside = false;

            // If either of these polygons are total crap, burn down the banana stand
            if (myPoly.getPointCount() < 3 || hisPoly.getPointCount() < 3)
                return inside;

            int i = 0;
            while (i < myPoly.getPointCount())
            {
                Point p1, p2;
                Point testPoint = new Point(
                            myPoly[i].x + owner.GetRealX(),
                            myPoly[i].y + owner.GetRealY()
                        );

                Point oldPoint = new Point(
                    hisPoly[ hisPoly.getPointCount() - 1 ].x + him.owner.GetRealX(),
                    hisPoly[ hisPoly.getPointCount() - 1].y + him.owner.GetRealY()
                );

                int j = 0;
                while (j < hisPoly.getPointCount())
                {
                    Point newPoint = new Point(
                        hisPoly[j].x + him.owner.GetRealX(),
                        hisPoly[j].y + him.owner.GetRealY()
                    );

                    if (newPoint.x > oldPoint.x)
                    {
                        p1 = oldPoint;
                        p2 = newPoint;
                    }
                    else
                    {
                        p1 = newPoint;
                        p2 = oldPoint;
                    }

                    if (
                        (newPoint.x < testPoint.x) == (testPoint.x <= oldPoint.x) &&
                        (testPoint.y - p1.y) * (p2.x - p1.x) < (p2.y - p1.y) * (testPoint.x - p1.x)
                        )
                    {
                        inside = !inside;
                    }

                    oldPoint = newPoint;

                    // If any one of these points is found to be inside the other guy, we good. 
                    if (inside)
                        return inside;

                    j++;
                }
                i++;
            }

           

            return inside;
        }
    }
}
