using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace XNAPractice
{
    class SimpleGun : Sprite, IWeapon
    {
        private const float DELAY_BETWEEN_SHOTS = .05f;
		//private const float DELAY_BETWEEN_SHOTS = .1f;
        private float delayRemaining = 0f;

        public SimpleGun(float x, float y) : base(Globals.Content.Load<Texture2D>("simplegun"), x, y)
        {
			layer = .6f;
        }

        public void Fire()
        {
            // If our delay has run out, we can fire
            if (delayRemaining <= 0)
            {
                World.getinstance().AddChild(new SimpleProjectile(GetRealX(), GetRealY()));
                delayRemaining = DELAY_BETWEEN_SHOTS;
            }
        }

        public float GetCooldownLevel()
        {
            return (DELAY_BETWEEN_SHOTS - delayRemaining) / DELAY_BETWEEN_SHOTS * 100;
        }

        public override void Update(float dt)
        {
            base.Update(dt);

            if (delayRemaining > 0)
                delayRemaining -= dt;

            if (delayRemaining <= 0)
                delayRemaining = 0;
        }
    }
}
