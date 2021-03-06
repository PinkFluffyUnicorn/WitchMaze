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
using WitchMaze.ownFunctions;
using WitchMaze.Sound;


namespace WitchMaze
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        ownGameTime ownTime;
        public static SoundCollection sounds;

        static GraphicsDeviceManager graphics;
        static ContentManager content;
        SpriteBatch spriteBatch;

        //Current GameState to run
        GameState gameState;

        EGameState currentGameState;
        EGameState prevGameState;

         public static BasicEffect effect;

        //getter f�r graphicsDeviceManager und Content Manager

        public static GraphicsDeviceManager getGraphics()
        {
            return graphics;
        }

        public static ContentManager getContent()
        {
            return content;
        }
        public static BasicEffect getEffect()
        {
            return effect;
        }

        public Game1()
        {


            Content.RootDirectory = "Content";
            content = Content;

            sounds = new SoundCollection();
            sounds.menuSound.Play();

            ownTime = new ownGameTime();

            currentGameState = EGameState.MainMenu; //tells GameState where so start //change back to main menu
            handleGameState();

            //graphicsDevice = GraphicsDevice;
            graphics = new GraphicsDeviceManager(this);
            //fullscreen
            graphics.IsFullScreen = Settings.isFullscreen();
            //fenstergr��e
            Settings.writeSettings(false, 1280, 1);
            Settings.readSettings();

            graphics.PreferredBackBufferHeight = Settings.getResolutionY();
            graphics.PreferredBackBufferWidth = Settings.getResolutionX();
            


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
            effect = new BasicEffect(graphics.GraphicsDevice);
            effect.EnableDefaultLighting();
            //effect.LightingEnabled = true;
            //effect.AmbientLightColor = new Vector3(0.5f, 0.5f, 0.5f);

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
            ownTime.update(gameTime);
            KeyboardState k = Keyboard.GetState();
            // Allows the game to exit
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            //    this.Exit();
            //if (k.IsKeyDown(Keys.Escape))
            //    this.Exit();

            // TODO: Add your update logic here
            currentGameState = gameState.update(ownTime);
            if (currentGameState != prevGameState)
                handleGameState();

            //gameState.update(ownTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {

            graphics.GraphicsDevice.BlendState = BlendState.Opaque;
            graphics.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            graphics.GraphicsDevice.Clear(Color.DarkGreen);
            //GraphicsDevice.SamplerStates[0] = SamplerState.LinearWrap;

            GraphicsDevice.RasterizerState = RasterizerState.CullNone;


            // TODO: Add your drawing code here
            gameState.Draw();


            base.Draw(gameTime);
        }



        /// <summary>
        /// Handles the GameStateChange, pretty basic
        /// </summary>
        public void handleGameState()
        {
            switch (currentGameState)
            {
                case EGameState.MainMenu:
                    gameState = new GameStates.MainMenu();
                    break;
                case EGameState.InGame:
                    gameState = new GameStates.InGame(gameState.eInGameState ,gameState.getPlayerList());
                    break;
                case EGameState.Credits:
                    gameState = new GameStates.Credits();
                    break;
                case EGameState.Options:
                    gameState = new GameStates.Options();
                    break;
                case EGameState.Exit:
                    this.Exit();
                    break;
                case EGameState.CharacterSelection:
                    gameState = new GameStates.CharacterSelection();
                    break;
                case EGameState.Help:
                    gameState = new GameStates.Help();
                    break;
            }

            gameState.initialize();

            gameState.loadContent();

            prevGameState = currentGameState;

            sounds.gameStateChange.Play(Settings.getSoundVolume(), 0, 0);
        }

    }
}
