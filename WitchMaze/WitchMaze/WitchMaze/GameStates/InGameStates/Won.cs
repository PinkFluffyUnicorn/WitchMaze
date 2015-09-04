using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WitchMaze.InterfaceObjects;
using WitchMaze.ownFunctions;

namespace WitchMaze.GameStates.InGameStates
{
    class Won
    {
        static Icon background;
        Text infoText;
        float elapsedTime;

        KeyboardState  keyboard;
        GamePadState gamePad1;
        GamePadState gamePad2;
        GamePadState gamePad3;
        GamePadState gamePad4;

        public Won(PlayerStuff.Player.EPlayerViewportPosition playerIndex, bool won)
        {
            infoText = new Text("03", new Vector2(0, 0));
            infoText.setIndividualScale(2);
            infoText.setPosition(new Vector2(Settings.getResolutionX() / 2 - infoText.getWidth() / 2, Settings.getResolutionY() - infoText.getHeight()));
            elapsedTime = 0;

            switch (playerIndex)
            {

                case PlayerStuff.Player.EPlayerViewportPosition.fullscreen:
                    if(won)
                       {
                           infoText.setColor(Color.Black);
                         background = new Icon(new Microsoft.Xna.Framework.Vector2(0, 0), "Textures/WinScreens/youWin");
                       }
                    else
                    {
                        infoText.setColor(Color.White);
                        background = new Icon(new Microsoft.Xna.Framework.Vector2(0, 0), "Textures/WinScreens/youLose");
                    }
                    break;
                case PlayerStuff.Player.EPlayerViewportPosition.left:
                    infoText.setColor(Color.White);
                    background = new Icon(new Microsoft.Xna.Framework.Vector2(0, 0), "Textures/WinScreens/player1Win");
                    break;
                case PlayerStuff.Player.EPlayerViewportPosition.right:
                    infoText.setColor(Color.White);
                    background = new Icon(new Microsoft.Xna.Framework.Vector2(0, 0), "Textures/WinScreens/player2Win");
                    break;
                case PlayerStuff.Player.EPlayerViewportPosition.topLeft:
                    infoText.setColor(Color.White);
                    background = new Icon(new Microsoft.Xna.Framework.Vector2(0, 0), "Textures/WinScreens/player1Win");
                    break;
                case PlayerStuff.Player.EPlayerViewportPosition.botLeft:
                    infoText.setColor(Color.White);
                    background = new Icon(new Microsoft.Xna.Framework.Vector2(0, 0), "Textures/WinScreens/player2Win");
                    break;
                case PlayerStuff.Player.EPlayerViewportPosition.topRight:
                    infoText.setColor(Color.White);
                    background = new Icon(new Microsoft.Xna.Framework.Vector2(0, 0), "Textures/WinScreens/player3Win");
                    break;
                case PlayerStuff.Player.EPlayerViewportPosition.botRight:
                    infoText.setColor(Color.White);
                    background = new Icon(new Microsoft.Xna.Framework.Vector2(0, 0), "Textures/WinScreens/player4Win");
                    break;
            }
        }
        /// <summary>
        /// updates the won state
        /// </summary>
        /// <param name="gameTime"></param>
        /// <returns>true if won should be closed</returns>
        public bool update(ownGameTime gameTime)
        {
            //zwei sekunden kann man nichts machen dann kann man mit enter oder a weiter
            //update the InfoText
            elapsedTime += gameTime.getElapsedGameTime();
            if (elapsedTime > 1000)
            {
                infoText.updateText("02");
                infoText.setPosition(new Vector2(Settings.getResolutionX() / 2 - infoText.getWidth() / 2, Settings.getResolutionY() - infoText.getHeight()));
            }
            if (elapsedTime > 2000)
            {
                infoText.updateText("01");
                infoText.setPosition(new Vector2(Settings.getResolutionX() / 2 - infoText.getWidth() / 2, Settings.getResolutionY() - infoText.getHeight()));
            }
            if (elapsedTime > 3000)
            {
                keyboard = Keyboard.GetState();
                gamePad1 = GamePad.GetState(PlayerIndex.One);
                gamePad2 = GamePad.GetState(PlayerIndex.Two);
                gamePad3 = GamePad.GetState(PlayerIndex.Three);
                gamePad4 = GamePad.GetState(PlayerIndex.Four);

                infoText.updateText("Press Enter or A to continue!");
                infoText.setPosition(new Vector2(Settings.getResolutionX() / 2 - infoText.getWidth() / 2, Settings.getResolutionY() - infoText.getHeight()));
                if (keyboard.IsKeyDown(Keys.Enter) || keyboard.IsKeyDown(Keys.Escape) ||gamePad1.IsButtonDown(Buttons.A) || gamePad2.IsButtonDown(Buttons.A) || gamePad3.IsButtonDown(Buttons.A) || gamePad4.IsButtonDown(Buttons.A))
                    return true;
            }
            return false;
        }
        public void draw()
        {
            background.draw();
            infoText.draw();
        }
    }
}
