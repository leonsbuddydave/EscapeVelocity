using System;

namespace TestingXamarin
{
	public class EntityModifier : IEntityModifier
	{
		protected IEntity target;

		public EntityModifier ()
		{

		}

		public void setTarget(IEntity target)
		{
			this.target = target;
		}

		public virtual void onStart()
		{

		}

		public virtual void onUpdate(float dt)
		{

		}

		public virtual void onCompleted()
		{

		}
	}
}

