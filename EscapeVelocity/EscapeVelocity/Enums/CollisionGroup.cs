using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNAPractice
{
    public class CollisionGroup
    {
        public const int COLLIDES_PLAYER = 1;
        public const int COLLIDES_ENEMIES = 2;
        public const int COLLIDES_PLAYER_PROJECTILES = 4;
        public const int COLLIDES_ENEMY_PROJECTILES = 8;
        public const int COLLIDES_ENVIRONMENT = 16;
    }
}
