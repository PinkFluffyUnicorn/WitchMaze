using Microsoft.Xna.Framework;
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

        void Draw(GameTime gameTime, GraphicsDeviceManager graphics);
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
