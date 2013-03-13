using System;

namespace TestingXamarin
{
	public interface IEntity : IUpdateListener
	{
		/*
		 * Anything that goes into the world, has to implement this
		 */

		void setTag(int tag);

		void setX(float x);
		void setY(float y);
		void setZ(float z);

		float getX();
		float getY();
		float getZ();

		void updateEntityModifiers(float dt);
		void removeEntityModifier(IEntityModifier e);
	}
}