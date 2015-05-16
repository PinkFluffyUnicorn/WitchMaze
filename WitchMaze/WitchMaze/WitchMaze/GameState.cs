using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WitchMaze
{
    public interface GameState
    {
        void initialize();

        void loadContent();

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
