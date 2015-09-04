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
using WitchMaze.ownFunctions;

namespace WitchMaze.GameStates
{
    class MainMenu : GameState
    {
        //GraphicsDeviceManager graphics;
        //GraphicsDevice graphicsDevice;
             
        int count;
        bool isPressed = true;

        KeyboardState keyboard = Keyboard.GetState();
        GamePadState gamePad = GamePad.GetState(PlayerIndex.One);
        Icon titel;

        Button start;
        Button option;
        Button help;
        Button credits;
        Button exit;


        public override void initialize()
        {

        }

        public override void loadContent()
        {
            
            if (Game1.getGraphics() != null)
            {
                GraphicsDevice graphicsDevice = Game1.getGraphics().GraphicsDevice;

                float distY = 105 * Settings.getInterfaceScale();
                float distX = 400 * Settings.getInterfaceScale();//float distX = 822.5f* Settings.getInterfaceScale();
                start = new Button(new Vector2(distX, distY), "Textures/mainmenu/startGame", "Textures/mainmenu/startGameIsPressed");
                help = new Button(new Vector2(distX, start.getPosition().Y + start.getHeight() + distY), "Textures/mainmenu/help", "Textures/mainmenu/helpIsPressed");
                option = new Button(new Vector2(distX, help.getPosition().Y + help.getHeight() + distY), "Textures/mainmenu/options", "Textures/mainmenu/optionsIsPressed");
                credits = new Button(new Vector2(distX, option.getPosition().Y + option.getHeight() + distY), "Textures/mainmenu/credits", "Textures/mainmenu/creditsIsPressed");
                exit = new Button(new Vector2(distX, credits.getPosition().Y + credits.getHeight() + distY), "Textures/mainmenu/exit", "Textures/mainmenu/exitIsPressed");

                titel = new Icon(new Vector2(0.5f* start.getPosition().X + start.getWidth() + distX, start.getPosition().Y + start.getHeight() + distY), "Textures/mainmenu/titel");

                count = 0;
                start.setSelected();
                help.setNotSelected();
                option.setNotSelected();
                credits.setNotSelected();
                exit.setNotSelected();
            }
        }

        public override void unloadContent()
        {
            
        }

        public override EGameState update(ownGameTime gameTime)
        {
            keyboard = Keyboard.GetState();
            if(GamePad.GetState(PlayerIndex.One).IsConnected)
                gamePad = GamePad.GetState(PlayerIndex.One);
            else if (GamePad.GetState(PlayerIndex.Two).IsConnected)
                gamePad = GamePad.GetState(PlayerIndex.Two);
            else if (GamePad.GetState(PlayerIndex.Three).IsConnected)
                gamePad = GamePad.GetState(PlayerIndex.Three);
            else if (GamePad.GetState(PlayerIndex.Four).IsConnected)
                gamePad = GamePad.GetState(PlayerIndex.Four);

            if (!keyboard.IsKeyDown(Keys.W) 
                && !keyboard.IsKeyDown(Keys.S) 
                && !keyboard.IsKeyDown(Keys.Up) 
                && !keyboard.IsKeyDown(Keys.Down) 
                && !keyboard.IsKeyDown(Keys.Enter)
                && !gamePad.IsButtonDown(Buttons.DPadUp) 
                && !gamePad.IsButtonDown(Buttons.DPadDown) 
                && !gamePad.IsButtonDown(Buttons.A))
                isPressed = false;
            //Input
            if ((keyboard.IsKeyDown(Keys.S) || keyboard.IsKeyDown(Keys.Down) || gamePad.IsButtonDown(Buttons.DPadDown)) && isPressed == false)
            {
                count++;
                count = count % 5;
                isPressed = true;
            }
            if ((keyboard.IsKeyDown(Keys.W) || keyboard.IsKeyDown(Keys.Up) || gamePad.IsButtonDown(Buttons.DPadUp)) && isPressed == false)
            {
                    count += 4;
                    count = count % 5;
                    isPressed = true;
            }
            //update Buttons
            if (count == 0)
            {
                start.setSelectedKlicked();
                help.setNotSelected();
                option.setNotSelected();
                credits.setNotSelected();
                exit.setNotSelected();
            }

            if (count == 1)
            {
                start.setNotSelected();
                help.setSelectedKlicked();
                option.setNotSelected();
                credits.setNotSelected();
                exit.setNotSelected();
            }

            if (count == 2)
            {
                start.setNotSelected();
                help.setNotSelected();
                option.setSelectedKlicked();
                credits.setNotSelected();
                exit.setNotSelected();
            }

            if (count == 3)
            {
                start.setNotSelected();
                help.setNotSelected();
                option.setNotSelected();
                credits.setSelectedKlicked();
                exit.setNotSelected();
            }


            if (count == 4)
            {
                start.setNotSelected();
                help.setNotSelected();
                option.setNotSelected();
                credits.setNotSelected();
                exit.setSelectedKlicked();
            }

            //switch the GameState
            if ((keyboard.IsKeyDown(Keys.Enter) || gamePad.IsButtonDown(Buttons.A)) && count == 0 && !isPressed)
                return EGameState.CharacterSelection;
            if ((keyboard.IsKeyDown(Keys.Enter) || gamePad.IsButtonDown(Buttons.A)) && count == 1 && !isPressed)
                return EGameState.Help;
            if ((keyboard.IsKeyDown(Keys.Enter) || gamePad.IsButtonDown(Buttons.A)) && count == 2 && !isPressed)
                return EGameState.Options;
            if ((keyboard.IsKeyDown(Keys.Enter) || gamePad.IsButtonDown(Buttons.A)) && count == 3 && !isPressed)
                return EGameState.Credits;
            if ((keyboard.IsKeyDown(Keys.Enter) || gamePad.IsButtonDown(Buttons.A)) && count == 4 && !isPressed)
                return EGameState.Exit;
            else
                return EGameState.MainMenu;
        }

        public override void Draw()
        {
            Game1.getGraphics().GraphicsDevice.BlendState = BlendState.Opaque;
            Game1.getGraphics().GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            Game1.getGraphics().GraphicsDevice.Clear(Color.DarkGreen);

            // Draw the sprite.
            //spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            //spriteBatch.Draw(myTexture, spritePosition, Color.White);

            start.draw();
            help.draw();
            option.draw();
            credits.draw();
            exit.draw();
            titel.draw();
            
        }
    }
}
