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

namespace WitchMaze
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Current GameState to run
        GameState gameState;

        EGameState currentGameState;
        EGameState prevGameState;

        GraphicsDevice graphicsDevice;


        public Game1()
        {
            currentGameState = EGameState.InGame; //tells GameState where so start //change back to main menu
            handleGameState();
            //graphicsDevice = GraphicsDevice;
            graphics = new GraphicsDeviceManager(this);
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
            gameState.initialize();

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

            // TODO: use this.Content to load your game content here
            gameState.loadContent();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            gameState.unloadContent();
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

            // TODO: Add your update logic here
            currentGameState = gameState.update(gameTime);

            if (currentGameState != prevGameState)
                handleGameState();

            gameState.update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            gameState.Draw(gameTime, graphicsDevice);


            base.Draw(gameTime);
        }



        /// <summary>
        /// Handles the GameStateChange, pretty basic
        /// </summary>
        public void handleGameState()
        {
            Console.Out.Write("GameState change! \n");

            switch (currentGameState)
            {
                case EGameState.MainMenu:
                    gameState = new GameStates.MainMenu();
                    break;
                case EGameState.InGame:
                    gameState = new GameStates.InGame();
                    break;
                case EGameState.Credits:
                    gameState = new GameStates.Credits();
                    break;
                case EGameState.Options:
                    gameState = new GameStates.Options();
                    break;
            }

            gameState.loadContent();

            gameState.initialize();

            prevGameState = currentGameState;
        }

    }
}
