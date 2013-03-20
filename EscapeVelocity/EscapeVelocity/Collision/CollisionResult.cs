using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNAPractice
{
	public struct CollisionResult
	{
		public bool one, two;

		public CollisionResult(bool one, bool two)
		{
			this.one = one;
			this.two = two;
		}
	}
}
