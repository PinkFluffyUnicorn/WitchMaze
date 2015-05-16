using Microsoft.Xna.Framework.Graphics;
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
        ContentManager Content;
        //GraphicsDeviceManager graphics;
        GraphicsDevice GraphicsDevice;
                
        int count = 0;
        bool isPressed = false;

        KeyboardState keyboard = Keyboard.GetState();

        Vector2 startPosition = new Vector2(400,50);
        
        Texture2D startGameIsNotPressed;
        SpriteBatch StartGameIsNotPressed;
        
        Texture2D startGameIsPressed;
        SpriteBatch StartGameIsPressed;

        Vector2 optionsPosition = new Vector2(400,200);

        Texture2D optionsIsNotPressed;
        SpriteBatch OptionsIsNotPressed;
        
        Texture2D optionsIsPressed;
        SpriteBatch OptionsIsPressed;

        Vector2 creditsPosition = new Vector2(400,300);

        Texture2D creditsIsNotPressed;
        SpriteBatch CreditsIsNotPressed;
        
        Texture2D creditsIsPressed;
        SpriteBatch CreditsIsPressed;

        Vector2 exitPosition = new Vector2(400,500);

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
            creditsIsNotPressed = Content.Load<Texture2D>("credits");

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
            if ((keyboard.IsKeyDown(Keys.Space) || keyboard.IsKeyDown(Keys.Enter)) && count % 4 == 0)
                return EGameState.InGame;
            if ((keyboard.IsKeyDown(Keys.Space) || keyboard.IsKeyDown(Keys.Enter)) && count % 4 == 1)
                return EGameState.Options;
            if ((keyboard.IsKeyDown(Keys.Space) || keyboard.IsKeyDown(Keys.Enter)) && count % 4 == 2)
                return EGameState.Credits;
            if ((keyboard.IsKeyDown(Keys.Space) || keyboard.IsKeyDown(Keys.Enter)) && count % 4 == 3)
                return EGameState.Exit;
            else
                return EGameState.MainMenu;
        }

        public void Draw(Microsoft.Xna.Framework.GameTime gameTime, GraphicsDevice graphicsDevice)
        {
            graphics.GraphicsDevice.Clear(Color.Black);

            // Draw the sprite.
            //spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            //spriteBatch.Draw(myTexture, spritePosition, Color.White);
            //spriteBatch.End(); 
            if (count % 4 == 0)
            {
                StartGameIsPressed.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
                StartGameIsPressed.Draw(startGameIsPressed, startPosition, Color.White);
                StartGameIsPressed.End();
            }
            else
            {
                StartGameIsNotPressed.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
                StartGameIsNotPressed.Draw(startGameIsNotPressed, startPosition, Color.White);
                StartGameIsNotPressed.End();
            }
            if (count % 4 == 1)
            {
                OptionsIsPressed.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
                OptionsIsPressed.Draw(optionsIsPressed, optionsPosition, Color.White);
                OptionsIsPressed.End();
            }
            else
            {
                OptionsIsNotPressed.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
                OptionsIsNotPressed.Draw(optionsIsNotPressed, optionsPosition, Color.White);
                OptionsIsNotPressed.End();
            }
            if (count % 4 == 2)
            {
                CreditsIsPressed.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
                CreditsIsPressed.Draw(creditsIsPressed, creditsPosition, Color.White);
                CreditsIsPressed.End();
            }
            else
            {
                CreditsIsNotPressed.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
                CreditsIsNotPressed.Draw(creditsIsNotPressed, creditsPosition, Color.White);
                CreditsIsNotPressed.End();
            }
            if (count % 4 == 3)
            {
                ExitIsPressed.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
                ExitIsPressed.Draw(exitIsPressed, exitPosition, Color.White);
                ExitIsPressed.End();
            }
            else
            {
                ExitIsNotPressed.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
                ExitIsNotPressed.Draw(exitIsNotPressed, exitPosition, Color.White);
                ExitIsNotPressed.End();
            }
            
            if (!keyboard.IsKeyDown(Keys.W) && !keyboard.IsKeyDown(Keys.S) && !keyboard.IsKeyDown(Keys.Up) && !keyboard.IsKeyDown(Keys.Down))
                isPressed = false;
        }
    }
}
