using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using FarseerPhysics;

namespace XNAPractice
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class TestGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public static int gameWidth = 1280;
        public static int gameHeight = 720;

        Graph graph = new Graph();

        public TestGame()
        {
            graphics = new GraphicsDeviceManager(this);

            this.graphics.PreferredBackBufferWidth = gameWidth;
            this.graphics.PreferredBackBufferHeight = gameHeight;
            this.Window.Title = "Escape Velocity";

            //graphics.IsFullScreen = true;
            //graphics.SynchronizeWithVerticalRetrace = false;
            //IsFixedTimeStep = false;

			Globals.ScreenHeight = gameHeight;
			Globals.ScreenWidth = gameWidth;

            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            Globals.Content = this.Content;
			Globals.Graphics = this.graphics.GraphicsDevice;

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            graph.AddChild(new Player(0, 0));
			graph.AddChild(new TestEnemy(18, 3));
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            graph.Update((float)gameTime.ElapsedGameTime.TotalSeconds);

            Mouse.SetPosition(TestGame.gameWidth / 2, TestGame.gameHeight / 2);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            Globals.ViewMatrix = Matrix.CreateLookAt(new Vector3(0, 0, 10), new Vector3(0, 0, 0), Vector3.UnitY);
            Globals.ProjectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, GraphicsDevice.Viewport.AspectRatio, 0.1f, 100f);
            Globals.WorldMatrix = Matrix.CreateTranslation(new Vector3(0, 0, 0));

            graph.Draw(spriteBatch);

            base.Draw(gameTime);
        }
    }
}
