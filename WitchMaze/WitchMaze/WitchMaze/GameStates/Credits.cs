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
    class Credits : GameState
    {
        public Credits() { }

        Icon credits;
        KeyboardState keyboard = Keyboard.GetState();

        public override void initialize()
        {
            
        }

        public override void loadContent()
        {
           credits = new Icon(new Vector2(0 * Settings.getInterfaceScale(), 0 * Settings.getInterfaceScale()), "Textures/credits/CreditsFilled");
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
                return EGameState.Credits; 
        }

        public override void Draw()
        {
            Game1.getGraphics().GraphicsDevice.BlendState = BlendState.Opaque;
            Game1.getGraphics().GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            Game1.getGraphics().GraphicsDevice.Clear(Color.DarkGreen);
            
            credits.draw();
        }
    }
}
