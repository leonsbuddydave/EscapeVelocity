using System;
using Microsoft.Xna.Framework;

namespace XNAPractice
{
	public class MoveModifier : EntityModifier
	{
		protected float targetX, targetY, speed;

        protected const float TARGET_TOLERANCE = .5f;

		public MoveModifier (float targetX, float targetY, float speed)
		{
			ConstructorHelper (targetX, targetY, speed);
		}

		public void ConstructorHelper(float targetX, float targetY, float speed)
		{
			this.targetX = targetX;
			this.targetY = targetY;
			this.speed = speed;
		}

        public override void ModifierCompleted()
        {
            // Remove this modifier, as it is done
            target.RemoveEntityModifier(this);
        }

		public override void onUpdate(float dt)
		{
			if (target == null) // might be worth making that an exception
				return;

			float currentX = target.GetX ();
			float currentY = target.GetY ();

			float remainingDeltaX = targetX - currentX;
			float remainingDeltaY = targetY - currentY;

            // Get the total distance remaining from current position to target position,
            // and normalize it to get a scaling factor
            Vector2 scaleVector = new Vector2(remainingDeltaX, remainingDeltaY);
            scaleVector.Normalize();

            // Calculate how much we're supposed to move this step
            float stepDeltaX = scaleVector.X * speed * dt;
            float stepDeltaY = scaleVector.Y * speed * dt;

            // Test to see if we're really super close to the target position
            if (Math.Abs(remainingDeltaX) < Math.Abs(stepDeltaX))
                stepDeltaX = remainingDeltaX;
            if (Math.Abs(remainingDeltaY) < Math.Abs(stepDeltaY))
                stepDeltaY = remainingDeltaY;

            // Jump to new position
            target.SetX(currentX + stepDeltaX);
            target.SetY(currentY + stepDeltaY);

            // We've reached our target and can FINALLY DIE
            if (remainingDeltaX < TARGET_TOLERANCE && remainingDeltaX > -TARGET_TOLERANCE && remainingDeltaY < TARGET_TOLERANCE && remainingDeltaY > -TARGET_TOLERANCE)
            {
                if (OnComplete != null)
                    OnComplete();
                ModifierCompleted();
				return;
			}
		}
	}
}

