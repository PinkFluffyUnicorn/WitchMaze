using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WitchMaze.InterfaceObjects;
using WitchMaze.PlayerStuff;


namespace WitchMaze.GameStates
    
{
    class CharacterSelection : GameState
    {
        bool isPressed;

        Player player1, player2, player3, player4;
        PlayerStuff.Player.EPlayerControlls player1Controlls, player2Controlls, player3Controlls, player4Controlls;

        KeyboardState keyboard = Keyboard.GetState();
        float distY = 96;//die abstände zwischen den Texturen in y-richtung ist 96 bei 1080p, ergibt sich aus button höhe und so...
        float offset = 10;//offset zwischen Icons und Switches
        
        Vector2 gameModeIconPosition = new Vector2(20, 10);

        Icon gameModeIcon, spaceNote, keyboard1, keyboard2, gamepad, rushHour, needForIngredients;

        Icon player1Icon, player2Icon, player3Icon, player4Icon;

        LeftRightSwitch GameModeSelected;
        LeftRightSwitch player1ControllsLRS, player2ControllsLRS, player3ControllsLRS, player4ControllsLRS;//LRS for LeftRightSwitch

        public override void initialize() 
        {
            
        }

        public override void loadContent() 
        {
            
            if (Game1.getGraphics() != null)
            {
                distY *= Settings.getInterfaceScale();
                offset *= Settings.getInterfaceScale();
                gameModeIcon = new Icon(gameModeIconPosition, "Textures/CharacterSelection/GameMode");
                rushHour = new Icon(new Vector2(630, 45 * Settings.getInterfaceScale()), "Textures/CharacterSelection/RushHourExplanation");
                needForIngredients = new Icon(new Vector2(630, 45 * Settings.getInterfaceScale()), "Textures/CharacterSelection/NeedForIngredientsExplanation");
                spaceNote = new Icon(new Vector2(645, 900 * Settings.getInterfaceScale()), "Textures/CharacterSelection/SpaceHinweis");
                keyboard1 = new Icon(new Vector2(640, 300 * Settings.getInterfaceScale()), "Textures/CharacterSelection/Keyboard1");
                keyboard2 = new Icon(new Vector2(605, 510 * Settings.getInterfaceScale()), "Textures/CharacterSelection/Keyboard2");
                gamepad = new Icon(new Vector2(640, 725 * Settings.getInterfaceScale()), "Textures/CharacterSelection/Gamepad1");
                player1Icon = new Icon(new Vector2(20, (gameModeIcon.getPosition().Y + gameModeIcon.getHeight()) + distY), "Textures/CharacterSelection/Player1NotSelected");
                player2Icon = new Icon(new Vector2(20, (player1Icon.getPosition().Y + player1Icon.getHeight()) + distY), "Textures/CharacterSelection/Player2NotSelected");
                player3Icon = new Icon(new Vector2(20, (player2Icon.getPosition().Y + player2Icon.getHeight()) + distY), "Textures/CharacterSelection/Player3NotSelected");
                player4Icon = new Icon(new Vector2(20, (player3Icon.getPosition().Y + player3Icon.getHeight()) + distY), "Textures/CharacterSelection/Player4NotSelected");
                                
                String[] gameModes = {"Textures/CharacterSelection/NeedForIngredientsNotSelected", "Textures/CharacterSelection/RushHourNotSelected"};//GameModeIdeen: RushHour, Need for Ingrediance, SpeedRun, NeedForItems
                //0 = test;
                //1 = rushHour;
                GameModeSelected = new LeftRightSwitch(new Vector2(gameModeIconPosition.X + gameModeIcon.getWidth() + offset, gameModeIconPosition.Y), gameModes);
                GameModeSelected.setSelected();
                String[] playerControlls = { "Textures/CharacterSelection/Join", "Textures/CharacterSelection/Keyboard", "Textures/CharacterSelection/Gamepad" };
                // 1 := join
                // 2 := Keyboard
                // 3 := Gamepad
                player1ControllsLRS = new LeftRightSwitch(new Vector2(player1Icon.getPosition().X + player1Icon.getWidth() + offset, player1Icon.getPosition().Y), playerControlls);
                player2ControllsLRS = new LeftRightSwitch(new Vector2(player2Icon.getPosition().X + player2Icon.getWidth() + offset, player2Icon.getPosition().Y), playerControlls);
                player3ControllsLRS = new LeftRightSwitch(new Vector2(player3Icon.getPosition().X + player3Icon.getWidth() + offset, player3Icon.getPosition().Y), playerControlls);
                player4ControllsLRS = new LeftRightSwitch(new Vector2(player4Icon.getPosition().X + player4Icon.getWidth() + offset, player4Icon.getPosition().Y), playerControlls);
            }
        }

        public override void unloadContent() { /*throw new NotImplementedException();*/ }

        public override EGameState update(GameTime gameTime) 
        {
            updatePlayer();
            if (!keyboard.IsKeyDown(Keys.Left) && !keyboard.IsKeyDown(Keys.Right))
                isPressed = false;

            if (keyboard.IsKeyDown(Keys.Left) && !isPressed)
            {
                GameModeSelected.switchLeft();
                isPressed = true;
            }

            if (keyboard.IsKeyDown(Keys.Right) && !isPressed)
            {
                GameModeSelected.switchRight();
                isPressed = true;
            }
            //set selected GameMode
            switch (GameModeSelected.getDisplayedIndex())
            {
                case 0:
                    this.eInGameState = EInGameState.SingleTime;
                    break;
                case 1:
                    this.eInGameState = EInGameState.MazeRun;
                    break;
                default:
                    throw new NotImplementedException();
            }

            if(keyboard.IsKeyDown(Keys.Space))
                return EGameState.InGame; // GameState InGame braucht übergabeparameter!
            else 
                return EGameState.CharacterSelection; 
        }


        private void updateColtrolls(Keys set, Keys unset, Player.EPlayerControlls controllType)
        {
            keyboard = Keyboard.GetState();
            if(keyboard.IsKeyDown(set)){
                //Player setzen
                if (!player1ControllsLRS.isSelected())
                {
                    player1ControllsLRS.setSelected();
                    player1Controlls = controllType;
                }
                if (!player2ControllsLRS.isSelected() && player1Controlls != controllType)
                {
                    player2ControllsLRS.setSelected();
                    player2Controlls = controllType;
                }
                if (!player3ControllsLRS.isSelected() && player1Controlls != controllType && player2Controlls != controllType)
                {
                    player3ControllsLRS.setSelected();
                    player3Controlls = controllType;
                }
                if (!player4ControllsLRS.isSelected() && player1Controlls != controllType && player2Controlls != controllType && player3Controlls != controllType)
                {
                    player4ControllsLRS.setSelected();
                    player4Controlls = controllType;
                }
            }
            if (keyboard.IsKeyDown(unset))
            {
                //Player zurück setzen 
                if (player1ControllsLRS.isSelected() && player1Controlls == controllType)
                {
                    player1ControllsLRS.setNotSelected();
                    player1Controlls = Player.EPlayerControlls.none;
                }
                if (player2ControllsLRS.isSelected() && player2Controlls == controllType)
                {
                    player2ControllsLRS.setNotSelected();
                    player2Controlls = Player.EPlayerControlls.none;
                }
                if (player3ControllsLRS.isSelected() && player3Controlls == controllType)
                {
                    player3ControllsLRS.setNotSelected();
                    player3Controlls = Player.EPlayerControlls.none;
                }
                if (player4ControllsLRS.isSelected() && player4Controlls == controllType)
                {
                    player4ControllsLRS.setNotSelected();
                    player4Controlls = Player.EPlayerControlls.none;
                }
            }
        }

        /// <summary>
        /// outsources the update process of the player 1 - 4 to prevent spagetti code
        /// </summary>
        private void updatePlayer()
        {
            //Der erste der eine seiner tasten drückt bekommt Player1, der zweite Player 2 und so weiter...
            //wird eine taste gedrückt welche schon einen spieler ausgewählt hat so wird er wieder unausgewählt

            updateColtrolls(Keys.S, Keys.W, Player.EPlayerControlls.Keyboard1);
            updateColtrolls(Keys.G, Keys.T, Player.EPlayerControlls.Keyboard2);
            updateColtrolls(Keys.K, Keys.I, Player.EPlayerControlls.Keyboard3);
            updateColtrolls(Keys.NumPad2, Keys.NumPad5, Player.EPlayerControlls.KeyboardNumPad);
            updateColtrolls(Keys.C, Keys.X, Player.EPlayerControlls.Gamepad1); //zum testen
            //if (keyboard.IsKeyDown(Keys.NumPad5) || keyboard.IsKeyDown(Keys.NumPad1) || keyboard.IsKeyDown(Keys.NumPad2) || keyboard.IsKeyDown(Keys.NumPad3))
            //switch (player1ControllsLRS.getDisplayedIndex())
            //{
            //    case 0:
            //        player1Controlls = Player.EPlayerControlls.none;
            //        break;
            //    case 1:
            //        player1Controlls = Player.EPlayerControlls.Keyboard1;
            //        break;
            //    case 2:
            //        player1Controlls = Player.EPlayerControlls.Gamepad1;
            //        break;
            //}
            //switch (player2ControllsLRS.getDisplayedIndex())
            //{
            //    case 0:
            //        player2Controlls = Player.EPlayerControlls.none;
            //        break;
            //    case 1:
            //        if (player1Controlls == Player.EPlayerControlls.Keyboard1)
            //            player2Controlls = Player.EPlayerControlls.Keyboard2;
            //        else
            //            player2Controlls = Player.EPlayerControlls.Keyboard1;
            //        break;
            //    case 2:
            //        if (player1Controlls == Player.EPlayerControlls.Gamepad1)
            //            player2Controlls = Player.EPlayerControlls.Gamepad2;
            //        else
            //            player2Controlls = Player.EPlayerControlls.Gamepad1;
            //        break;
            //}//nochmal überarbeiten :/
            //switch (player3ControllsLRS.getDisplayedIndex())
            //{
            //    case 0:
            //        break;
            //    case 1:
            //        break;
            //    case 2:
            //        break;
            //    case 3:
            //        break;
            //    case 4:
            //        break;
            //    case 5:
            //        break;
            //    case 6:
            //        break;
            //}
            //switch (player4ControllsLRS.getDisplayedIndex())
            //{
            //    case 0:
            //        break;
            //    case 1:
            //        break;
            //    case 2:
            //        break;
            //    case 3:
            //        break;
            //    case 4:
            //        break;
            //    case 5:
            //        break;
            //    case 6:
            //        break;
            //}

            //player1ControllsLRS.setSelected();
            //player2ControllsLRS.setSelected();
            //player3ControllsLRS.setSelected();
            //player4ControllsLRS.setSelected();
            //player1Controlls = Player.EPlayerControlls.Keyboard1;
            //player2Controlls = Player.EPlayerControlls.Keyboard2;
            //player3Controlls = Player.EPlayerControlls.Gamepad1;
            //player4Controlls = Player.EPlayerControlls.Gamepad2;

            //delete all player
            playerList.Clear();
            //now create all needet Player Objects
            //player updaten und in playerList einfügen
            if (player1ControllsLRS.isSelected())
            {//Player1
                player1 = new Player(player1Controlls, Player.EPlayerIndex.one);
                playerList.Add(player1);
            }
            else
                player1 = null;
            if (player2ControllsLRS.isSelected())
            {//Player2
                player2 = new Player(player2Controlls, Player.EPlayerIndex.two);
                playerList.Add(player2);
            }
            else
                player2 = null;
            if (player3ControllsLRS.isSelected())
            {//Player3
                player3 = new Player(player3Controlls, Player.EPlayerIndex.three);
                playerList.Add(player3);
            }
            else
                player3 = null;
            if (player4ControllsLRS.isSelected())
            {//Player4
                player4 = new Player(player4Controlls, Player.EPlayerIndex.vour);
                playerList.Add(player4);
            }
            else
                player4 = null;
            //Check if list is too big
            if (playerList.Count > 4 || playerList == null)
                throw new IndexOutOfRangeException();

        }

        public override void Draw(GameTime gameTime) 
        {
            Game1.getGraphics().GraphicsDevice.BlendState = BlendState.Opaque;
            Game1.getGraphics().GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            Game1.getGraphics().GraphicsDevice.Clear(Color.DarkGreen);

            gameModeIcon.draw();
            player1Icon.draw();
            player2Icon.draw();
            player3Icon.draw();
            player4Icon.draw();

            GameModeSelected.draw();
            player1ControllsLRS.draw();
            player2ControllsLRS.draw();
            player3ControllsLRS.draw();
            player4ControllsLRS.draw();
            spaceNote.draw();
            keyboard1.draw();
            keyboard2.draw();
            gamepad.draw();
            
            if(GameModeSelected.getDisplayedIndex() == 1)
                rushHour.draw();

            if (GameModeSelected.getDisplayedIndex() == 0)
                needForIngredients.draw();

        }

    }
}
