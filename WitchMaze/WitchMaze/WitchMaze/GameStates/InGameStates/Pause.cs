using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WitchMaze.InterfaceObjects;
using WitchMaze.PlayerStuff;

namespace WitchMaze.GameStates.InGameStates
{
    class Pause
    {
        public enum EPausState{
        pause,
        game,
        mainMenu,
        };

        static Button resume;
        static Button exit;
        static Icon gresScreen;
        static int count;
        static bool isPressed;
        static KeyboardState keyboard;
        static GamePadState gamePad;
        static Player.EPlayerControlls controlls;
        public Pause(Player.EPlayerControlls _controlls)
        {
            controlls = _controlls;
            count = 0;
            isPressed = true;

            Vector2 position = new Vector2(0, 0);
            gresScreen = new Icon( position, "Textures/Pause/greyScreen");

            float distY = 100 * Settings.getInterfaceScale();
            resume = new Button(position, "Textures/Pause/resumeNot", "Textures/Pause/resumeSelected");
            exit = new Button(position, "Textures/Pause/exitNot", "Textures/Pause/exitSelected");

            position = new Vector2(Settings.getResolutionX() / 2 - resume.getWidth()/2, Settings.getResolutionY() / 2 - resume.getHeight() - distY);
            resume.setPosition(position);
            position = new Vector2(Settings.getResolutionX() / 2 - exit.getWidth() / 2, Settings.getResolutionY() / 2 + distY);
            exit.setPosition(position);
        }


        internal static EPausState update()
        {
            keyboard = Keyboard.GetState();
            switch (controlls)
            {
                case Player.EPlayerControlls.Gamepad1:
                    return updateG(PlayerIndex.One);
                case Player.EPlayerControlls.Gamepad2:
                    return updateG(PlayerIndex.Two);
                case Player.EPlayerControlls.Gamepad3:
                    return updateG(PlayerIndex.Three);
                case Player.EPlayerControlls.Gamepad4:
                    return updateG(PlayerIndex.Four);
                //case Player.EPlayerControlls.Keyboard1:
                //    return updateK(Keys.W, Keys.S, Keys.Enter);
                //case Player.EPlayerControlls.KeyboardNumPad:
                //    return updateK(Keys.NumPad2, Keys.NumPad5, Keys.Enter);
                default:
                    return updateK(Keys.Up, Keys.Down, Keys.Enter);
            }


            throw new NotImplementedException();
        }

        /// <summary>
        /// handles the update of the gamepad
        /// </summary>
        /// <param name="index">gamePad to paused the game</param>
        /// <returns>the state of the pause</returns>
        private static EPausState updateG(PlayerIndex index)
        {
            gamePad = GamePad.GetState(index);
            if (!gamePad.IsButtonDown(Buttons.DPadUp) && !gamePad.IsButtonDown(Buttons.A) && !gamePad.IsButtonDown(Buttons.DPadDown))
                isPressed = false;

            if (gamePad.IsButtonDown(Buttons.DPadUp) && !isPressed)
            {
                count++;
                isPressed = true;
            }
            if (gamePad.IsButtonDown(Buttons.DPadDown) && !isPressed)
            {
                count = count + 1;
                isPressed = true;
            }
            count %= 2;

            switch (count)
            {
                case 0:
                    exit.setNotSelected();
                    resume.setSelected();
                    break;
                case 1:
                    exit.setSelected();
                    resume.setNotSelected();
                    break;
            }

            if (gamePad.IsButtonDown(Buttons.A) && !isPressed)
            {
                if (exit.isSelected())
                    return EPausState.mainMenu;
                if (resume.isSelected())
                    return EPausState.game;
                isPressed = true;
            }
            return EPausState.pause;
        }

        /// <summary>
        /// handles the update of the gamepad
        /// </summary>
        /// <param name="index">gamePad to paused the game</param>
        /// <returns>the state of the pause</returns>
        private static EPausState updateK(Keys up, Keys down, Keys enter)
        {
            if (!keyboard.IsKeyDown(up) && !keyboard.IsKeyDown(down) && !keyboard.IsKeyDown(enter))
                isPressed = false;

            if (keyboard.IsKeyDown(up) && !isPressed)
            {
                count++;
                isPressed = true;
            }
            if (keyboard.IsKeyDown(down) && !isPressed)
            {
                count = count + 1;
                isPressed = true;
            }
            count %= 2;

            switch (count)
            {
                case 0:
                    exit.setNotSelected();
                    resume.setSelected();
                    break;
                case 1:
                    exit.setSelected();
                    resume.setNotSelected();
                    break;
            }

            if (keyboard.IsKeyDown(enter) && !isPressed)
            {
                if (exit.isSelected())
                    return EPausState.mainMenu;
                if (resume.isSelected())
                    return EPausState.game;
                isPressed = true;
            }
            return EPausState.pause;
        }
        public void draw()
        {
            gresScreen.draw();
            resume.draw();
            exit.draw();
        }
    }

}
