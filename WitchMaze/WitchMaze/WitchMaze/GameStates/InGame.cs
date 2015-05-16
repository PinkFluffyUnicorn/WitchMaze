using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WitchMaze.GameStates
{
    class InGame : GameState
    {
        EInGameState currentInGameState = EInGameState.SingleTime; //change back to character selection
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
            prevInGameState = currentInGameState;
            currentInGameState = inGameState.update(gameTime);
            if(currentInGameState != prevInGameState)


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

        /// <summary>
        /// Handles the GameStateChange, pretty basic
        /// </summary>
        public void handleGameState()
        {
            Console.Out.Write("GameState change! \n");

            switch (currentInGameState)
            {
                case EInGameState.CharacterSelection:
                    inGameState = new InGameStates.CharacterSelection();
                    break;
                case EInGameState.SingleTime:
                    inGameState = new InGameStates.SinlgeTime();
                    break;
                case EInGameState.MultiTime:
                    throw new NotImplementedException();
                case EInGameState.ExitInGame:
                    throw new NotImplementedException();
                case EInGameState.ExitGame:
                    throw new NotImplementedException();
            }

            inGameState.loadContent();

            inGameState.initialize();

            prevInGameState = currentInGameState;
        }

    }
}
