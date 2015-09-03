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
using WitchMaze.ownFunctions;


namespace WitchMaze.GameStates
{
    class Options : GameState 
    {
        int count = 0;
        bool isPressed = true;
        KeyboardState keyboard = Keyboard.GetState();

        float distY = 96;//die abstände zwischen den Texturen in y-richtung ist 96 bei 1080p, ergibt sich aus button höhe und so...
        float offset = 10;//offset zwischen Icons und Switches

        Vector2 optionsTitelPosition = new Vector2(710, 100);
        Icon optionsTitel;
        Button resolutionButton, fullscreenButton, volumeButton;
        LeftRightSwitch resolutionLR, fullscreenLR, volumeLR;

        int resolutionX = Settings.getResolutionX();
        bool fullScreen = Settings.isFullscreen();
        float Volume = Settings.getSoundVolume();
        
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
                fullscreenButton = new Button(new Vector2(560 * Settings.getInterfaceScale(), (optionsTitel.getPosition().Y + optionsTitel.getHeight()) + distY),  "Textures/option/Fullscreen", "Textures/option/FullscreenSelected");
                resolutionButton = new Button(new Vector2(560 * Settings.getInterfaceScale(), (fullscreenButton.getPosition().Y + fullscreenButton.getHeight()) + distY),"Textures/option/Resolution", "Textures/option/ResolutionSelected");
                volumeButton = new Button(new Vector2(560 * Settings.getInterfaceScale(), (resolutionButton.getPosition().Y + resolutionButton.getHeight()) + distY), "Textures/option/Volume", "Textures/option/VolumeSelected");
                String[] resolutions = { "Textures/option/720p", "Textures/option/1366p", "Textures/option/1080p" };//, "Textures/option/1024p" 
                String[] fullscreenmode = { "Textures/option/offButton", "Textures/option/onButton" };
                String[] volumtmodes = { "Textures/option/100Prozent", "Textures/option/90Prozent", "Textures/option/80Prozent", "Textures/option/70Prozent", "Textures/option/60Prozent", "Textures/option/50Prozent", "Textures/option/40Prozent", "Textures/option/30Prozent", "Textures/option/20Prozent", "Textures/option/10Prozent", "Textures/option/offButton" };

                resolutionLR = new LeftRightSwitch(new Vector2(resolutionButton.getPosition().X + resolutionButton.getWidth() + offset, resolutionButton.getPosition().Y), resolutions);
                fullscreenLR = new LeftRightSwitch(new Vector2(fullscreenButton.getPosition().X + fullscreenButton.getWidth() + offset, fullscreenButton.getPosition().Y), fullscreenmode);
                volumeLR = new LeftRightSwitch(new Vector2(volumeButton.getPosition().X + volumeButton.getWidth() + offset, volumeButton.getPosition().Y), volumtmodes);

                //always show the actual settings selected first
                if (Settings.isFullscreen())
                    fullscreenLR.switchLeft();
                if (Settings.getResolutionX() == 1366)
                    resolutionLR.switchRight();
                if (Settings.getResolutionX() == 1920)
                    resolutionLR.switchLeft();
                int h = 10 - (int)(Settings.getSoundVolume() * 10);
                while (volumeLR.getDisplayedIndex() != h)
                    volumeLR.switchLeft();

                //initially set the buttons
                volumeButton.setNotSelected();
                volumeLR.setNotSelected();
                resolutionButton.setNotSelected();
                resolutionLR.setNotSelected();
                fullscreenButton.setSelected();
                fullscreenLR.setSelected();
                
            }
        }

        public override void unloadContent() 
        {

        }

        public override EGameState update(ownGameTime gameTime) 
        {
            keyboard = Keyboard.GetState();
            if (!keyboard.IsKeyDown(Keys.Left) && !keyboard.IsKeyDown(Keys.Right) && !keyboard.IsKeyDown(Keys.Down) && !keyboard.IsKeyDown(Keys.Up) && !keyboard.IsKeyDown(Keys.Enter))
                isPressed = false;

            //Input
            if ((keyboard.IsKeyDown(Keys.Down)) && isPressed == false)
            {
                count++;
                count = count % 3;
                isPressed = true;
            }
            if ((keyboard.IsKeyDown(Keys.Up)) && isPressed == false)
            {
                count += 2;
                count = count % 3;
                isPressed = true;
            }

            //update Buttons
            if (count == 0)
            {
                volumeButton.setNotSelected();
                volumeLR.setNotSelected();
                resolutionButton.setNotSelected();
                resolutionLR.setNotSelected();
                fullscreenButton.setSelectedKlicked();
                fullscreenLR.setSelected();
            }

            if (count == 1)
            {
                volumeButton.setNotSelected();
                volumeLR.setNotSelected();
                resolutionButton.setSelectedKlicked();
                resolutionLR.setSelected();
                fullscreenButton.setNotSelected();
                fullscreenLR.setNotSelected();
            }

            if (count == 2)
            {

                resolutionButton.setNotSelected();
                resolutionLR.setNotSelected();
                fullscreenButton.setNotSelected();
                fullscreenLR.setNotSelected();
                volumeButton.setSelectedKlicked();
                volumeLR.setSelected();
            }

            if (resolutionButton.isSelected())//resolutionLR.isSelected()
            {
                if (keyboard.IsKeyDown(Keys.Right) && isPressed == false)
                {
                    resolutionLR.switchRightKlicked();
                    isPressed = true;
                }

                if (keyboard.IsKeyDown(Keys.Left) && isPressed == false)
                {
                    resolutionLR.switchLeftKlicked();
                    isPressed = true;
                }

                if (resolutionLR.getDisplayedIndex() == 2)
                {
                    resolutionX = 1920;
                }
                if (resolutionLR.getDisplayedIndex() == 1)
                {
                    resolutionX = 1366;
                }
                if (resolutionLR.getDisplayedIndex() == 0)
                {
                    resolutionX = 1280;
                }

            }

            if (fullscreenButton.isSelected())//fullscreenLR.isSelected()
            {
                if (keyboard.IsKeyDown(Keys.Right) && fullscreenLR.getDisplayedIndex() == 0 && isPressed == false)
                {
                    fullscreenLR.switchRightKlicked();
                    fullScreen = true;
                    isPressed = true;
                }
                if (keyboard.IsKeyDown(Keys.Left) && fullscreenLR.getDisplayedIndex() == 0 && isPressed == false)
                {
                    fullscreenLR.switchLeftKlicked();
                    fullScreen = true;
                    isPressed = true;
                }

                if (keyboard.IsKeyDown(Keys.Right) && fullscreenLR.getDisplayedIndex() == 1 && isPressed == false)
                {
                    fullscreenLR.switchRightKlicked();
                    fullScreen = false;
                    isPressed = true;
                }
                if (keyboard.IsKeyDown(Keys.Left) && fullscreenLR.getDisplayedIndex() == 1 && isPressed == false)
                {
                    fullscreenLR.switchLeftKlicked();
                    fullScreen = false;
                    isPressed = true;
                }
            }

            if (volumeButton.isSelected())
            {
                if (keyboard.IsKeyDown(Keys.Right) && isPressed == false)
                {
                    volumeLR.switchRightKlicked();
                    Volume = (10 - (float)volumeLR.getDisplayedIndex()) / 10;
                    isPressed = true;
                }
                if (keyboard.IsKeyDown(Keys.Left) && isPressed == false)
                {
                    volumeLR.switchLeftKlicked();
                    Volume = (10 - (float)volumeLR.getDisplayedIndex()) / 10;
                    isPressed = true;
                }
                //Console.WriteLine(volumeLR.getDisplayedIndex());
            }

            if (keyboard.IsKeyDown(Keys.Enter) && !isPressed)
            {
                apply();
                return EGameState.MainMenu;
            }
                

            if (keyboard.IsKeyDown(Keys.Escape))
                return EGameState.MainMenu;
            else
                return EGameState.Options;
        }

        public void apply()
        {
            if (fullScreen)
                Settings.setFullscreen( true);
            else
                Settings.setFullscreen(false);

            Settings.setResolutionX(resolutionX);

            Settings.setSoundVolume(Volume);
            initialize();
            loadContent();
        }


        public override void Draw() 
        {
            Game1.getGraphics().GraphicsDevice.BlendState = BlendState.Opaque;
            Game1.getGraphics().GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            Game1.getGraphics().GraphicsDevice.Clear(Color.DarkGreen);

            optionsTitel.draw();

            resolutionButton.draw();
                resolutionLR.draw();

            
            fullscreenButton.draw();
                fullscreenLR.draw();


            volumeButton.draw();
                volumeLR.draw();


        }

    }
}
