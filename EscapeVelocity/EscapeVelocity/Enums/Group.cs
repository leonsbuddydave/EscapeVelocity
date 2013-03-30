using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FarseerPhysics.Dynamics;

namespace XNAPractice
{
	public static class Group
	{
		public const Category PLAYER = Category.Cat1;
		public const Category ENEMY = Category.Cat2;
		public const Category PROJECTILE_PLAYER = Category.Cat3;
		public const Category PROJECTILE_ENEMY = Category.Cat4;
		public const Category WORLD_BORDER = Category.Cat5;
	}
}
