using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna;
using Microsoft.Xna.Framework;

using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Common;

namespace XNAPractice
{
	class WorldBorder : Entity
	{
		public WorldBorder()
		{
			World world = Graph.getPhysicsWorld();

			Body border = BodyFactory.CreateBody(world);
			border.BodyType = BodyType.Kinematic;
			border.IgnoreGravity = true;
			border.Position = new Vector2(0, 0);

			float margin = Globals.BorderMargin;
			float width = Globals.BorderWidth;
			float screenWidthInMeters = (float)Globals.ScreenWidth / Globals.PixelsPerMeter;
			float screenHeightInMeters = (float)Globals.ScreenHeight / Globals.PixelsPerMeter;

            Fixture leftBorder, rightBorder, topBorder, bottomBorder;

			leftBorder = border.CreateFixture(new PolygonShape(new Vertices(new Vector2[]
			{
				new Vector2(-margin - width, -margin - width),
				new Vector2(-margin, -margin - width),
				new Vector2(-margin, screenHeightInMeters + (margin + width) * 2),
				new Vector2(-margin - width, screenHeightInMeters + (margin + width) * 2)
			}), 1.0f));

            rightBorder = border.CreateFixture(new PolygonShape(new Vertices(new Vector2[]
            {
                new Vector2( screenWidthInMeters + margin, -margin - width),
                new Vector2( screenWidthInMeters + margin + width, -margin - width),
                new Vector2( screenWidthInMeters + margin + width, screenHeightInMeters + margin + width),
                new Vector2( screenWidthInMeters + margin, screenHeightInMeters + margin + width)
            }), 1.0f));

            topBorder = border.CreateFixture(new PolygonShape(new Vertices(new Vector2[]
            {
                new Vector2( -margin - width, -margin - width ),
                new Vector2( screenWidthInMeters + margin + width, -margin - width ),
                new Vector2( screenWidthInMeters + margin + width, -margin ),
                new Vector2( -margin - width, -margin)
            }), 1.0f));

            bottomBorder = border.CreateFixture(new PolygonShape(new Vertices(new Vector2[]
            {
                new Vector2( -margin - width, screenHeightInMeters + margin ),
                new Vector2( screenWidthInMeters + margin + width, screenHeightInMeters + margin),
                new Vector2( screenWidthInMeters + margin + width, screenHeightInMeters + margin + width),
                new Vector2( -margin - width, screenHeightInMeters + margin + width )
            }), 1.0f));

            leftBorder.CollidesWith =
                rightBorder.CollidesWith =
                bottomBorder.CollidesWith =
                topBorder.CollidesWith = Group.PROJECTILE_PLAYER;

            leftBorder.CollisionCategories =
                rightBorder.CollisionCategories =
                bottomBorder.CollisionCategories = 
                topBorder.CollisionCategories = Group.WORLD_BORDER;
   		}
	}
}
 