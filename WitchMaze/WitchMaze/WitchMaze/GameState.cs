using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace WitchMaze
{
    public interface GameState
    {
        void initialize();

        void loadContent(ContentManager content, GraphicsDeviceManager graphics);

        void unloadContent();

        EGameState update(GameTime gameTime);

        void Draw(GameTime gameTime, GraphicsDeviceManager graphicsDevice);
    }

    public enum EGameState
    {
        MainMenu,
        InGame,
        Credits,
        Options,
        Exit,
    }


}
