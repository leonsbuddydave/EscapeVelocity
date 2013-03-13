using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNAPractice
{
    interface IWeapon
    {
        void Fire();

        float GetCooldownLevel();
    }
}
