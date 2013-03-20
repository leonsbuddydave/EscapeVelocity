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
    class Globals
    {
		// Global devices stuff
        public static ContentManager Content;
		public static GraphicsDevice Graphics;

		// Global access to time delta
        public static float DeltaTime = 0.0f;

		// Matrices
		public static Matrix ViewMatrix, ProjectionMatrix, WorldMatrix;

		public const float PixelsPerMeter = 50.0f;
    }
}
