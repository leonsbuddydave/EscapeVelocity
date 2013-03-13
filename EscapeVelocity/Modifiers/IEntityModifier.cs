using System;

namespace TestingXamarin
{
	public interface IEntityModifier
	{
		void setTarget(IEntity target);

		void onStart();

		void onUpdate(float dt);

		void onCompleted();
	}
}