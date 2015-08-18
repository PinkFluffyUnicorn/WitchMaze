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
        EInGameState currentInGameState = EInGameState.CharacterSelection; //change back to character selection
        EInGameState prevInGameState;

        InGameState inGameState = new WitchMaze.GameStates.InGameStates.CharacterSelection();

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
            if (currentInGameState != prevInGameState)
                handleInGameState();

            if (currentInGameState == EInGameState.Exit)
                return EGameState.MainMenu;
            return EGameState.InGame;

        }

        public void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            inGameState.Draw(gameTime);
        }

        /// <summary>
        /// Handles the GameStateChange, pretty basic
        /// </summary>
        public void handleInGameState()
        {
            List<PlayerStuff.Player> newPlayerList = inGameState.getPlayerList();
            switch (currentInGameState)
            {
                case EInGameState.CharacterSelection:
                    inGameState = new InGameStates.CharacterSelection();
                    break;
                case EInGameState.SingleTime:
                    
                    inGameState = new InGameStates.SingleTime(newPlayerList);
                    break;
                case EInGameState.MazeRun:
                    inGameState = new InGameStates.MazeRun(newPlayerList);
                    break;
                case EInGameState.Rumble:
                    throw new NotImplementedException();
                    break;
                case EInGameState.Exit:
                    throw new NotImplementedException();
                    break;
            }
            Console.WriteLine(inGameState);
            inGameState.initialize();

            inGameState.loadContent();

            prevInGameState = currentInGameState;
        }

    }
}
