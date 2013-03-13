using System;
using Microsoft.Xna.Framework.Graphics;

namespace XNAPractice
{
	public abstract class Drawable : Entity, IDrawable
	{
        public abstract void Draw(SpriteBatch spriteBatch);

        public abstract void DrawChildren(SpriteBatch spriteBatch);
	}
}

