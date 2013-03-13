using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace XNAPractice
{
    public class Sprite : Drawable
    {
        private Texture2D texture;
        protected float
            angle = 0.0f,
            scaleX = 1.0f,
            scaleY = 1.0f,
            layer = 0.0f;

        protected int Width = 0, Height = 0;

        protected PolygonCollider polygonCollider = null;

	    public Sprite(Texture2D texture, float x, float y)
	    {
            this.texture = texture;
            this.x = x;
            this.y = y;

            this.Width = texture.Width;
            this.Height = texture.Height;
	    }

        public bool SimpleCollidesWith(Sprite him)
        {
            if (this.polygonCollider == null || him.polygonCollider == null)
                return false;

            return polygonCollider.SimpleCollidesWith(him.polygonCollider);
        }

        public bool ComplexCollidesWith(Sprite him)
        {
            // initial check
            if (
                ( this.polygonCollider.CollisionCategory & him.polygonCollider.CollisionGroups ) != this.polygonCollider.CollisionCategory &&
                ( him.polygonCollider.CollisionCategory & this.polygonCollider.CollisionGroups ) != him.polygonCollider.CollisionCategory
                )
            {
                // we have no desired matches here - nothing happens
                return false;
            }

            return false;
        }

        public Point getCenter()
        {
            return new Point(Width / 2, Height / 2);
        }

        public Point getWorldCenter()
        {
            return new Point(Width / 2 + GetRealX(), Height / 2 + GetRealY());
        }

        public PolygonCollider getCollider()
        {
            return polygonCollider;
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
            }

            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null);
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
            spriteBatch.End();

            DrawChildren(spriteBatch);
        }
    }
}