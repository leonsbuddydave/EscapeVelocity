using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XNAPractice
{
	class Primitives
	{
		private static BasicEffect basicEffect = new BasicEffect(Globals.Graphics);
		private static Texture2D lineBlank = new Texture2D(Globals.Graphics, 1, 1, false, SurfaceFormat.Color);

		private static float debugLineWidth = 4f;

		public static void DrawCollider(Collider collider, SpriteBatch batch, Color color)
		{
			Globals.Graphics.Textures[0] = null;
			lineBlank.SetData(new[] { color });

			Vector2[] points = collider.GetPolygon().GetPoints();

			Vector2 ownerPosition = collider.GetOwner().GetRealPosition();

			for (int i = 0; i < points.Length; i++)
			{
				Vector2 point1 = points[i] + ownerPosition;
				Vector2 point2 = points[i + 1 < points.Length ? i + 1 : 0] + ownerPosition;

				DrawLine(batch, color, point1, point2);
			}
		}

		private static void DrawLine(SpriteBatch batch, Color color, Vector2 point1, Vector2 point2)
		{
			float angle = (float)Math.Atan2( point2.Y - point1.Y, point2.X - point1.X );
			float length = Vector2.Distance(point1, point2);

			batch.Draw(lineBlank, point1, null, Color.White, angle, Vector2.Zero, new Vector2(length, debugLineWidth), SpriteEffects.None, 1);
		}

		public static void BasicEffectTest()
		{
			VertexBuffer vertexBuffer;

			VertexPositionColor[] vertices = new VertexPositionColor[4];
			vertices[0] = new VertexPositionColor(new Vector3(0, 1, 0), Color.Red);
			vertices[1] = new VertexPositionColor(new Vector3(0.5f, 0, 0), Color.Red);
			vertices[2] = new VertexPositionColor(new Vector3(0.5f, 0, 0), Color.Red);
			vertices[3] = new VertexPositionColor(new Vector3(-0.5f, 0, 0), Color.Red);

			vertexBuffer = new VertexBuffer(Globals.Graphics, typeof(VertexPositionColor), 4, BufferUsage.WriteOnly);
			vertexBuffer.SetData<VertexPositionColor>(vertices);

			basicEffect.VertexColorEnabled = true;

			RasterizerState rasterizerState = new RasterizerState();
			rasterizerState.CullMode = CullMode.None;
			Globals.Graphics.RasterizerState = rasterizerState;

			foreach (EffectPass pass in basicEffect.CurrentTechnique.Passes)
			{
				pass.Apply();
				Globals.Graphics.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.LineList, vertices, 0, 2 );
			}
		}	
	}
}
