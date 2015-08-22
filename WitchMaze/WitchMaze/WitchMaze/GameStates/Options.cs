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
        KeyboardState keyboard = Keyboard.GetState();
        int count = 0;
        bool isPressed = false;

        float distY = 96;//die abstände zwischen den Texturen in y-richtung ist 96 bei 1080p, ergibt sich aus button höhe und so...
        float offset = 10;//offset zwischen Icons und Switches

        Vector2 resolutionIconPosition = new Vector2(50, 50);

        Icon resolutionIcon, fullscreenIcon;
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

                resolutionIcon = new Icon(resolutionIconPosition, "Textures/option/Resolution");
                fullscreenIcon = new Icon(new Vector2(50, (resolutionIcon.getPosition().Y + resolutionIcon.getHeight()) + distY), "Textures/option/Fullscreen");

                String[] resolutions = { "Textures/option/1080p", "Textures/option/720p" };
                String[] fullscreenmode = { "Textures/option/offButton", "Textures/option/onButton" };

                resolutionLR = new LeftRightSwitch(new Vector2(resolutionIcon.getPosition().X + resolutionIcon.getWidth() + offset, resolutionIcon.getPosition().Y), resolutions);
                fullscreenLR = new LeftRightSwitch(new Vector2(fullscreenIcon.getPosition().X + fullscreenIcon.getWidth() + offset, fullscreenIcon.getPosition().Y), fullscreenmode);
                
            }
        }

        public override void unloadContent() 
        {

        }

        public override EGameState update(GameTime gameTime) 
        {
            keyboard = Keyboard.GetState();
            if(keyboard.IsKeyDown(Keys.Escape))
                return EGameState.MainMenu;
            else 
                return EGameState.Options; 

        }

        public void updateOptions()
        {
            //resolutionLR.setSelected();

 
        }

        public override void Draw(GameTime gameTime) 
        {
            Game1.getGraphics().GraphicsDevice.BlendState = BlendState.Opaque;
            Game1.getGraphics().GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            Game1.getGraphics().GraphicsDevice.Clear(Color.DarkGreen);

            resolutionIcon.draw();
            resolutionLR.draw();

            fullscreenIcon.draw();
            fullscreenLR.draw();


        }

    }
}
