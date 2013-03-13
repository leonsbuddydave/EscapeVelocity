using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace XNAPractice
{
    class SimpleProjectile : Sprite
    {
        private const int projectileSpeed = 1500;

        private SimpleProjectile instance;

        protected int damage = 1;

        public SimpleProjectile(float x, float y) : base(Globals.Content.Load<Texture2D>("simpleprojectile"), x, y)
        {
            instance = this;
            RegisterEntityModifier(new MoveModifier(1450, y, projectileSpeed), delegate()
            {
                // Movement has completed, kill me
                instance.Remove();
            });

            polygonCollider = new PolygonCollider(this, new Polygon(new Point[]
            {
                new Point(-13, -9),
                new Point(13, -9),
                new Point(13, 9),
                new Point(-13, 9)
            }));
        }

        public int getDamage()
        {
            return damage;
        }
    }
}
