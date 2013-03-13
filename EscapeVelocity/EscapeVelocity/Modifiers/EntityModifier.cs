using System;

namespace XNAPractice
{
	public class EntityModifier : IEntityModifier
	{
		protected Entity target;
        public delegate void EntityModifierOnCompleteHandler();

        public EntityModifierOnCompleteHandler OnComplete;

		public EntityModifier ()
		{

		}

		public void setTarget(Entity target)
		{
			this.target = target;
		}

		public virtual void onStart()
		{

		}

		public virtual void onUpdate(float dt)
		{

		}

        public virtual void ModifierCompleted()
        {

        }
	}
}

