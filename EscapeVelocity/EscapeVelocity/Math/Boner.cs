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

        public static float Length(Point p1, Point p2)
        {
            return (float)Math.Sqrt( Math.Pow( p2.x - p1.x, 2 ) + Math.Pow(p2.y - p1.y, 2) );
        }
	}
}

