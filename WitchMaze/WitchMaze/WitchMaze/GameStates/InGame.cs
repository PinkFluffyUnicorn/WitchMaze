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
    class InGame : GameState
    {
        EInGameState currentInGameState = EInGameState.SingleTime; //change back to character selection
        EInGameState prevInGameState;

        InGameState inGameState = new WitchMaze.GameStates.InGameStates.SingleTime();
        
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

        public void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            
            inGameState.Draw(gameTime);
           
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
                    inGameState = new InGameStates.SingleTime();
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
