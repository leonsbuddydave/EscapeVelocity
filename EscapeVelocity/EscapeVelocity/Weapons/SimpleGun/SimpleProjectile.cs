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

namespace XNAPractice
{
    class SimpleProjectile : Sprite
    {
        private Vector2 projectileVelocity = new Vector2(50, 0);

        private SimpleProjectile instance;

        protected int damage = 1;

        public SimpleProjectile(float x, float y) : base(Globals.Content.Load<Texture2D>("simpleprojectile"), x, y)
        {
			layer = .55f;
            instance = this;

			mBody = BodyFactory.CreateBody(Graph.getPhysicsWorld());
			mBody.BodyType = BodyType.Dynamic;
			mBody.Position = new Vector2(x, y);
			mBody.Restitution = 1f;
			mBody.IgnoreGravity = true;

			PolygonShape shape = new PolygonShape(new Vertices(new Vector2[]
            {
                new Vector2(-.26f, -.18f),
                new Vector2(.26f, -.18f),
                new Vector2(.26f, .18f),
                new Vector2(-.26f, .18f)
            }), .1f);

			Fixture f = mBody.CreateFixture(shape);

			f.OnCollision = OnCollision;

			f.CollisionCategories = Group.PROJECTILE_PLAYER;
			f.CollidesWith = Group.ENEMY | Group.WORLD_BORDER;

            mBody.LinearVelocity = projectileVelocity;
        }

		public override bool OnCollision(Fixture f1, Fixture f2, FarseerPhysics.Dynamics.Contacts.Contact contact)
		{
			this.Remove();
			return true;
		}

        public int getDamage()
        {
            return damage;
        }

		public override void Update(float dt)
		{
			base.Update(dt);

			
		}
    }
}
