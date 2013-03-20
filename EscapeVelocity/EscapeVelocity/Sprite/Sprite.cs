using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Common;
using FarseerPhysics.Dynamics.Contacts;
using FarseerPhysics.Collision;

namespace XNAPractice
{
    public class Sprite : Drawable
    {
        protected Texture2D texture;
		protected Texture2D boundingTexture;

        protected float
            angle = 0.0f,
            scaleX = 1.0f,
            scaleY = 1.0f,
            layer = 0.0f;

        protected int Width = 0, Height = 0;

        protected Collider collider = null;

		public delegate void OnEntityCollisionListener(Entity theOtherGuy);

		public OnEntityCollisionListener Collision;

		protected Body mBody = null;
		protected Fixture mFixture = null;

	    public Sprite(Texture2D texture, float x, float y)
	    {
            this.texture = texture;
            this.x = x;
            this.y = y;

            this.Width = texture.Width;
            this.Height = texture.Height;
	    }

        public bool WithinRangeOf(Sprite him)
        {
            if (this.collider == null || him.collider == null)
                return false;

            return collider.WithinRangeOf(him.collider);
        }

        public Vector2 getCenter()
        {
            return new Vector2(Width / 2, Height / 2);
        }

        public Vector2 getWorldCenter()
        {
            return new Vector2(Width / 2 + GetRealX(), Height / 2 + GetRealY());
        }

		public Body GetBody()
		{
			return mBody;
		}

        public void setScale(float scale)
        {
            scaleX = scale;
            scaleY = scale;
        }

        public void setLayer(float layer)
        {
            this.layer = layer;
        }

        public override void DrawChildren(SpriteBatch spriteBatch)
        {
            foreach (Entity child in children)
            {
                if (child is IDrawable)
                {
                    ((IDrawable)child).Draw(spriteBatch);
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            float drawX = x, drawY = y;

            if (parent != null)
            {
                drawX = x + parent.GetX();
                drawY = y + parent.GetY();

				drawX *= Globals.PixelsPerMeter;
				drawY *= Globals.PixelsPerMeter;
            }

			
            spriteBatch.Draw(
                texture,
                new Vector2( drawX, drawY ),
                null,
                Color.White,
                angle,
                new Vector2(texture.Width / 2, texture.Height / 2),
                new Vector2(scaleX, scaleY),
                SpriteEffects.None,
                layer
             );

			if (Settings.DEBUG_OUTPUT)
			{
				if (collider != null)
					Primitives.DrawCollider(collider, spriteBatch, Color.White);
			}
        }

		public override void Update(float dt)
		{
			// If we're attached to a physics body, relocate to wherever it went
			if (mBody != null)
			{
				SetPosition(mBody.Position);
				angle = mBody.Rotation;
			}
		}

		public virtual bool OnCollision(Fixture f1, Fixture f2, Contact contact)
		{

			return true;
		}
    }
}