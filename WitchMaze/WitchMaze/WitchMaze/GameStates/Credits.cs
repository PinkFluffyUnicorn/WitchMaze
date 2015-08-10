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
    class Credits : GameState
    {
        public Credits() { }

        Icon credits;
        KeyboardState keyboard = Keyboard.GetState();

        public void initialize()
        {
            
        }

        public void loadContent()
        {
            credits = new Icon(new Vector2(411, 20), "Textures/credits/creditsTitel");
        }

        public void unloadContent()
        {

        }

        public EGameState update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(Keys.Escape))
                return EGameState.MainMenu;
            else
                return EGameState.Credits; 
        }

        public void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            Game1.getGraphics().GraphicsDevice.BlendState = BlendState.Opaque;
            Game1.getGraphics().GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            Game1.getGraphics().GraphicsDevice.Clear(Color.Black);

            credits.draw();
        }
    }
}
