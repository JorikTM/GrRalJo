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

        private Camera camera; 

        private BasicEffect effect;
        private VertexPositionColor[] vertices;

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

        private void setupVertices()
        {
            this.vertices = new VertexPositionColor[3];
            this.vertices[0].Position = new Vector3(0f, 0f, 0f); 
            this.vertices[0].Color = Color.Red;
            this.vertices[1].Position = new Vector3(10f, 10f, 0f); 
            this.vertices[1].Color = Color.Yellow;
            this.vertices[2].Position = new Vector3(10f, 0f, -5f);
            this.vertices[2].Color = Color.Green;
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
            spriteBatch = new SpriteBatch(GraphicsDevice);
            this.effect = new BasicEffect(this.GraphicsDevice);
            this.setupVertices();
            this.effect.VertexColorEnabled = true;
            this.camera = new Camera(new Vector3(0, 0, -50), new Vector3(0, 0, 0),
 new Vector3(0, 1, 0)); 


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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            KeyboardState testkeyboardstate = Keyboard.GetState();
            
            if(testkeyboardstate.IsKeyDown(Keys.Escape))
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
                CullMode = CullMode.None
            }; 


            GraphicsDevice.Clear(Color.DarkSlateBlue);

            this.effect.Projection = this.camera.ProjectionMatrix;
            this.effect.View = this.camera.ViewMatrix;
            this.effect.World = Matrix.Identity; 


            // TODO: Add your drawing code here
            foreach(EffectPass pass in this.effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                this.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, this.vertices, 0, 1,
 VertexPositionColor.VertexDeclaration); 


            }


            base.Draw(gameTime);
        }
    }
}
