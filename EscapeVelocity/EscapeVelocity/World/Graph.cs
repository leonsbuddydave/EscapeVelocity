using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

using FarseerPhysics;
using FarseerPhysics.Dynamics;

namespace XNAPractice
{
	public class Graph : Entity
	{
        private static Graph instance = null;

		private static World physicsWorld = new World( new Vector2(0f, 0f) );

        protected List<Entity> removalQueue = new List<Entity>();

        public static Graph getinstance()
        {
            return instance;
        }

		public static World getPhysicsWorld()
		{
			return physicsWorld;
		}

		public Graph ()
		{
            instance = this;
		}

		public override void Update(float dt)
		{
            base.Update(dt);

            Globals.DeltaTime = dt;

			int i = 0;
			int t = children.Count;
			while (i < t)
			{
				children[i].Update (dt);
				children[i].UpdateEntityModifiers(dt);
				i++;
			}

			physicsWorld.Step(dt);

			ClearRemovalQueue();
		}

        // Maybe avoid all the checks each time by just keeping a list of IDrawable entities
        public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Begin(SpriteSortMode.FrontToBack, null, SamplerState.PointClamp, null, null);
			//spriteBatch.Begin();
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

                string debugString =
                    "ENTITIES: " + children.Count + "\n" +
                    "REMOVAL QUEUE: " + removalQueue.Count + "\n" +
                    "FPS: " + (60f / Globals.DeltaTime) + "\n" +
                    "CONTROL TYPE: " + ControlType.CONTROL_NAMES[Settings.CONTROL_TYPE] + "\n";
                spriteBatch.DrawString(Fonts.DebugOutput, debugString, new Vector2(10, 10), Color.White);

                string logString = Log.GetLastNString(13);
                spriteBatch.DrawString(Fonts.DebugOutput, logString, new Vector2(10, 500), Color.White);
            }

			spriteBatch.End();
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

				if (e is Sprite)
				{
					Body b = ((Sprite)e).GetBody();

					if (b != null)
						getPhysicsWorld().RemoveBody(b);
				}

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

