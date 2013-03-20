using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace XNAPractice
{
	public class Entity : IEntity
	{
		protected int tag = Tags.NONE;
		protected float x = 0.0f;
		protected float y = 0.0f;
		protected float z = 0.0f;

        protected List<Entity> children = new List<Entity>();
        protected Entity parent = null;

		private List<IEntityModifier> entityModifiers = new List<IEntityModifier> ();

		public Entity ()
		{
            parent = Graph.getinstance();
		}

		// Adds an entity modifier to this entity
		public void RegisterEntityModifier(EntityModifier em)
		{
			em.setTarget (this);
			entityModifiers.Add (em);
		}

        // Adds an entity modifier to this entity WITH a callback on the completion of the modifier
        public void RegisterEntityModifier(EntityModifier em, EntityModifier.EntityModifierOnCompleteHandler d)
        {
            em.OnComplete = d;
            RegisterEntityModifier(em);
        }

        // Removes an entity modifier given its reference
		public void RemoveEntityModifier(IEntityModifier em)
		{
			entityModifiers.Remove (em);
		}

        // update my goddamn entity modifiers
        public void UpdateEntityModifiers(float dt)
        {
            int i = 0;
            while (i < entityModifiers.Count)
            {
                entityModifiers[i].onUpdate(dt);
                i++;
            }
        }

        // Attaches a child to tihs entity
        public virtual void AddChild(Entity child)
        {
            Graph.getinstance().AddChild(child);
            child.SetParent(this);
            children.Add(child);
        }

        // Removes a child from this entity - 
        // or rather, marks it for removal on the next loop
        // to prevent conflict
        public void RemoveChild(Entity child)
        {
            if (child == null)
                return;

            children.Remove(child);
        }

        // Queues this entity for removal
        public virtual void Remove()
        {
            Graph.getinstance().AddToRemovalQueue(this);
        }

        // Set this entity's parent
        public void SetParent(Entity parent)
        {
            this.parent = parent;
        }

        public Entity GetParent()
        {
            return parent;
        }

		public void SetTag(int tag)
		{
			this.tag = tag;
		}

        public int GetTag()
        {
            return tag;
        }

        /*
         * Returns the first child it finds with the specified tag 
         */
        public Entity GetChildByTag(int tag)
        {
            int i = 0;
            while (i < children.Count)
            {
                if (children[i].GetTag() == tag)
                    return children[i];
                i++;
            }

            return null;
        }

        public Entity[] GetChildrenByTag(int tag)
        {
            List<Entity> entities = new List<Entity>();

            int i = 0;
            while (i < children.Count)
            {
                if (children[i].GetTag() == tag)
                    entities.Add(children[i]);
                i++;
            }

            return entities.ToArray();
        }

		public virtual void Update(float dt)
		{
            
		}

        protected void UpdateChildren(float dt)
        {
            foreach (Entity child in children)
            {
                child.Update(dt);
            }
        }

		public void SetX(float x)
		{
			this.x = x;
		}

		public void SetY(float y)
		{
			this.y = y;
		}

		public void SetZ(float z)
		{
			this.z = z;
		}

		public void SetPosition(Vector2 pos)
		{
			SetX(pos.X);
			SetY(pos.Y);
		}

		public float GetX()
		{
			return x;
		}

		public float GetY()
		{
			return y;
		}

		public float GetZ()
		{
			return z;
		}

		public Vector2 GetRealPosition()
		{
			return new Vector2(GetRealX(), GetRealY());
		}

        public float GetRealX()
        {
            float x = GetX();
            Entity ent = this.parent;

            while (ent != null)
            {
                x += ent.GetX();
                ent = ent.parent;
            }

            return x;
        }

        public float GetRealY()
        {
            float y = GetY();
            Entity ent = this.parent;

            while (ent != null)
            {
                y += ent.GetY();

                ent = ent.parent;
            }

            return y;
        }
	}
}

