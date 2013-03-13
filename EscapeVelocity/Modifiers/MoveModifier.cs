using System;

namespace TestingXamarin
{
	public class MoveModifier : EntityModifier
	{
		private float targetX, targetY, targetZ, speed;

		private bool zEnabled = false;

		// 2D Constructor
		public MoveModifier (float targetX, float targetY, float speed)
		{
			ConstructorHelper (targetX, targetY, 0, speed);
		}

		// 3D Constructor
		public MoveModifier(float targetX, float targetY, float targetZ, float speed)
		{
			zEnabled = true;
			ConstructorHelper (targetX, targetY, targetZ, speed);
		}

		public void ConstructorHelper(float targetX, float targetY, float targetZ, float speed)
		{
			this.targetX = targetX;
			this.targetY = targetY;
			this.targetZ = targetZ;
			this.speed = speed;
		}

		public override void onUpdate(float dt)
		{
			if (target == null) // might be worth making that an exception
				return;

			float currentX = target.getX ();
			float currentY = target.getY ();
			float currentZ = target.getZ ();

			float deltaX = targetX - currentX;
			float deltaY = targetY - currentY;
			float deltaZ = targetZ - currentZ;

			if (zEnabled) {
				Vector3 scaleVector = new Vector3 (deltaX, deltaY, deltaZ).getNormalized ();

				if (deltaX < 0)
					target.setX (targetX);
				else
					target.setX (currentX + scaleVector.x * speed * dt);

				if (deltaY < 0)
					target.setY (targetY);
				else
					target.setY (currentY + scaleVector.y * speed * dt);

				if (deltaZ < 0)
					target.setZ (targetZ);
				else
					target.setZ (currentZ + scaleVector.z * speed * dt);


				if (deltaX <= 0 && deltaY <= 0 && deltaZ <= 0) {
					target.removeEntityModifier (this);
					return;
				}
			}
			else
			{
				if (deltaX < 0 && deltaY < 0)
				{
					target.setX (targetX);
					target.setY (targetY);
					target.removeEntityModifier (this);
					return;
				}

				Vector2 scaleVector = new Vector2 (deltaX, deltaY).getNormalized ();
				target.setX ( currentX + scaleVector.x * speed * dt);
				target.setY ( currentY + scaleVector.y * speed * dt);
			}
		}
	}
}

