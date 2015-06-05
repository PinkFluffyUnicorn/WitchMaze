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
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace WitchMaze.GameStates
{
    class MainMenu : GameState
    {
        //GraphicsDeviceManager graphics;
        //GraphicsDevice graphicsDevice;
                
        int count = 0;
        bool isPressed = false;

        KeyboardState keyboard = Keyboard.GetState();

        Vector2 startPosition = new Vector2(250,50);
        
        Texture2D startGameIsNotPressed;
        SpriteBatch sStartGameIsNotPressed;
        
        Texture2D startGameIsPressed;
        SpriteBatch sStartGameIsPressed;

        Vector2 optionsPosition = new Vector2(250,150);

        Texture2D optionsIsNotPressed;
        SpriteBatch sOptionsIsNotPressed;
        
        Texture2D optionsIsPressed;
        SpriteBatch sOptionsIsPressed;

        Vector2 creditsPosition = new Vector2(250,250);

        Texture2D creditsIsNotPressed;
        SpriteBatch sCreditsIsNotPressed;
        
        Texture2D creditsIsPressed;
        SpriteBatch sCreditsIsPressed;

        Vector2 exitPosition = new Vector2(250,350);
        
        Texture2D exitIsNotPressed;
        SpriteBatch sExitIsNotPressed;
        
        Texture2D exitIsPressed;
        SpriteBatch sExitIsPressed;

      
        public void initialize()
        {

        }

        public void loadContent(ContentManager content, GraphicsDeviceManager graphics)
        {
            if (graphics != null)
            {
                GraphicsDevice graphicsDevice = graphics.GraphicsDevice;

                sOptionsIsNotPressed = new SpriteBatch(graphicsDevice);
                optionsIsNotPressed = content.Load<Texture2D>("options");

                sOptionsIsPressed = new SpriteBatch(graphicsDevice);
                optionsIsPressed = content.Load<Texture2D>("optionsIsPressed");

                sExitIsNotPressed = new SpriteBatch(graphicsDevice);
                exitIsNotPressed = content.Load<Texture2D>("exit");

                sExitIsPressed = new SpriteBatch(graphicsDevice);
                exitIsPressed = content.Load<Texture2D>("exitIsPressed");

                sCreditsIsNotPressed = new SpriteBatch(graphicsDevice);
                creditsIsNotPressed = content.Load<Texture2D>("credits");

                sCreditsIsPressed = new SpriteBatch(graphicsDevice);
                creditsIsPressed = content.Load<Texture2D>("creditsIsPressed");

                sStartGameIsNotPressed = new SpriteBatch(graphicsDevice);
                startGameIsNotPressed = content.Load<Texture2D>("startGame");

                sStartGameIsPressed = new SpriteBatch(graphicsDevice);
                startGameIsPressed = content.Load<Texture2D>("startGameIsPressed");
            }
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
                    count += 3;
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

        public void Draw(Microsoft.Xna.Framework.GameTime gameTime, GraphicsDeviceManager graphics)
        {
            graphics.GraphicsDevice.BlendState = BlendState.Opaque;
            graphics.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            graphics.GraphicsDevice.Clear(Color.Black);

            // Draw the sprite.
            //spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            //spriteBatch.Draw(myTexture, spritePosition, Color.White);

                if (count % 4 == 0)
                {
                    sStartGameIsPressed.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
                    sStartGameIsPressed.Draw(startGameIsPressed, startPosition, Color.White);
                    sStartGameIsPressed.End();
                }
                else
                {
                    sStartGameIsNotPressed.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
                    sStartGameIsNotPressed.Draw(startGameIsNotPressed, startPosition, Color.White);
                    sStartGameIsNotPressed.End();
                }
                if (count % 4 == 1)
                {
                    sOptionsIsPressed.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
                    sOptionsIsPressed.Draw(optionsIsPressed, optionsPosition, Color.White);
                    sOptionsIsPressed.End();
                }
                else
                {
                    sOptionsIsNotPressed.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
                    sOptionsIsNotPressed.Draw(optionsIsNotPressed, optionsPosition, Color.White);
                    sOptionsIsNotPressed.End();
                }
                if (count % 4 == 2)
                {
                    sCreditsIsPressed.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
                    sCreditsIsPressed.Draw(creditsIsPressed, creditsPosition, Color.White);
                    sCreditsIsPressed.End();
                }
                else
                {
                    sCreditsIsNotPressed.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
                    sCreditsIsNotPressed.Draw(creditsIsNotPressed, creditsPosition, Color.White);
                    sCreditsIsNotPressed.End();
                }
                if (count % 4 == 3)
                {
                    sExitIsPressed.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
                    sExitIsPressed.Draw(exitIsPressed, exitPosition, Color.White);
                    sExitIsPressed.End();
                }
                else
                {
                    sExitIsNotPressed.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
                    sExitIsNotPressed.Draw(exitIsNotPressed, exitPosition, Color.White);
                    sExitIsNotPressed.End();
                }
            

            if (!keyboard.IsKeyDown(Keys.W) && !keyboard.IsKeyDown(Keys.S) && !keyboard.IsKeyDown(Keys.Up) && !keyboard.IsKeyDown(Keys.Down))
                isPressed = false;            
        }
    }
}
