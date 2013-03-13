using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace XNAPractice
{
    class Cursor : Sprite
    {
        public Cursor(float x, float y)
            : base(Globals.Content.Load<Texture2D>("cursor"), x, y)
        {
            
        }

        public override void Update(float dt)
        {
            base.Update(dt);

            MouseState mouse = Mouse.GetState();

            x += mouse.X - TestGame.gameWidth / 2;
            y += mouse.Y - TestGame.gameHeight / 2;
        }
    }
}
