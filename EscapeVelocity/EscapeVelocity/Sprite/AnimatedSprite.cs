using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace XNAPractice
{
    class AnimatedSprite : Sprite
    {
        public Texture2D texture { get; set; }
        public int rows { get; set; }
        public int columns { get; set; }
        private float timePerFrame;
        private int currentFrame;
        private int totalFrames;

        private float timelinePosition = 0.0f;
        private float timelineLength = 0.0f;

        private bool looping = true;

        public AnimatedSprite(Texture2D texture, int rows, int columns, float x, float y) : base(texture, x, y)
        {
            ConstructorHelper(texture, rows, columns, 1f);
        }

        public AnimatedSprite(Texture2D texture, int rows, int columns, float x, float y, float timePerFrame) : base(texture, x, y)
        {
            ConstructorHelper(texture, rows, columns, timePerFrame);
        }

        // Branches out code common to both constructors
        private void ConstructorHelper(Texture2D texture, int rows, int columns, float timePerFrame)
        {
            this.texture = texture;
            this.rows = rows;
            this.columns = columns;
            this.timePerFrame = timePerFrame;

            // Makes sure we get the width and height of the SPRITE, not the ENTIRE TEXTURE
            this.Width = texture.Width / columns;
            this.Height = texture.Height / rows;

            currentFrame = 0;
            totalFrames = rows * columns;

            timelineLength = totalFrames * timePerFrame;
        }

        public void setLooping(bool looping)
        {
            this.looping = looping;
        }

        public void setTimePerFrame(float timePerFrame)
        {
            this.timePerFrame = timePerFrame;
            timelineLength = totalFrames * timePerFrame;
            timelinePosition = 0.0f;
        }

        public override void Update(float dt)
        {
            base.Update(dt);

            timelinePosition += dt;

            if (timelinePosition >= timelineLength && looping)
                timelinePosition -= timelineLength;
            else if (!looping)
                timelinePosition = timelineLength;

            currentFrame = (int)Math.Floor(timelinePosition / timePerFrame);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            float drawX = x, drawY = y;

            if (parent != null)
            {
                drawX = x + parent.GetX();
                drawY = y + parent.GetY();
            }

            int width = texture.Width / columns;
            int height = texture.Height / rows;

            int row = (int)((float)currentFrame / (float)columns);
            int column = currentFrame % columns;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);

            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null);
            spriteBatch.Draw(
                texture,
                new Vector2(drawX, drawY),
                sourceRectangle,
                Color.White,
                angle,
                new Vector2(width / 2, height / 2),
                new Vector2(scaleX, scaleY),
                SpriteEffects.None,
                layer
             );
            spriteBatch.End();

            DrawChildren(spriteBatch);
        }
    }
}
