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


namespace WitchMaze.GameStates
{
    class Options : GameState 
    {
        KeyboardState keyboard = Keyboard.GetState();
        
        public void initialize()
        {

        }

        public void loadContent() 
        { 

        }

        public void unloadContent() 
        {

        }

        public EGameState update(GameTime gameTime) 
        {
            keyboard = Keyboard.GetState();
            if(keyboard.IsKeyDown(Keys.Escape))
                return EGameState.MainMenu;
            else 
                return EGameState.Options; 
        }

        public void Draw(GameTime gameTime) 
        {
            Game1.getGraphics().GraphicsDevice.BlendState = BlendState.Opaque;
            Game1.getGraphics().GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            Game1.getGraphics().GraphicsDevice.Clear(Color.DarkGreen);

        }

    }
}
