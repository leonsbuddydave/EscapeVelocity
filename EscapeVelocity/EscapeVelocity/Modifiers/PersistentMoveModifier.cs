using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace XNAPractice
{
    class PersistentMoveModifier : MoveModifier
    {
        public PersistentMoveModifier(float targetX, float targetY, float speed) : base(targetX, targetY, speed)
        {

        }

        public void setNewTarget(Vector2 v)
        {
            setNewTarget(v.X, v.Y);
        }

        public void setNewTarget(float x, float y)
        {
            targetX = x;
            targetY = y;
        }

        public override void ModifierCompleted()
        {
            // do FUCKING NOTHING
            // CLOWN
        }
    }
}
