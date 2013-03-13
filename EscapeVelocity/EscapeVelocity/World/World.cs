using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace XNAPractice
{
	public class World : Entity
	{
        private static World instance = null;

        protected List<Entity> removalQueue = new List<Entity>();

        public static World getinstance()
        {
            return instance;
        }

		public World ()
		{
            instance = this;
		}

		public override void Update(float dt)
		{
            base.Update(dt);

            ClearRemovalQueue();

            Globals.DeltaTime = dt;

			int i = 0;
			int t = children.Count;
			while (i < t)
			{
				children[i].UpdateEntityModifiers(dt);
				children[i].Update (dt);
				i++;
			}

            UpdateWorldCollisions();
		}

        // log ( n ^ 2 ) as FUCK
        public void UpdateWorldCollisions()
        {
            List<BroadPhaseCollisionMatch> matches = new List<BroadPhaseCollisionMatch>();

            // Iterating through all entities
            int i = 0;
            int t = children.Count;
            for (; i < t; i++)
            {
                // If this entity is not a Sprite, it can't collide,
                // and we don't have to care about it
                Entity cur = children[i];
                if (!(cur is Sprite))
                    continue;

                // Start iterating through the remainder of the entity list
                int j = i + 1;
                for (; j < t; j++)
                {
                    // If this one isn't a Sprite, we have
                    // no reason to care about it either
                    Entity next = children[j];
                    if (!(next is Sprite))
                        continue;

                    // Temp cast both entities to sprites, since
                    // we know they both can at this point
                    Sprite ent1 = (Sprite)cur;
                    Sprite ent2 = (Sprite)next;

                    // Here's where we check the actual collision
                    // If we get a collision, we make a simple group
                    // that we'll check into later
                    if (ent1.SimpleCollidesWith(ent2))
                    {
                        BroadPhaseCollisionMatch match = new BroadPhaseCollisionMatch();
                        match.spriteOne = ent1;
                        match.spriteTwo = ent2;
                        matches.Add(match);
                    }
                }
            }

            // For debugging, just so we have somewhere
            // to put a breakpoint and observe shit
            if (children.Count > 15)
            {
                int f = 0;
            }

            // Here is where we'll check all the matches
            i = 0;
            t = matches.Count;
            for (; i < t; i++)
            {
                BroadPhaseCollisionMatch match = matches[i];

                if (match.spriteOne.ComplexCollidesWith(match.spriteTwo))
                {
                    Log.Write("Successful complex collision.");
                }
                else
                {
                    Log.Write("No full collision made.");
                }
            }
        }

        // Maybe avoid all the checks each time by just keeping a list of IDrawable entities
        public void Draw(SpriteBatch spriteBatch)
		{
			int i = 0;
			int t = children.Count;
			while (i < t)
			{
				Entity e = children[i];

				if (e is IDrawable)
				{
					((IDrawable)e).Draw(spriteBatch);
				}

				i++;
			}

            if (Settings.DEBUG_OUTPUT)
            {
                spriteBatch.Begin();

                string debugString =
                    "ENTITIES: " + children.Count + "\n" +
                    "REMOVAL QUEUE: " + removalQueue.Count + "\n" +
                    "FPS: " + (60f / Globals.DeltaTime) + "\n" +
                    "CONTROL TYPE: " + ControlType.CONTROL_NAMES[Settings.CONTROL_TYPE] + "\n";
                spriteBatch.DrawString(Fonts.DebugOutput, debugString, new Vector2(10, 10), Color.White);

                string logString = Log.GetLastNString(12);
                spriteBatch.DrawString(Fonts.DebugOutput, logString, new Vector2(10, 500), Color.White);

                spriteBatch.End();
            }
		}

        public void AddToRemovalQueue(Entity e)
        {
            removalQueue.Add(e);
        }

        private void ClearRemovalQueue()
        {
            int i = 0;
            while (i < removalQueue.Count)
            {
                Entity e = removalQueue[i];
                e.GetParent().RemoveChild(e);
                RemoveChild(e);
                i++;
            }

            removalQueue.Clear();
        }

        // Overriding to remove the reference to the world
        public override void AddChild(Entity child)
        {
            child.SetParent(this);
            children.Add(child);
        }
	}
}

