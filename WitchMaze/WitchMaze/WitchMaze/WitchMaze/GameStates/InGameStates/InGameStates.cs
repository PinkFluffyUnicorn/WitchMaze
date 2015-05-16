using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WitchMaze.GameStates
{
    public interface InGameState
    {
        void initialize();

        void loadContent();

        void unloadContent();

        EInGameState update(GameTime gameTime);

        void Draw(GameTime gameTime, Microsoft.Xna.Framework.Graphics.GraphicsDevice graphicsDevice);
    }

    public enum EInGameState
    {
        CharacterSelection,
        SingleTime,
        MultiTime,
        MultiNotTime, //dunno good name
        ExitInGame,
        ExitGame,
    }
}
