using System;
using Microsoft.Xna.Framework;

namespace XNAPractice
{
	public class Boner
	{
		public static float Length( Vector2 v )
		{
			return (float)Math.Sqrt( Math.Pow (v.X, 2) + Math.Pow (v.Y, 2) );
		}

        public static float Length(Vector2 p1, Vector2 p2)
        {
            return (float)Math.Sqrt( Math.Pow( p2.X - p1.X, 2 ) + Math.Pow(p2.Y - p1.Y, 2) );
        }

		public static Vector2 GetPerpendicularVector(Vector2 v)
		{
			return new Vector2(-v.Y, v.X);
		}

		public static float Dot(Vector2 v1, Vector2 v2)
		{
			return (v1.X * v2.X + v1.Y * v2.Y);
		}

		public static bool VectorOverlap(Vector2 v1, Vector2 v2)
		{
			if (v1.X < v2.X)
			{
				// The vectors are already in the right order to test
			}
			else
			{
				Vector2 temp = v2;
				v2 = v1;
				v1 = temp;
			}

			return v1.Y > v2.X;
		}
	}
}

