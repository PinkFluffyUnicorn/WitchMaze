using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using WitchMaze.InterfaceObjects;


namespace WitchMaze.GameStates
{
    class Options : GameState 
    {
        int count = 0;
        bool isPressed = false;
        KeyboardState keyboard = Keyboard.GetState();

        float distY = 96;//die abstände zwischen den Texturen in y-richtung ist 96 bei 1080p, ergibt sich aus button höhe und so...
        float offset = 10;//offset zwischen Icons und Switches

        Vector2 optionsTitelPosition = new Vector2(710, 100);
        Icon optionsTitel;
        Button resolutionButton, fullscreenButton;
        LeftRightSwitch resolutionLR, fullscreenLR;
        
        public override void initialize()
        {

        }

        public override void loadContent() 
        {
            if (Game1.getGraphics() != null)
            {
                distY *= Settings.getInterfaceScale();
                offset *= Settings.getInterfaceScale();
                optionsTitel = new Icon(optionsTitelPosition * Settings.getInterfaceScale(), "Textures/option/optionsTitel");
                resolutionButton = new Button(new Vector2(560 * Settings.getInterfaceScale(), (optionsTitel.getPosition().Y + optionsTitel.getHeight()) + distY), "Textures/option/Resolution", "Textures/option/ResolutionSelected");
                fullscreenButton = new Button(new Vector2(560 * Settings.getInterfaceScale(), (resolutionButton.getPosition().Y + resolutionButton.getHeight()) + distY), "Textures/option/Fullscreen", "Textures/option/FullscreenSelected");

                String[] resolutions = { "Textures/option/1080p", "Textures/option/1366p", "Textures/option/720p", "Textures/option/1024p" };
                String[] fullscreenmode = { "Textures/option/offButton", "Textures/option/onButton" };

                resolutionLR = new LeftRightSwitch(new Vector2(resolutionButton.getPosition().X + resolutionButton.getWidth() + offset, resolutionButton.getPosition().Y), resolutions);
                fullscreenLR = new LeftRightSwitch(new Vector2(fullscreenButton.getPosition().X + fullscreenButton.getWidth() + offset, fullscreenButton.getPosition().Y), fullscreenmode);
                
            }
        }

        public override void unloadContent() 
        {

        }

        public override EGameState update(GameTime gameTime) 
        {
            keyboard = Keyboard.GetState();
            if (!keyboard.IsKeyDown(Keys.Left) && !keyboard.IsKeyDown(Keys.Right) && !keyboard.IsKeyDown(Keys.Down) && !keyboard.IsKeyDown(Keys.Up) && !keyboard.IsKeyDown(Keys.Enter))
                isPressed = false;

            //Input
            if ((keyboard.IsKeyDown(Keys.Down)) && isPressed == false)
            {
                count++;
                count = count % 2;
                isPressed = true;
            }
            if ((keyboard.IsKeyDown(Keys.Up)) && isPressed == false)
            {
                count += 1;
                count = count % 2;
                isPressed = true;
            }

            //update Buttons
            if (count == 0)
            {
                resolutionButton.setSelected();
                resolutionLR.setSelected();
                fullscreenButton.setNotSelected();
            }

            if (count == 1)
            {
                resolutionButton.setNotSelected();
                fullscreenButton.setSelected();
                fullscreenLR.setSelected();
            }


            if (resolutionLR.isSelected())
            {
                if (keyboard.IsKeyDown(Keys.Right) && resolutionButton.isSelected() && isPressed == false)
                {
                    resolutionLR.switchRight();
                    isPressed = true;
                }

                if (keyboard.IsKeyDown(Keys.Left) && resolutionButton.isSelected() && isPressed == false)
                {
                    resolutionLR.switchLeft();
                    isPressed = true;
                }

                if (resolutionLR.getDisplayedIndex() == 0 && resolutionButton.isSelected() && keyboard.IsKeyDown(Keys.Enter) && isPressed == false)
                {
                    Settings.setResolutionX(1920);
                    Game1.getGraphics().PreferredBackBufferHeight = Settings.getResolutionY();
                    Game1.getGraphics().PreferredBackBufferWidth = Settings.getResolutionX();
                    isPressed = true;
                }
                if (resolutionLR.getDisplayedIndex() == 1 && resolutionButton.isSelected() && keyboard.IsKeyDown(Keys.Enter) && isPressed == false)
                {
                    Settings.setResolutionX(1366);
                    Game1.getGraphics().PreferredBackBufferHeight = Settings.getResolutionY();
                    Game1.getGraphics().PreferredBackBufferWidth = Settings.getResolutionX();
                    isPressed = true;
                }
                if (resolutionLR.getDisplayedIndex() == 2 && resolutionButton.isSelected() && keyboard.IsKeyDown(Keys.Enter) && isPressed == false)
                {
                    Settings.setResolutionX(1280);
                    Game1.getGraphics().PreferredBackBufferHeight = Settings.getResolutionY();
                    Game1.getGraphics().PreferredBackBufferWidth = Settings.getResolutionX();
                    isPressed = true;
                }
                if (resolutionLR.getDisplayedIndex() == 3 && resolutionButton.isSelected() && keyboard.IsKeyDown(Keys.Enter) && isPressed == false)
                {
                    Settings.setResolutionX(1024);
                    Game1.getGraphics().PreferredBackBufferHeight = Settings.getResolutionY();
                    Game1.getGraphics().PreferredBackBufferWidth = Settings.getResolutionX();
                    isPressed = true;
                }

            }

            if (fullscreenLR.isSelected())
            {
                if (keyboard.IsKeyDown(Keys.Right) && fullscreenButton.isSelected() && fullscreenLR.getDisplayedIndex() == 0 && isPressed == false)
                {
                    fullscreenLR.switchRight();
                    Game1.getGraphics().IsFullScreen = true;
                    Game1.getGraphics().ApplyChanges();
                    isPressed = true;
                }

                if (keyboard.IsKeyDown(Keys.Left) && fullscreenButton.isSelected() && fullscreenLR.getDisplayedIndex() == 0 && isPressed == false)
                {
                    fullscreenLR.switchLeft();
                    Game1.getGraphics().IsFullScreen = true;
                    Game1.getGraphics().ApplyChanges();
                    isPressed = true;
                }

                if (keyboard.IsKeyDown(Keys.Right) && fullscreenButton.isSelected() && fullscreenLR.getDisplayedIndex() == 1 && isPressed == false)
                {
                    fullscreenLR.switchRight();
                    Game1.getGraphics().IsFullScreen = false;
                    Game1.getGraphics().ApplyChanges();
                    isPressed = true;
                }

                if (keyboard.IsKeyDown(Keys.Left) && fullscreenButton.isSelected() && fullscreenLR.getDisplayedIndex() == 1 && isPressed == false)
                {
                    fullscreenLR.switchLeft();
                    Game1.getGraphics().IsFullScreen = false;
                    Game1.getGraphics().ApplyChanges();
                    isPressed = true;
                }

                //if (fullscreenLR.getDisplayedIndex() == 0 && fullscreenButton.isSelected())
                //{
                //    Game1.getGraphics().IsFullScreen = false;
                //    Game1.getGraphics().ApplyChanges();
                //}

                //if (fullscreenLR.getDisplayedIndex() == 1 && fullscreenButton.isSelected())
                //{
                                               
                //}
            }
 

            if (keyboard.IsKeyDown(Keys.Escape))
                return EGameState.MainMenu;
            else
                return EGameState.Options;
        }


        public override void Draw(GameTime gameTime) 
        {
            Game1.getGraphics().GraphicsDevice.BlendState = BlendState.Opaque;
            Game1.getGraphics().GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            Game1.getGraphics().GraphicsDevice.Clear(Color.DarkGreen);

            optionsTitel.draw();

            resolutionButton.draw();
            if (resolutionButton.isSelected())
            {
                resolutionLR.draw();
            }
            
            fullscreenButton.draw();
            if (fullscreenButton.isSelected())
            {
                fullscreenLR.draw();
            }

        }

    }
}
