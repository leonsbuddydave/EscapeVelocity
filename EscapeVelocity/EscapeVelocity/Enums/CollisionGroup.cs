using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNAPractice
{
    public class CollisionGroup
    {
        public const int COLLIDES_PLAYER = 1;
        public const int MASK_ENEMY = 2;
        public const int MASK_PLAYER_PROJECTILE = 4;
        public const int COLLIDES_ENEMY_PROJECTILES = 8;
        public const int COLLIDES_ENVIRONMENT = 16;
    }
}
