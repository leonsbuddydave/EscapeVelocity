using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using FarseerPhysics;
using FarseerPhysics.Collision;
using FarseerPhysics.Common;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Factories;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;

namespace XNAPractice
{
	class TestEnemy : Sprite
	{
		private float hitDelay = .1f;
		private float currentDelay = 0;

		private bool hit = false;

		Texture2D hitTexture = Globals.Content.Load<Texture2D>("testenemyhit");
		Texture2D notHitTexture = Globals.Content.Load<Texture2D>("testenemy");

		public TestEnemy(float x, float y) : base(Globals.Content.Load<Texture2D>("testenemy"), x, y)
		{
			layer = .5f;

			mBody = BodyFactory.CreateBody(Graph.getPhysicsWorld());
			mBody.BodyType = BodyType.Dynamic;
			mBody.Position = new Vector2(x, y);

			PolygonShape shape = new PolygonShape(new Vertices(new Vector2[]
            {
                new Vector2(0, -1.08f),
                new Vector2(1.44f, 1.08f),
                new Vector2(-1.44f, 1.08f)
            }), 1);

			Fixture f = mBody.CreateFixture(shape);

			f.OnCollision = OnCollision;
		}

		public override bool OnCollision(Fixture f1, Fixture f2, Contact contact)
		{
            hit = true;
            currentDelay = hitDelay;
			return true;
		}

		public override void Update(float dt)
		{
			base.Update(dt);

			this.currentDelay -= dt;

			if (currentDelay <= 0)
			{
				currentDelay = 0;
				hit = false;
			}

			if (hit)
				this.texture = hitTexture;
			else
				this.texture = notHitTexture;
		}
	}
}
