using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WitchMaze.GameStates
{
    class InGame : GameState
    {
        EInGameState currentInGameState;
        EInGameState prevInGameState;

        InGameState inGameState;
        
        public void initialize()
        {
            inGameState.initialize();
        }

        public void loadContent()
        {
            inGameState.loadContent();
        }

        public void unloadContent()
        {
            inGameState.unloadContent();
        }

        public EGameState update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            currentInGameState = inGameState.update(gameTime);

            if (currentInGameState == EInGameState.ExitInGame)
                return EGameState.MainMenu;
            if (currentInGameState == EInGameState.ExitInGame)
                return EGameState.Exit;
            return EGameState.InGame;
        }

        public void Draw(Microsoft.Xna.Framework.GameTime gameTime, Microsoft.Xna.Framework.GraphicsDeviceManager graphics)
        {
            throw new NotImplementedException();
        }


    }
}
