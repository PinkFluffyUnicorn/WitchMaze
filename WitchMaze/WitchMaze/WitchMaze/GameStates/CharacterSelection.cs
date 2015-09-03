using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WitchMaze.InterfaceObjects;
using WitchMaze.ownFunctions;
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

        Icon gameModeIcon, spaceNote, keyboard1, keyboard2, gamepad, rushHour, needForIngredients, escapeNote;

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
                rushHour = new Icon(new Vector2(1200 * Settings.getInterfaceScale(), 45 * Settings.getInterfaceScale()), "Textures/CharacterSelection/RushHourExplanation");
                needForIngredients = new Icon(new Vector2(1200 * Settings.getInterfaceScale(), 45 * Settings.getInterfaceScale()), "Textures/CharacterSelection/NeedForIngredientsExplanation");
                spaceNote = new Icon(new Vector2(1090 * Settings.getInterfaceScale(), 900 * Settings.getInterfaceScale()), "Textures/CharacterSelection/SpaceHinweis");
                escapeNote = new Icon(new Vector2(1125 * Settings.getInterfaceScale(), 950 * Settings.getInterfaceScale()), "Textures/CharacterSelection/escapeNote");
                keyboard1 = new Icon(new Vector2(1075 * Settings.getInterfaceScale(), 300 * Settings.getInterfaceScale()), "Textures/CharacterSelection/Keyboard1");
                keyboard2 = new Icon(new Vector2(1005 * Settings.getInterfaceScale(), 510 * Settings.getInterfaceScale()), "Textures/CharacterSelection/Keyboard2");
                gamepad = new Icon(new Vector2(1070 * Settings.getInterfaceScale(), 725 * Settings.getInterfaceScale()), "Textures/CharacterSelection/Gamepad1");
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

        public override EGameState update(ownGameTime gameTime) 
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
                    this.eInGameState = EInGameState.NeedForIngrediance;
                    break;
                case 1:
                    this.eInGameState = EInGameState.RushHour;
                    break;
                default:
                    throw new NotImplementedException();
            }

            if(keyboard.IsKeyDown(Keys.Space))
                return EGameState.InGame; // GameState InGame braucht übergabeparameter!
            else if (keyboard.IsKeyDown(Keys.Escape))
                return EGameState.MainMenu; 
            else
                return EGameState.CharacterSelection;           
 
        }

        /// <summary>
        /// manages the update of the keyboard controlls
        /// </summary>
        /// <param name="set">set the keyboard contoll with this key</param>
        /// <param name="unset">set the keyboard contoll with this ke</param>
        /// <param name="controllType">the controll type to set</param>
        private void updateColtrolls(Keys set, Keys unset, Player.EPlayerControlls controllType)
        {
            keyboard = Keyboard.GetState();
            if(keyboard.IsKeyDown(set)){
                //Player setzen
                if (!player1ControllsLRS.isSelected() && player2Controlls != controllType && player3Controlls != controllType && player4Controlls != controllType)
                {
                    player1ControllsLRS.setSelected();
                    player1Controlls = controllType;
                }
                if (!player2ControllsLRS.isSelected() && player1Controlls != controllType && player3Controlls != controllType && player4Controlls != controllType)
                {
                    player2ControllsLRS.setSelected();
                    player2Controlls = controllType;
                }
                if (!player3ControllsLRS.isSelected() && player1Controlls != controllType && player2Controlls != controllType && player4Controlls != controllType)
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

        private void updateColtrolls(GamePadState currentState, Player.EPlayerControlls controllType)
        {
            //schauen ob das GamePad überhaupt verbunden ist
            if (currentState.IsConnected)
            {
                if (currentState.IsButtonDown(Buttons.A))
                {
                    //Player setzen
                    if (!player1ControllsLRS.isSelected() && player2Controlls != controllType && player3Controlls != controllType && player4Controlls != controllType)
                    {
                        player1ControllsLRS.setSelected();
                        player1Controlls = controllType;
                    }
                    if (!player2ControllsLRS.isSelected() && player1Controlls != controllType && player3Controlls != controllType && player4Controlls != controllType)
                    {
                        player2ControllsLRS.setSelected();
                        player2Controlls = controllType;
                    }
                    if (!player3ControllsLRS.isSelected() && player1Controlls != controllType && player2Controlls != controllType && player4Controlls != controllType)
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
                if (currentState.IsButtonDown(Buttons.B))
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
        }

        /// <summary>
        /// outsources the update process of the player 1 - 4 to prevent spagetti code
        /// </summary>
        private void updatePlayer()
        {
            //KeyboardControlls
            updateColtrolls(Keys.S, Keys.W, Player.EPlayerControlls.Keyboard1);
            updateColtrolls(Keys.G, Keys.T, Player.EPlayerControlls.Keyboard2);
            updateColtrolls(Keys.K, Keys.I, Player.EPlayerControlls.Keyboard3);
            updateColtrolls(Keys.NumPad2, Keys.NumPad5, Player.EPlayerControlls.KeyboardNumPad);

            //gamepad Controlls
            GamePadState currentState = GamePad.GetState(PlayerIndex.One);
            updateColtrolls(currentState, Player.EPlayerControlls.Gamepad1);
            currentState = GamePad.GetState(PlayerIndex.Two);
            updateColtrolls(currentState, Player.EPlayerControlls.Gamepad2);
            currentState = GamePad.GetState(PlayerIndex.Three);
            updateColtrolls(currentState, Player.EPlayerControlls.Gamepad3);
            currentState = GamePad.GetState(PlayerIndex.Four);
            updateColtrolls(currentState, Player.EPlayerControlls.Gamepad4);

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

        public override void Draw() 
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
            escapeNote.draw();
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
