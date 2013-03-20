using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Box2D.XNA;

namespace XNAPractice
{
    class SimpleProjectile : Sprite
    {
        private const int projectileSpeed = 1400;

        private SimpleProjectile instance;

        protected int damage = 1;

		private MoveModifier mm;

        public SimpleProjectile(float x, float y) : base(Globals.Content.Load<Texture2D>("simpleprojectile"), x, y)
        {
			layer = .55f;
            instance = this;

			mm = new MoveModifier(1450, y, projectileSpeed);

			BodyDef bd = new BodyDef();
			PolygonShape shape = new PolygonShape();
			FixtureDef fd = new FixtureDef();

			bd.type = BodyType.Dynamic;
			bd.position = new Vector2(x, y);
			mBody = World.getPhysicsWorld().CreateBody(bd);

			mBody.SetUserData(this);

			shape.Set(new Vector2[]
            {
                new Vector2(-.26f, -.18f),
                new Vector2(.26f, -.18f),
                new Vector2(.26f, .18f),
                new Vector2(-.26f, .18f)
            }, 4);

			fd.shape = shape;
			fd.density = 1.0f;
			fd.friction = 5.0f;
			fd.restitution = .5f;
			fd.filter.maskBits = CollisionGroup.MASK_PLAYER_PROJECTILE;
			fd.filter.categoryBits = CollisionGroup.MASK_ENEMY;
			fd.density = .01f;

			mFixture = mBody.CreateFixture(fd);

			World.getPhysicsWorld().ContactListener = this;
        }

        public int getDamage()
        {
            return damage;
        }

		public override void Update(float dt)
		{
			base.Update(dt);

			mBody.SetLinearVelocity(new Vector2(40, 0));
		}

		public override void BeginContact(Contact contact)
		{
			instance.Remove();
		}
    }
}
