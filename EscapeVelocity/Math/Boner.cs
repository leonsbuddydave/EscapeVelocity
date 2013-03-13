using System;

namespace TestingXamarin
{
	public class Boner
	{
		public static float Length( Vector2 v )
		{
			return (float)Math.Sqrt( Math.Pow (v.x, 2) + Math.Pow (v.y, 2) );
		}
	}
}

