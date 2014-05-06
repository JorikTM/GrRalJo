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

namespace Graphics_tutorial_1
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private FrameRateCounter frameRateCounter;
        private float[,] heightData;
        private float angle;
        private Terrain terrain;

        private Camera camera;

        private BasicEffect effect;
        private VertexPositionColor[] vertices;

        private void loadHeightData()
        {
            this.heightData = new float[4, 3];

            this.heightData[0, 0] = 0;
            this.heightData[1, 0] = 0;
            this.heightData[2, 0] = 0;
            this.heightData[3, 0] = 0;

            this.heightData[0, 1] = 0.5f;
            this.heightData[1, 1] = 0;
            this.heightData[2, 1] = -1.0f;
            this.heightData[3, 1] = 0.2f;

            this.heightData[0, 2] = 1.0f;
            this.heightData[1, 2] = 1.2f;
            this.heightData[2, 2] = 0.8f;
            this.heightData[3, 2] = 0;
        }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.graphics.PreferredBackBufferWidth = 800;
            this.graphics.PreferredBackBufferHeight = 600;
            this.graphics.IsFullScreen = false;
            this.graphics.SynchronizeWithVerticalRetrace = false;
            this.frameRateCounter = new FrameRateCounter(this);
            this.Components.Add(frameRateCounter);
            this.graphics.ApplyChanges();
            this.IsFixedTimeStep = false;
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
            // Create a new SpriteBatch, which can be used to draw textures.
          // Texture2D map = Content.Load<Texture2D>("heightmap"); 
            spriteBatch = new SpriteBatch(GraphicsDevice);
            this.effect = new BasicEffect(this.GraphicsDevice);
            //this.loadHeightData();

            Texture2D map = Content.Load<Texture2D>("heightmap");
            this.terrain = new Terrain(new HeightMap(map), 0.2f);

            this.effect.VertexColorEnabled = true;
            this.camera = new Camera(new Vector3(0, 10, 0), new Vector3(0, 0, 0),
     new Vector3(0, 0, -1));

            // TODO: use this.Content to load your game content here
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
            float timeStep = (float)gameTime.ElapsedGameTime.TotalSeconds;
            this.effect.Projection = this.camera.ProjectionMatrix;
            this.effect.View = this.camera.ViewMatrix;
            this.effect.World = Matrix.Identity;
            float deltaAngle = 0;
            KeyboardState kbState = Keyboard.GetState();

            if (kbState.IsKeyDown(Keys.Left))
                deltaAngle += -3 * timeStep;
            if (kbState.IsKeyDown(Keys.Right))
                deltaAngle += 3 * timeStep;

            if (deltaAngle != 0)
                this.camera.Eye = Vector3.Transform(this.camera.Eye, Matrix.CreateRotationY(deltaAngle));

            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            KeyboardState testkeyboardstate = Keyboard.GetState();

            if (testkeyboardstate.IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }


            // TODO: Add your update logic here
            this.Window.Title = "graphics Tutorial | FPS: " + this.frameRateCounter.FrameRate;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            this.GraphicsDevice.RasterizerState = new RasterizerState
            {
                CullMode = CullMode.None,
                FillMode = FillMode.WireFrame
            };

            GraphicsDevice.Clear(Color.DarkSlateBlue);

            this.effect.Projection = this.camera.ProjectionMatrix;
            this.effect.View = this.camera.ViewMatrix;
            Matrix translation = Matrix.CreateTranslation(-0.5f * this.terrain.Width, 0,
  0.5f * this.terrain.Width);
            this.effect.World = translation; 


            // TODO: Add your drawing code here
            foreach (EffectPass pass in this.effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                this.terrain.Draw(this.GraphicsDevice);


            }


            base.Draw(gameTime);
        }
    }
}
