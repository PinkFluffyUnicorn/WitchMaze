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
        KeyboardState keyboard = Keyboard.GetState();
        EInGameState currentInGameState; //change back to character selection
        EInGameState prevInGameState;

        InGameState inGameState;

        public InGame(EInGameState _inGameState, List<PlayerStuff.Player> _playerList)
        {
            currentInGameState = _inGameState;
            
            Console.WriteLine(_playerList.Count);
            playerList = _playerList;

            handleInGameState();
        }

        public override void initialize()
        {
            inGameState.initialize();
        }

        public override void loadContent()
        {
            inGameState.loadContent();
        }

        public override void unloadContent()
        {
            inGameState.unloadContent();
        }

        public override EGameState update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            keyboard = Keyboard.GetState();
            prevInGameState = currentInGameState;
            currentInGameState = inGameState.update(gameTime);
            if (currentInGameState != prevInGameState)
                handleInGameState();
            
            if (keyboard.IsKeyDown(Keys.Escape))
                return EGameState.MainMenu;
            return EGameState.InGame;
            
            //if (currentInGameState == EInGameState.Exit)
            //    return EGameState.MainMenu;
            //return EGameState.InGame;



        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            inGameState.Draw(gameTime);
        }

        /// <summary>
        /// Handles the GameStateChange, pretty basic
        /// </summary>
        public void handleInGameState()
        {
            List<PlayerStuff.Player> newPlayerList = playerList;
            switch (currentInGameState)
            {
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
