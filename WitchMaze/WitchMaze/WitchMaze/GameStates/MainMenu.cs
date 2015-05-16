using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace WitchMaze.GameStates
{
    class MainMenu : GameState
    {   
        
        GraphicsDeviceManager graphics;
        
        int count = 0;
        bool isPressed = false;

        KeyboardState keyboard = Keyboard.GetState();

        Texture2D startGameIsNotPressed;
        SpriteBatch StartGameIsNotPressed;
        
        Texture2D startGameIsPressed;
        SpriteBatch StartGameIsPressed;
        
        Texture2D optionsIsNotPressed;
        SpriteBatch OptionsIsNotPressed;
        
        Texture2D optionsIsPressed;
        SpriteBatch OptionsIsPressed;
        
        Texture2D creditsIsNotPressed;
        SpriteBatch CreditsIsNotPressed;
        
        Texture2D creditsIsPressed;
        SpriteBatch CreditsIsPressed;
        
        Texture2D exitIsNotPressed;
        SpriteBatch ExitIsNotPressed;
        
        Texture2D exitIsPressed;
        SpriteBatch ExitIsPressed;
          
      
        public void initialize()
        {
    
        
        }

        public void loadContent()
        {
            OptionsIsNotPressed = new SpriteBatch(GraphicsDevice);
            optionsIsNotPressed = Content.Load<Texture2D>("options");

            OptionsIsPressed = new SpriteBatch(GraphicsDevice);
            optionsIsPressed = Content.Load<Texture2D>("optionsIsPressed");

            ExitIsNotPressed = new SpriteBatch(GraphicsDevice);
            exitIsNotPressed = Content.Load<Texture2D>("exit");

            ExitIsPressed = new SpriteBatch(GraphicsDevice);
            exitIsPressed = Content.Load<Texture2D>("exitIsPressed");

            CreditsIsNotPressed = new SpriteBatch(GraphicsDevice);
            creditsIsNotPressed = Microsoft.Xna.Framework.Content.Load<Texture2D>("credits");

            CreditsIsPressed = new SpriteBatch(GraphicsDevice);
            creditsIsPressed = Content.Load<Texture2D>("creditsIsPressed");

            StartGameIsNotPressed = new SpriteBatch(GraphicsDevice);
            startGameIsNotPressed = Content.Load<Texture2D>("startGame");

            StartGameIsPressed = new SpriteBatch(GraphicsDevice);
            startGameIsPressed = Content.Load<Texture2D>("startGameIsPressed");
            
          
            
        }

        public void unloadContent()
        {
        }

        public EGameState update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            keyboard = Keyboard.GetState();
            //Input
            if ((keyboard.IsKeyDown(Keys.S) || keyboard.IsKeyDown(Keys.Down)) && isPressed == false)
            {
                count++;
                isPressed = true;
            }
            if ((keyboard.IsKeyDown(Keys.W) || keyboard.IsKeyDown(Keys.Up)) && isPressed == false)
            {
                count += 2;
                isPressed = true;
            }
            //switch the GameState
            if ((keyboard.IsKeyDown(Keys.Space) || keyboard.IsKeyDown(Keys.Enter)) && count % 3 == 0)
                return EGameState.InGame;
            if ((keyboard.IsKeyDown(Keys.Space) || keyboard.IsKeyDown(Keys.Enter)) && count % 3 == 1)
                return EGameState.Options;
            if ((keyboard.IsKeyDown(Keys.Space) || keyboard.IsKeyDown(Keys.Enter)) && count % 3 == 2)
                return EGameState.Exit;
            else
                return EGameState.MainMenu;
        }

        public void Draw(Microsoft.Xna.Framework.GameTime gameTime, Microsoft.Xna.Framework.GraphicsDeviceManager graphics)
        {
            graphics.GraphicsDevice.Clear(Color.Black);


            
            if (!keyboard.IsKeyDown(Keys.W) && !keyboard.IsKeyDown(Keys.S) && !keyboard.IsKeyDown(Keys.Up) && !keyboard.IsKeyDown(Keys.Down))
                isPressed = false;
        }
    }
}
