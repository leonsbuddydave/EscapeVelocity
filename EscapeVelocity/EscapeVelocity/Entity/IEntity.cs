using System;

namespace XNAPractice
{
	public interface IEntity : IUpdateListener
	{
		/*
		 * Anything that goes into the world, has to implement this
		 */

		void SetTag(int tag);

        // Sets coordinates
		void SetX(float x);
		void SetY(float y);
		void SetZ(float z);

        // Gets coordinates
		float GetX();
		float GetY();
		float GetZ();

        // Gets "real" coordinates, accounting for parent offsets
        float GetRealX();
        float GetRealY();

        // Add and remove entity modifiers
		void UpdateEntityModifiers(float dt);
		void RemoveEntityModifier(IEntityModifier e);

        // Add and remove entities
        void AddChild(Entity e);
        void RemoveChild(Entity e);
	}
}