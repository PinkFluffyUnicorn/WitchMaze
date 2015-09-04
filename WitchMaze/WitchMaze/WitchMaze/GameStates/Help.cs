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
        GamePadState gamePad;

        public override void initialize()
        {

        }

        public override void loadContent()
        {
           xboxControl = new Icon(new Vector2(320 * Settings.getInterfaceScale(), 20 * Settings.getInterfaceScale()), "Textures/help/xboxControl");
           numpadControl = new Icon(new Vector2(1000 * Settings.getInterfaceScale(), 20 * Settings.getInterfaceScale()), "Textures/help/numpadControl");
           wasdControl = new Icon(new Vector2(320 * Settings.getInterfaceScale(), 520 * Settings.getInterfaceScale()), "Textures/help/wasdControl");
        }

        public override void unloadContent() 
        {

        }

        public override EGameState update(ownGameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).IsConnected)
                gamePad = GamePad.GetState(PlayerIndex.One);
            else if (GamePad.GetState(PlayerIndex.Two).IsConnected)
                gamePad = GamePad.GetState(PlayerIndex.Two);
            else if (GamePad.GetState(PlayerIndex.Three).IsConnected)
                gamePad = GamePad.GetState(PlayerIndex.Three);
            else if (GamePad.GetState(PlayerIndex.Four).IsConnected)
                gamePad = GamePad.GetState(PlayerIndex.Four);

            keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(Keys.Escape) || gamePad.IsButtonDown(Buttons.B) || gamePad.IsButtonDown(Buttons.Back))
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
