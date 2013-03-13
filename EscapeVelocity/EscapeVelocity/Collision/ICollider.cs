using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNAPractice
{
    // Not sure why I thought we'd need this, may get rid of it
    interface ICollider
    {
        bool CollidesWith(ICollider e);
    }
}
