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
        float distY = 96;//die abstände zwischen den Texturen in y-richtung ist 96 bei 1080p, ergibt sich aus button höhe und so...
        float offset = 10;//offset zwischen Icons und Switches
        
        Vector2 gameModeIconPosition = new Vector2(20, 10);

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
                Console.WriteLine(Settings.getInterfaceScale());
                distY *= Settings.getInterfaceScale();
                offset *= Settings.getInterfaceScale();
                gameModeIcon = new Icon(gameModeIconPosition, "Textures/CharacterSelection/GameMode");
                player1 = new Icon(new Vector2(20, (gameModeIcon.getPosition().Y + gameModeIcon.getHeight()) + distY), "Textures/CharacterSelection/Player1NotSelected");
                player2 = new Icon(new Vector2(20, (player1.getPosition().Y + player1.getHeight()) + distY), "Textures/CharacterSelection/Player2NotSelected");
                player3 = new Icon(new Vector2(20, (player2.getPosition().Y + player2.getHeight()) + distY), "Textures/CharacterSelection/Player3NotSelected");
                player4 = new Icon(new Vector2(20, (player3.getPosition().Y + player3.getHeight()) + distY), "Textures/CharacterSelection/Player4NotSelected");

                
                String[] gameModes = {"Textures/CharacterSelection/SinglePlayerTestNotSelected", "Textures/CharacterSelection/MultyPlayerTestNotSelected"};//GameModeIdeen: RushHour, Need for Ingrediance, SpeedRun, NeedForItems
                GameModeSelected = new LeftRightSwitch(new Vector2(gameModeIconPosition.X + gameModeIcon.getWidth() + offset, gameModeIconPosition.Y), gameModes);
                String[] playerControlls = { "Textures/CharacterSelection/Join", "Textures/CharacterSelection/Keyboard", "Textures/CharacterSelection/Gamepad" };
                player1Controlls = new LeftRightSwitch(new Vector2(player1.getPosition().X + player1.getWidth() + offset, player1.getPosition().Y), playerControlls);
                player2Controlls = new LeftRightSwitch(new Vector2(player2.getPosition().X + player2.getWidth() + offset, player2.getPosition().Y), playerControlls);
                player3Controlls = new LeftRightSwitch(new Vector2(player3.getPosition().X + player3.getWidth() + offset, player3.getPosition().Y), playerControlls);
                player4Controlls = new LeftRightSwitch(new Vector2(player4.getPosition().X + player4.getWidth() + offset, player4.getPosition().Y), playerControlls);
            }
        }

        public void unloadContent() { /*throw new NotImplementedException();*/ }

        public EGameState update(GameTime gameTime) 
        {
            keyboard = Keyboard.GetState();
            if(keyboard.IsKeyDown(Keys.Space))
                return EGameState.InGame;
            else 
                return EGameState.CharacterSelection; 
        }

        public void Draw(GameTime gameTime) 
        {
            Game1.getGraphics().GraphicsDevice.BlendState = BlendState.Opaque;
            Game1.getGraphics().GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            Game1.getGraphics().GraphicsDevice.Clear(Color.DarkGreen);

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
