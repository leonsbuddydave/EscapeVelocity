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
	public class TriangleEntity : Sprite
	{

		public TriangleEntity () : base(Globals.Content.Load<Texture2D>("shuttle"), 0, 30)
		{
			z = 100;
			RegisterEntityModifier (new MoveModifier (-10, -10, 5)
			{
                
			});
		}
	}
}

