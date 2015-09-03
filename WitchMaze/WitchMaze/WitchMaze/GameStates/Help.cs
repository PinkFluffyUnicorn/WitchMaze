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
    class Help : GameState 
    {
        
        KeyboardState keyboard = Keyboard.GetState();
        Icon xboxControl, numpadControl, wasdControl;

        public override void initialize()
        {

        }

        public override void loadContent()
        {
           xboxControl = new Icon(new Vector2(120 * Settings.getInterfaceScale(), 200 * Settings.getInterfaceScale()), "Textures/help/xboxControl");
           numpadControl = new Icon(new Vector2(1320 * Settings.getInterfaceScale(), 200 * Settings.getInterfaceScale()), "Textures/help/numpadControl");
           wasdControl = new Icon(new Vector2(720 * Settings.getInterfaceScale(), 200 * Settings.getInterfaceScale()), "Textures/help/wasdControl");
        }

        public override void unloadContent() 
        {

        }

        public override EGameState update(ownGameTime gameTime)
        {
            keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(Keys.Escape))
                return EGameState.MainMenu;
            else
                return EGameState.Help; 
        }

        public override void Draw()
        {
            Game1.getGraphics().GraphicsDevice.BlendState = BlendState.Opaque;
            Game1.getGraphics().GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            Game1.getGraphics().GraphicsDevice.Clear(Color.DarkGreen);

            xboxControl.draw();
            numpadControl.draw();
            wasdControl.draw();
                
        }
    }
}
