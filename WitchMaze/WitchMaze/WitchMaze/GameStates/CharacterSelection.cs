using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WitchMaze.InterfaceObjects;
namespace WitchMaze.GameStates
{
    class CharacterSelection : GameState
    {
        KeyboardState keyboard = Keyboard.GetState();

        Vector2 gameModeIconPosition = new Vector2(0, 0);
        Vector2 player1Position = new Vector2(0, 300);
        Vector2 player2Position = new Vector2(0, 500);
        Vector2 player3Position = new Vector2(0, 700);
        Vector2 player4Position = new Vector2(0, 900);

        Icon gameModeIcon;

        Icon player1;
        Icon player2;
        Icon player3;
        Icon player4;

        LeftRightSwitch GameModeSelected;
        LeftRightSwitch player1Controlls;
        LeftRightSwitch player2Controlls;
        LeftRightSwitch player3Controlls;
        LeftRightSwitch player4Controlls;


        public void initialize() 
        {
            
        }

        public void loadContent() 
        {
            if (Game1.getGraphics() != null)
            {
                gameModeIcon = new Icon(gameModeIconPosition, "Textures/CharacterSelection/GameMode");
                player1 = new Icon(player1Position, "Textures/CharacterSelection/Player1NotSelected");
                player2 = new Icon(player2Position, "Textures/CharacterSelection/Player2NotSelected");
                player3 = new Icon(player3Position, "Textures/CharacterSelection/Player3NotSelected");
                player4 = new Icon(player4Position, "Textures/CharacterSelection/Player4NotSelected");

                int offset = 10;//offset zwischen Icons und Switches
                String[] gameModes = {"Textures/CharacterSelection/SinglePlayerTestNotSelected", "Textures/CharacterSelection/MultyPlayerTestNotSelected"};//GameModeIdeen: RushHour, Need for Ingrediance, SpeedRun, NeedForItems
                GameModeSelected = new LeftRightSwitch(new Vector2(gameModeIconPosition.X + gameModeIcon.getWidth() + offset, gameModeIconPosition.Y), gameModes);
                String[] playerControlls = { "Textures/CharacterSelection/Join", "Textures/CharacterSelection/Keyboard", "Textures/CharacterSelection/Gamepad" };
                player1Controlls = new LeftRightSwitch(new Vector2(player1Position.X + player1.getWidth() + offset, player1Position.Y), playerControlls);
                player2Controlls = new LeftRightSwitch(new Vector2(player2Position.X + player2.getWidth() + offset, player2Position.Y), playerControlls);
                player3Controlls = new LeftRightSwitch(new Vector2(player3Position.X + player3.getWidth() + offset, player3Position.Y), playerControlls);
                player4Controlls = new LeftRightSwitch(new Vector2(player4Position.X + player4.getWidth() + offset, player4Position.Y), playerControlls);
            }
        }

        public void unloadContent() { /*throw new NotImplementedException();*/ }

        public EGameState update(GameTime gameTime) 
        { 
            if(keyboard.IsKeyDown(Keys.Enter))
                return EGameState.InGame;
            return EGameState.CharacterSelection; 
        }

        public void Draw(GameTime gameTime) 
        {
            gameModeIcon.draw();
            player1.draw();
            player2.draw();
            player3.draw();
            player4.draw();

            GameModeSelected.draw();
            player1Controlls.draw();
            player2Controlls.draw();
            player3Controlls.draw();
            player4Controlls.draw();
        }

    }
}
