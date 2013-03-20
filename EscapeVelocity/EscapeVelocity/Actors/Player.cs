using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace XNAPractice
{
    class Player : AnimatedSprite
    {
        private IWeapon weaponPrimary, weaponSecondary;

        private float GAMEPAD_SPEED = 600f;

        public Player(float x, float y)
            : base(Globals.Content.Load<Texture2D>("player"), 1, 1, x, y, .5f)
        {
            tag = Tags.PLAYER;
			layer = .5f;

            setWeapon(WeaponSlot.PRIMARY, new SimpleGun(0, 54 / Globals.PixelsPerMeter));

            collider = new Collider(this, new Vector2[]{
                new Vector2(-72, -54),
                new Vector2(72, -54),
                new Vector2(72, 54),
                new Vector2(-72, 54)
            });
            collider.CollisionGroups = CollisionGroup.COLLIDES_ENEMY_PROJECTILES;
            collider.CollisionCategory = CollisionGroup.COLLIDES_PLAYER;
        }

        // Assigns a weapon to a particular slot on the player - 
        // we may want to abstract functionality like this out to a
        // "ship" subclass so we can take advantage of it with enemies, too
        public void setWeapon(WeaponSlot slot, IWeapon weapon)
        {
            switch (slot)
            {
                case WeaponSlot.PRIMARY:
                    {
                        RemoveChild( (Entity)weaponPrimary);
                        weaponPrimary = weapon;
                        AddChild( (Entity)weaponPrimary);
                    }
                    break;

                case WeaponSlot.SECONDARY:
                    {
                        RemoveChild((Entity)weaponSecondary);
                        weaponSecondary = weapon;
                        AddChild( (Entity)weaponSecondary );
                    }
                    break;
            }
        }

        public override void Update(float dt)
        {
            base.Update(dt);

            switch (Settings.CONTROL_TYPE)
            {
                case ControlType.MOUSE:
                default:
                    {
                        MouseState mouse = Mouse.GetState();

                        x += (mouse.X - TestGame.gameWidth / 2 ) / Globals.PixelsPerMeter;
                        y += (mouse.Y - TestGame.gameHeight / 2 ) / Globals.PixelsPerMeter;

                        if (mouse.LeftButton == ButtonState.Pressed && weaponPrimary != null)
                            weaponPrimary.Fire();
                    
                        if (mouse.RightButton == ButtonState.Pressed && weaponSecondary != null)
                            weaponSecondary.Fire();
                    }
                    break;

                case ControlType.GAMEPAD:
                    {
                        GamePadState gamepad = GamePad.GetState(PlayerIndex.One);

                        x += gamepad.ThumbSticks.Left.X * GAMEPAD_SPEED * dt;
                        y -= gamepad.ThumbSticks.Left.Y * GAMEPAD_SPEED * dt;

                        if (gamepad.Triggers.Right > 0 && weaponPrimary != null)
                            weaponPrimary.Fire();

                        if (gamepad.Triggers.Left > 0 && weaponSecondary != null)
                            weaponSecondary.Fire();
                    }
                    break;

                case ControlType.KEYBOARD:
                    {
                        KeyboardState keyboard = Keyboard.GetState();

                        if (keyboard.IsKeyDown(Keys.D) || keyboard.IsKeyDown(Keys.Right))
                            x += GAMEPAD_SPEED * dt;
                        else if (keyboard.IsKeyDown(Keys.A) || keyboard.IsKeyDown(Keys.Left))
                            x -= GAMEPAD_SPEED * dt;

                        if (keyboard.IsKeyDown(Keys.W) || keyboard.IsKeyDown(Keys.Up))
                            y -= GAMEPAD_SPEED * dt;
                        else if (keyboard.IsKeyDown(Keys.S) || keyboard.IsKeyDown(Keys.Down))
                            y += GAMEPAD_SPEED * dt;

                        if (keyboard.IsKeyDown(Keys.Space) && weaponPrimary != null)
                            weaponPrimary.Fire();
                    }
                    break;
            }
        }
    }
}
