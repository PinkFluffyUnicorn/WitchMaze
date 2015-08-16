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
using WitchMaze;
using WitchMaze.InterfaceObjects;

namespace WitchMaze.GameStates
{
    class MainMenu : GameState
    {
        //GraphicsDeviceManager graphics;
        //GraphicsDevice graphicsDevice;
             
        int count = 0;
        bool isPressed = false;

        KeyboardState keyboard = Keyboard.GetState();

        Icon titel;

        Button start;
        Button option;
        Button help;
        Button credits;
        Button exit;

      
        public void initialize()
        {

        }

        public void loadContent()
        {
            
            if (Game1.getGraphics() != null)
            {
                GraphicsDevice graphicsDevice = Game1.getGraphics().GraphicsDevice;

                float distY = 105* Settings.getInterfaceScale();
                float distX = 400 * Settings.getInterfaceScale();//float distX = 822.5f* Settings.getInterfaceScale();
                start = new Button(new Vector2(distX, distY), "Textures/mainmenu/startGame", "Textures/mainmenu/startGameIsPressed");
                help = new Button(new Vector2(distX, start.getPosition().Y + start.getHeight() + distY), "Textures/mainmenu/help", "Textures/mainmenu/helpIsPressed");
                option = new Button(new Vector2(distX, help.getPosition().Y + help.getHeight() + distY), "Textures/mainmenu/options", "Textures/mainmenu/optionsIsPressed");
                credits = new Button(new Vector2(distX, option.getPosition().Y + option.getHeight() + distY), "Textures/mainmenu/credits", "Textures/mainmenu/creditsIsPressed");
                exit = new Button(new Vector2(distX, credits.getPosition().Y + credits.getHeight() + distY), "Textures/mainmenu/exit", "Textures/mainmenu/exitIsPressed");

                titel = new Icon(new Vector2(450, 150), "Textures/mainmenu/titel");
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
                count = count % 5;
                isPressed = true;
            }
            if ((keyboard.IsKeyDown(Keys.W) || keyboard.IsKeyDown(Keys.Up)) && isPressed == false)
            {
                    count += 4;
                    count = count % 5;
                    isPressed = true;
            }
            //update Buttons
            if (count == 0)
            {
                start.setSelected();
                help.setNotSelected();
                option.setNotSelected();
                credits.setNotSelected();
                exit.setNotSelected();
            }

            if (count == 1)
            {
                start.setNotSelected();
                help.setSelected();
                option.setNotSelected();
                credits.setNotSelected();
                exit.setNotSelected();
            }

            if (count == 2)
            {
                start.setNotSelected();
                help.setNotSelected();
                option.setSelected();
                credits.setNotSelected();
                exit.setNotSelected();
            }

            if (count == 3)
            {
                start.setNotSelected();
                help.setNotSelected();
                option.setNotSelected();
                credits.setSelected();
                exit.setNotSelected();
            }


            if (count == 4)
            {
                start.setNotSelected();
                help.setNotSelected();
                option.setNotSelected();
                credits.setNotSelected();
                exit.setSelected();
            }

            //switch the GameState
            if ((keyboard.IsKeyDown(Keys.Space) || keyboard.IsKeyDown(Keys.Enter)) && count == 0)
                return EGameState.InGame;
            if ((keyboard.IsKeyDown(Keys.Space) || keyboard.IsKeyDown(Keys.Enter)) && count == 2)
                return EGameState.Options;
            if ((keyboard.IsKeyDown(Keys.Space) || keyboard.IsKeyDown(Keys.Enter)) && count == 3)
                return EGameState.Credits;
            if ((keyboard.IsKeyDown(Keys.Space) || keyboard.IsKeyDown(Keys.Enter)) && count == 4)
                return EGameState.Exit;
            else
                return EGameState.MainMenu;
        }

        public void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            Game1.getGraphics().GraphicsDevice.BlendState = BlendState.Opaque;
            Game1.getGraphics().GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            Game1.getGraphics().GraphicsDevice.Clear(Color.DarkGreen);

            // Draw the sprite.
            //spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            //spriteBatch.Draw(myTexture, spritePosition, Color.White);

            if (!keyboard.IsKeyDown(Keys.W) && !keyboard.IsKeyDown(Keys.S) && !keyboard.IsKeyDown(Keys.Up) && !keyboard.IsKeyDown(Keys.Down))
                isPressed = false;

            start.draw();
            help.draw();
            option.draw();
            credits.draw();
            exit.draw();
            titel.draw();
            
        }
    }
}
