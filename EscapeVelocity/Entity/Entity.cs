using System;
using System.Collections.Generic;

namespace TestingXamarin
{
	public class Entity : IEntity
	{
		protected int tag = -1;
		protected float x = 0.0f;
		protected float y = 0.0f;
		protected float z = 0.0f;

		private List<IEntityModifier> entityModifiers = new List<IEntityModifier> ();

		public Entity ()
		{
			// Why? Why the hell?
		}

		// Entity Modifier methods
		public void registerEntityModifier(IEntityModifier em)
		{
			em.setTarget (this);
			entityModifiers.Add (em);
		}

		public void removeEntityModifier(IEntityModifier em)
		{
			entityModifiers.Remove (em);
		}

		public void updateEntityModifiers(float dt)
		{
			int i = 0;
			while (i < entityModifiers.Count)
			{
				entityModifiers[i].onUpdate (dt);
				i++;
			}
		}

		public void setTag(int tag)
		{
			this.tag = tag;
		}

		public virtual void Update(float dt)
		{

		}

		public void setX(float x)
		{
			this.x = x;
		}

		public void setY(float y)
		{
			this.y = y;
		}

		public void setZ(float z)
		{
			this.z = z;
		}

		public float getX()
		{
			return x;
		}

		public float getY()
		{
			return y;
		}

		public float getZ()
		{
			return z;
		}
	}
}

