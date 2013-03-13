using System;

namespace TestingXamarin
{
	public class TriangleEntity : Entity, IDrawable
	{
		private float angle = 0.0f;
		public TriangleEntity ()
		{
			z = 100;
			registerEntityModifier (new MoveModifier (-10, -10, 30, 5)
			{

			});
		}

		public void Draw()
		{

		}

		public override void Update(float dt)
		{

		}
	}
}

