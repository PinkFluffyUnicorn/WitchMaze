using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace WitchMaze.GameStates
{
    class MainMenu : GameState
    {
        int count = 0;
        bool isPressed = false;

        KeyboardState keyboard = Keyboard.GetState();

        Texture2D optionsIsNotPressed;
        SpriteBatch OptionsIsNotPressed;
       
        
        public void initialize()
        {
    
        
        }

        public void loadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            optionsIsNotPressed = Content.Load<Texture2D>("options");
            
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

        public void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {

           /* graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

            // Draw the sprite.
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            spriteBatch.Draw(myTexture, spritePosition, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);

            //hier werden die Fenster gezeichnet
            if (count % 3 == 0)
                window.Draw(StartIsPressed);
            else
                window.Draw(Start);
            if (count % 3 == 1)
                window.Draw(CreditsIsPressed);
            else
                window.Draw(Credits);
            if (count % 3 == 2)
                window.Draw(ExitIsPressed);
            else
                window.Draw(Exit);*/


            if (!keyboard.IsKeyDown(Keys.W) && !keyboard.IsKeyDown(Keys.S) && !keyboard.IsKeyDown(Keys.Up) && !keyboard.IsKeyDown(Keys.Down))
                isPressed = false;
        }
    }
}
