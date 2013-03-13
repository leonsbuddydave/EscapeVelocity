using System;
using System.Collections.Generic;

namespace TestingXamarin
{
	public class World : IUpdateListener
	{
		private List<IEntity> entities = new List<IEntity> ();

		public World ()
		{

		}

		public void Update(float dt)
		{
			int i = 0;
			int t = entities.Count;
			while (i < t)
			{
				entities[i].updateEntityModifiers(dt);
				entities[i].Update (dt);
				i++;
			}
		}

		// Maybe avoid all the checks each time by just keeping a list of IDrawable entities
		public void Draw()
		{
			int i = 0;
			int t = entities.Count;
			while (i < t)
			{
				IEntity e = entities[i];

				if (e is IDrawable)
				{
					((IDrawable)e).Draw();
				}

				i++;
			}
		}

		public void Add(IEntity entity)
		{
			entities.Add (entity);
		}
	}
}

