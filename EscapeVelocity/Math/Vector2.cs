using System;

namespace TestingXamarin
{
	public class Vector2
	{
		public float x;
		public float y;

		public Vector2 (float x, float y)
		{
			this.x = x;
			this.y = y;
		}

		public float Magnitude()
		{
			return Boner.Length(this);
		}

		public Vector2 getNormalized()
		{
			float m = Magnitude ();
			return new Vector2( this.x / m, this.y / m );
		}
	}
}

