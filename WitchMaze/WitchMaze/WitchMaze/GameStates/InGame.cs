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
using WitchMaze.ownFunctions;
using WitchMaze.GameStates.InGameStates;
using WitchMaze.PlayerStuff;


namespace WitchMaze.GameStates
{
    class InGame : GameState
    {
        KeyboardState keyboard = Keyboard.GetState();
        GamePadState gamePad1 = GamePad.GetState(PlayerIndex.One);
        GamePadState gamePad2 = GamePad.GetState(PlayerIndex.Two);
        GamePadState gamePad3 = GamePad.GetState(PlayerIndex.Three);
        GamePadState gamePad4 = GamePad.GetState(PlayerIndex.Four);

        EInGameState currentInGameState; //change back to character selection
        EInGameState prevInGameState;

        InGameState inGameState;
        Pause pause;
        bool isPaused = false;

        Won won;
        bool isWon = false;


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

        public override EGameState update(ownGameTime gameTime)
        {
            if (isWon)
            {
                bool w = won.update(gameTime);
                if (w)
                    return EGameState.MainMenu;
            }
            else
            {
                gamePad1 = GamePad.GetState(PlayerIndex.One);
                gamePad2 = GamePad.GetState(PlayerIndex.Two);
                gamePad3 = GamePad.GetState(PlayerIndex.Three);
                gamePad4 = GamePad.GetState(PlayerIndex.Four);

                keyboard = Keyboard.GetState();
                prevInGameState = currentInGameState;
                currentInGameState = inGameState.update(gameTime);
                if (currentInGameState != prevInGameState)
                    handleInGameState();

                if (isPaused)
                {
                    Pause.EPausState pauseState = Pause.update();
                    if (pauseState == Pause.EPausState.pause)
                        isPaused = true;
                    if (pauseState == Pause.EPausState.game)
                    {
                        isPaused = false;
                        gameTime.resume();
                    }

                    if (pauseState == Pause.EPausState.mainMenu)
                    {
                        gameTime.resume();
                        //wird sonst nicht vorher aufgerufen
                        Game1.sounds.inGameSound.Stop();
                        Game1.sounds.menuSound.Play();
                        //currentInGameState = EInGameState.Exit;
                        return EGameState.MainMenu;
                    }
                }

                //if (currentInGameState == EInGameState.Exit)
                //    return EGameState.CharacterSelection;

                //activate pause Keyboard
                if (keyboard.IsKeyDown(Keys.Escape) && !isPaused)
                {
                    isPaused = true;
                    gameTime.pause();
                    pause = new Pause(PlayerStuff.Player.EPlayerControlls.Keyboard1);
                }
                if (gamePad1.IsButtonDown(Buttons.Back) && !isPaused)
                {
                    isPaused = true;
                    gameTime.pause();
                    pause = new Pause(PlayerStuff.Player.EPlayerControlls.Gamepad1);
                }
                if (gamePad2.IsButtonDown(Buttons.Back) && !isPaused)
                {
                    isPaused = true;
                    gameTime.pause();
                    pause = new Pause(PlayerStuff.Player.EPlayerControlls.Gamepad2);
                }
                if (gamePad3.IsButtonDown(Buttons.Back) && !isPaused)
                {
                    isPaused = true;
                    gameTime.pause();
                    pause = new Pause(PlayerStuff.Player.EPlayerControlls.Gamepad3);
                }
                if (gamePad4.IsButtonDown(Buttons.Back) && !isPaused)
                {
                    isPaused = true;
                    gameTime.pause();
                    pause = new Pause(PlayerStuff.Player.EPlayerControlls.Gamepad4);
                }
            }
            
            return EGameState.InGame;
        }

        public override void Draw()
        {
            if (isWon)
                won.draw();
            else
            {
                inGameState.Draw();
                if (isPaused)
                    pause.draw();
            }
        }

        /// <summary>
        /// Handles the GameStateChange, pretty basic
        /// </summary>
        public void handleInGameState()
        {
            List<PlayerStuff.Player> newPlayerList = playerList;
            switch (currentInGameState)
            {
                case EInGameState.NeedForIngrediance:
                    inGameState = new InGameStates.NeedForIngrediance(newPlayerList);
                    break;
                case EInGameState.RushHour:
                    inGameState = new InGameStates.RushHour(newPlayerList);
                    break;
                case EInGameState.Rumble:
                    throw new NotImplementedException();
                    break;
                case EInGameState.Exit:
                    //handle winning
                    foreach (Player p in inGameState.getPlayerList())
                    {
                        if (p.hasWon)
                        {
                            isWon = true;
                            won = new Won(p.getViewportPosition(), true);
                            break;
                        }
                        if (!isWon)
                        {
                            won = new Won(Player.EPlayerViewportPosition.fullscreen, false);
                            isWon = true;
                        }

                    }
                        Game1.sounds.inGameSound.Stop();
                        Game1.sounds.menuSound.Play();
                    break;
            }

            prevInGameState = currentInGameState;
        }

    }
}
