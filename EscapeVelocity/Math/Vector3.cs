using System;

namespace TestingXamarin
{
	public class Vector3
	{
		public float x, y, z;

		public Vector3 (float x, float y, float z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		public float Magnitude()
		{
			return (float)Math.Sqrt( Math.Pow (x, 2) + Math.Pow (y, 2) + Math.Pow (z, 2) );
		}

		public Vector3 getNormalized()
		{
			float m = Magnitude ();

			return new Vector3(x / m, y / m, z / m);
		}
	}
}

