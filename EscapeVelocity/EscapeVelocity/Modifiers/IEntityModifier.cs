using System;

namespace XNAPractice
{
	public interface IEntityModifier
	{
		void setTarget(Entity target);

		void onStart();

		void onUpdate(float dt);

        void ModifierCompleted();
	}
}