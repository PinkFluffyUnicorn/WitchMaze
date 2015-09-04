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
        static bool isPressed, isPressedP1 = true, isPressedP2 = true, isPressedP3 = true, isPressedP4 = true;

        GamePadState gamePad;

        float elapsedTime;
        Text infoText;

        bool startGame;
        Player player1, player2, player3, player4;
        PlayerStuff.Player.EPlayerControlls player1Controlls, player2Controlls, player3Controlls, player4Controlls;

        KeyboardState keyboard = Keyboard.GetState();
        float distY = 96;//die abstände zwischen den Texturen in y-richtung ist 96 bei 1080p, ergibt sich aus button höhe und so...
        float offset = 10;//offset zwischen Icons und Switches
        
        Vector2 gameModeIconPosition = new Vector2(20, 10);

        Icon gameModeIcon, spaceNote, keyboard1, keyboard2, gamepad, rushHour, needForIngredients, escapeNote;

        Button player1Icon, player2Icon, player3Icon, player4Icon;

        LeftRightSwitch GameModeSelected;
        LeftRightSwitch player1ControllsLRS, player2ControllsLRS, player3ControllsLRS, player4ControllsLRS;//LRS for LeftRightSwitch

        public override void initialize() 
        {
            elapsedTime = 0;
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
                //spaceNote = new Icon(new Vector2(1090 * Settings.getInterfaceScale(), 900 * Settings.getInterfaceScale()), "Textures/CharacterSelection/SpaceHinweis");
                escapeNote = new Icon(new Vector2(1080 * Settings.getInterfaceScale(), 950 * Settings.getInterfaceScale()), "Textures/CharacterSelection/escapeNote");
                keyboard1 = new Icon(new Vector2(1005 * Settings.getInterfaceScale(), 300 * Settings.getInterfaceScale()), "Textures/CharacterSelection/Keyboard1");
                keyboard2 = new Icon(new Vector2(1005 * Settings.getInterfaceScale(), 510 * Settings.getInterfaceScale()), "Textures/CharacterSelection/Keyboard2");
                gamepad = new Icon(new Vector2(1005 * Settings.getInterfaceScale(), 725 * Settings.getInterfaceScale()), "Textures/CharacterSelection/Gamepad1");
                player1Icon = new Button(new Vector2(20, (gameModeIcon.getPosition().Y + gameModeIcon.getHeight()) + distY), "Textures/CharacterSelection/Player1NotSelected", "Textures/CharacterSelection/Player1Selected");
                player2Icon = new Button(new Vector2(20, (player1Icon.getPosition().Y + player1Icon.getHeight()) + distY), "Textures/CharacterSelection/Player2NotSelected", "Textures/CharacterSelection/Player2Selected");
                player3Icon = new Button(new Vector2(20, (player2Icon.getPosition().Y + player2Icon.getHeight()) + distY), "Textures/CharacterSelection/Player3NotSelected", "Textures/CharacterSelection/Player3Selected");
                player4Icon = new Button(new Vector2(20, (player3Icon.getPosition().Y + player3Icon.getHeight()) + distY), "Textures/CharacterSelection/Player4NotSelected", "Textures/CharacterSelection/Player4Selected");
                                
                String[] gameModes = {"Textures/CharacterSelection/NeedForIngredientsNotSelected", "Textures/CharacterSelection/RushHourNotSelected"};//GameModeIdeen: RushHour, Need for Ingrediance, SpeedRun, NeedForItems
                //0 = test;
                //1 = rushHour;
                GameModeSelected = new LeftRightSwitch(new Vector2(gameModeIconPosition.X + gameModeIcon.getWidth() + offset, gameModeIconPosition.Y), gameModes);
                GameModeSelected.setSelected();
                String[] playerControlls = { "Textures/CharacterSelection/Join", 
                                               "Textures/CharacterSelection/Keyboard","Textures/CharacterSelection/KeyboardNumPad", 
                                               "Textures/CharacterSelection/GamepadNr1", "Textures/CharacterSelection/GamepadNr2", "Textures/CharacterSelection/GamepadNr3", "Textures/CharacterSelection/GamepadNr4"  };
                // 1 := join
                // 2 := Keyboard
                // 3 := Gamepad
                player1ControllsLRS = new LeftRightSwitch(new Vector2(player1Icon.getPosition().X + player1Icon.getWidth() + offset, player1Icon.getPosition().Y), playerControlls);
                player2ControllsLRS = new LeftRightSwitch(new Vector2(player2Icon.getPosition().X + player2Icon.getWidth() + offset, player2Icon.getPosition().Y), playerControlls);
                player3ControllsLRS = new LeftRightSwitch(new Vector2(player3Icon.getPosition().X + player3Icon.getWidth() + offset, player3Icon.getPosition().Y), playerControlls);
                player4ControllsLRS = new LeftRightSwitch(new Vector2(player4Icon.getPosition().X + player4Icon.getWidth() + offset, player4Icon.getPosition().Y), playerControlls);

                infoText = new Text("Game starts in 03!", new Vector2(0, 0));
                infoText.setIndividualScale(3);
                infoText.setPosition(new Vector2(Settings.getResolutionX() / 2 - infoText.getWidth() / 2, Settings.getResolutionY() / 2 - infoText.getHeight() / 2));
                infoText.setColor(Color.White);
            }
        }

        public override void unloadContent() { /*throw new NotImplementedException();*/ }

        public override EGameState update(ownGameTime gameTime) 
        {
            if (GamePad.GetState(PlayerIndex.One).IsConnected)
                gamePad = GamePad.GetState(PlayerIndex.One);
            else if (GamePad.GetState(PlayerIndex.Two).IsConnected)
                gamePad = GamePad.GetState(PlayerIndex.Two);
            else if (GamePad.GetState(PlayerIndex.Three).IsConnected)
                gamePad = GamePad.GetState(PlayerIndex.Three);
            else if (GamePad.GetState(PlayerIndex.Four).IsConnected)
                gamePad = GamePad.GetState(PlayerIndex.Four);

            updatePlayer();
            if (!keyboard.IsKeyDown(Keys.Left) && !keyboard.IsKeyDown(Keys.Right) && !gamePad.IsButtonDown(Buttons.DPadLeft) && !gamePad.IsButtonDown(Buttons.DPadRight) && !gamePad.IsButtonDown(Buttons.A) && !gamePad.IsButtonDown(Buttons.B))
                isPressed = false;

            if ((keyboard.IsKeyDown(Keys.Left) || gamePad.IsButtonDown(Buttons.DPadLeft)) && !isPressed)
            {
                GameModeSelected.switchLeftKlicked();
                isPressed = true;
            }

            if ((keyboard.IsKeyDown(Keys.Right) || gamePad.IsButtonDown(Buttons.DPadRight)) && !isPressed)
            {
                GameModeSelected.switchRightKlicked();
                isPressed = true;
            }

            //start?
            if ((player1Controlls == Player.EPlayerControlls.none || player1Icon.isSelected())
                && (player2Controlls == Player.EPlayerControlls.none || player2Icon.isSelected())
                && (player3Controlls == Player.EPlayerControlls.none || player3Icon.isSelected())
                && (player4Controlls == Player.EPlayerControlls.none || player4Icon.isSelected())
                && !(player1Controlls == Player.EPlayerControlls.none && player2Controlls == Player.EPlayerControlls.none && player3Controlls == Player.EPlayerControlls.none && player4Controlls == Player.EPlayerControlls.none))
                startGame = true;
            else
                startGame = false;

            if (startGame)
            {
                elapsedTime += gameTime.getElapsedGameTime();
                if (elapsedTime > 1000)
                {
                    infoText.updateText("Game starts in 02!");
                    infoText.setPosition(new Vector2(Settings.getResolutionX() / 2 - infoText.getWidth() / 2, Settings.getResolutionY()/2 - infoText.getHeight()/2));
                }
                if (elapsedTime > 2000)
                {
                    infoText.updateText("Game starts in 01!");
                    infoText.setPosition(new Vector2(Settings.getResolutionX() / 2 - infoText.getWidth() / 2, Settings.getResolutionY() / 2 - infoText.getHeight() / 2));
                }
                if (elapsedTime > 3000)
                {
                    infoText.updateText("Start!");
                    infoText.setPosition(new Vector2(Settings.getResolutionX() / 2 - infoText.getWidth() / 2, Settings.getResolutionY() / 2 - infoText.getHeight() / 2));
                }
                if (elapsedTime > 3200)
                {
                    return EGameState.InGame; // GameState InGame braucht übergabeparameter!
                }
            }
            else
            {
                elapsedTime = 0;
                infoText.updateText("Game starts in 03!");
                infoText.setPosition(new Vector2(Settings.getResolutionX() / 2 - infoText.getWidth() / 2, Settings.getResolutionY() / 2 - infoText.getHeight() / 2));
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

            //if(keyboard.IsKeyDown(Keys.Space))
            //    return EGameState.InGame; // GameState InGame braucht übergabeparameter!
            if (keyboard.IsKeyDown(Keys.Escape) /*|| gamePad.IsButtonDown(Buttons.B)*/ || gamePad.IsButtonDown(Buttons.Back))
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

            //set isPressed = false
            if (!keyboard.IsKeyDown(set) && !keyboard.IsKeyDown(unset) && player1Controlls == controllType || (player1Controlls == Player.EPlayerControlls.none))
                isPressedP1 = false;
            if (!keyboard.IsKeyDown(set) && !keyboard.IsKeyDown(unset) && player2Controlls == controllType || (player2Controlls == Player.EPlayerControlls.none))
                isPressedP2 = false;
            if (!keyboard.IsKeyDown(set) && !keyboard.IsKeyDown(unset) && player3Controlls == controllType || (player3Controlls == Player.EPlayerControlls.none))
                isPressedP3 = false;
            if (!keyboard.IsKeyDown(set) && !keyboard.IsKeyDown(unset) && player4Controlls == controllType || (player4Controlls == Player.EPlayerControlls.none))
                isPressedP4 = false;

            if (keyboard.IsKeyDown(set) && player1Controlls != controllType && player2Controlls != controllType && player3Controlls != controllType && player4Controlls != controllType)
            {
                //Player setzen falls noch kein Player ControlType besitzt
                if (!player1ControllsLRS.isSelected() && player2Controlls != controllType && player3Controlls != controllType && player4Controlls != controllType && !isPressedP1)
                {
                    player1ControllsLRS.setSelectedKlick();
                    while(player1ControllsLRS.getDisplayedIndex() != (int)controllType)
                        player1ControllsLRS.switchRight();
                    player1Controlls = controllType;
                    isPressedP1 = true;
                }
                if (!player2ControllsLRS.isSelected() && player1Controlls != controllType && player3Controlls != controllType && player4Controlls != controllType && !isPressedP2)
                {
                    player2ControllsLRS.setSelectedKlick();
                    while (player2ControllsLRS.getDisplayedIndex() != (int)controllType)
                        player2ControllsLRS.switchRight();
                    player2Controlls = controllType;
                    isPressedP2 = true;
                }
                if (!player3ControllsLRS.isSelected() && player1Controlls != controllType && player2Controlls != controllType && player4Controlls != controllType && !isPressedP3)
                {
                    player3ControllsLRS.setSelectedKlick();
                    while (player3ControllsLRS.getDisplayedIndex() != (int)controllType)
                        player3ControllsLRS.switchRight();
                    player3Controlls = controllType;
                    isPressedP3 = true;
                }
                if (!player4ControllsLRS.isSelected() && player1Controlls != controllType && player2Controlls != controllType && player3Controlls != controllType && !isPressedP4)
                {
                    player4ControllsLRS.setSelectedKlick();
                    while (player4ControllsLRS.getDisplayedIndex() != (int)controllType)
                        player4ControllsLRS.switchRight();
                    player4Controlls = controllType;
                    isPressedP4 = true;
                }
            }
            //player ready setzen
            if (keyboard.IsKeyDown(set) && player1Controlls == controllType && !isPressedP1)
            {
                isPressedP1 = true;
                player1Icon.setSelected();
            }
            if (keyboard.IsKeyDown(set) && player2Controlls == controllType && !isPressedP2)
            {
                isPressedP2 = true;
                player2Icon.setSelected();
            }
            if (keyboard.IsKeyDown(set) && player3Controlls == controllType && !isPressedP3)
            {
                isPressedP3 = true;
                player3Icon.setSelected();
            }
            if (keyboard.IsKeyDown(set) && player4Controlls == controllType && !isPressedP4)
            {
                isPressedP4 = true;
                player4Icon.setSelected();
            }


            if (keyboard.IsKeyDown(unset) &&
                ((!player1Icon.isSelected() && player1Controlls == controllType)
                || (!player2Icon.isSelected() && player2Controlls == controllType)
                || (!player3Icon.isSelected() && player3Controlls == controllType)
                || (!player4Icon.isSelected() && player4Controlls == controllType)))
            {
                //Player zurück setzen 
                if (player1ControllsLRS.isSelected() && player1Controlls == controllType && !isPressedP1)
                {
                    player1ControllsLRS.setNotSelected();
                    player1Controlls = Player.EPlayerControlls.none;
                    while (player1ControllsLRS.getDisplayedIndex() != (int)controllType)
                        player1ControllsLRS.switchLeft();
                    isPressedP1 = true;
                }
                if (player2ControllsLRS.isSelected() && player2Controlls == controllType && !isPressedP2)
                {
                    player2ControllsLRS.setNotSelected();
                    player2Controlls = Player.EPlayerControlls.none;
                    while (player2ControllsLRS.getDisplayedIndex() != (int)controllType)
                        player2ControllsLRS.switchLeft();
                    isPressedP2 = true;
                }
                if (player3ControllsLRS.isSelected() && player3Controlls == controllType && !isPressedP3)
                {
                    player3ControllsLRS.setNotSelected();
                    player3Controlls = Player.EPlayerControlls.none;
                    while (player3ControllsLRS.getDisplayedIndex() != (int)controllType)
                        player3ControllsLRS.switchLeft();
                    isPressedP3 = true;
                }
                if (player4ControllsLRS.isSelected() && player4Controlls == controllType && !isPressedP4)
                {
                    player4ControllsLRS.setNotSelected();
                    player4Controlls = Player.EPlayerControlls.none;
                    while (player4ControllsLRS.getDisplayedIndex() != (int)controllType)
                        player4ControllsLRS.switchLeft();
                    isPressedP4 = true;
                }
            }

            //alle player auf unready setzen
            if (keyboard.IsKeyDown(unset) && player1Controlls == controllType && !isPressedP1)
            {
                isPressedP1 = true;
                player1Icon.setNotSelected();
            }
            if (keyboard.IsKeyDown(unset) && player2Controlls == controllType && !isPressedP2)
            {
              isPressedP2 = true;
              player2Icon.setNotSelected();
            }
            if (keyboard.IsKeyDown(unset) && player3Controlls == controllType && !isPressedP3)
            {
                isPressedP3 = true;
                player3Icon.setNotSelected();
            }
            if (keyboard.IsKeyDown(unset) && player4Controlls == controllType && !isPressedP4)
            {
                isPressedP4 = true;
                player4Icon.setNotSelected();
            }

        }

        private void updateColtrolls(GamePadState currentState, Player.EPlayerControlls controllType)
        {
            //schauen ob das GamePad überhaupt verbunden ist
            if (currentState.IsConnected)
            {
                //set isPressed = false
                if (!currentState.IsButtonDown(Buttons.A) && !currentState.IsButtonDown(Buttons.B) && (player1Controlls == controllType || player1Controlls == Player.EPlayerControlls.none))
                    isPressedP1 = false;
                if (!currentState.IsButtonDown(Buttons.A) && !currentState.IsButtonDown(Buttons.B) && (player2Controlls == controllType || player2Controlls == Player.EPlayerControlls.none))
                    isPressedP2 = false;
                if (!currentState.IsButtonDown(Buttons.A) && !currentState.IsButtonDown(Buttons.B) && (player3Controlls == controllType || player3Controlls == Player.EPlayerControlls.none))
                    isPressedP3 = false;
                if (!currentState.IsButtonDown(Buttons.A) && !currentState.IsButtonDown(Buttons.B) && (player4Controlls == controllType || player4Controlls == Player.EPlayerControlls.none))
                    isPressedP4 = false;

                if (currentState.IsButtonDown(Buttons.A) && player1Controlls != controllType && player2Controlls != controllType && player3Controlls != controllType && player4Controlls != controllType)
                {
                    //Player setzen falls noch kein Player ControlType besitzt
                    if (!player1ControllsLRS.isSelected() && player2Controlls != controllType && player3Controlls != controllType && player4Controlls != controllType && !isPressedP1)
                    {
                        player1ControllsLRS.setSelectedKlick();
                        while (player1ControllsLRS.getDisplayedIndex() != (int)controllType)
                            player1ControllsLRS.switchRight();
                        player1Controlls = controllType;
                        isPressedP1 = true;
                    }
                    if (!player2ControllsLRS.isSelected() && player1Controlls != controllType && player3Controlls != controllType && player4Controlls != controllType && !isPressedP2)
                    {
                        player2ControllsLRS.setSelectedKlick();
                        while (player2ControllsLRS.getDisplayedIndex() != (int)controllType)
                            player2ControllsLRS.switchRight();
                        player2Controlls = controllType;
                        isPressedP2 = true;
                    }
                    if (!player3ControllsLRS.isSelected() && player1Controlls != controllType && player2Controlls != controllType && player4Controlls != controllType && !isPressedP3)
                    {
                        player3ControllsLRS.setSelectedKlick();
                        while (player3ControllsLRS.getDisplayedIndex() != (int)controllType)
                            player3ControllsLRS.switchRight();
                        player3Controlls = controllType;
                        isPressedP3 = true;
                    }
                    if (!player4ControllsLRS.isSelected() && player1Controlls != controllType && player2Controlls != controllType && player3Controlls != controllType && !isPressedP4)
                    {
                        player4ControllsLRS.setSelectedKlick();
                        while (player4ControllsLRS.getDisplayedIndex() != (int)controllType)
                            player4ControllsLRS.switchRight();
                        player4Controlls = controllType;
                        isPressedP4 = true;
                    }
                }
                //player ready setzen
                if (currentState.IsButtonDown(Buttons.A) && player1Controlls == controllType && !isPressedP1)
                {
                    isPressedP1 = true;
                    player1Icon.setSelected();
                }
                if (currentState.IsButtonDown(Buttons.A) && player2Controlls == controllType && !isPressedP2)
                {
                    isPressedP2 = true;
                    player2Icon.setSelected();
                }
                if (currentState.IsButtonDown(Buttons.A) && player3Controlls == controllType && !isPressedP3)
                {
                    isPressedP3 = true;
                    player3Icon.setSelected();
                }
                if (currentState.IsButtonDown(Buttons.A) && player4Controlls == controllType && !isPressedP4)
                {
                    isPressedP4 = true;
                    player4Icon.setSelected();
                }


                if (currentState.IsButtonDown(Buttons.B) &&
                    ((!player1Icon.isSelected() && player1Controlls == controllType)
                    || (!player2Icon.isSelected() && player2Controlls == controllType)
                    || (!player3Icon.isSelected() && player3Controlls == controllType)
                    || (!player4Icon.isSelected() && player4Controlls == controllType)))
                {
                    //Player zurück setzen 
                    if (player1ControllsLRS.isSelected() && player1Controlls == controllType && !isPressedP1)
                    {
                        player1ControllsLRS.setNotSelected();
                        player1Controlls = Player.EPlayerControlls.none;
                        while (player1ControllsLRS.getDisplayedIndex() != (int)controllType)
                            player1ControllsLRS.switchLeft();
                        isPressedP1 = true;
                    }
                    if (player2ControllsLRS.isSelected() && player2Controlls == controllType && !isPressedP2)
                    {
                        player2ControllsLRS.setNotSelected();
                        player2Controlls = Player.EPlayerControlls.none;
                        while (player2ControllsLRS.getDisplayedIndex() != (int)controllType)
                            player2ControllsLRS.switchLeft();
                        isPressedP2 = true;
                    }
                    if (player3ControllsLRS.isSelected() && player3Controlls == controllType && !isPressedP3)
                    {
                        player3ControllsLRS.setNotSelected();
                        player3Controlls = Player.EPlayerControlls.none;
                        while (player3ControllsLRS.getDisplayedIndex() != (int)controllType)
                            player3ControllsLRS.switchLeft();
                        isPressedP3 = true;
                    }
                    if (player4ControllsLRS.isSelected() && player4Controlls == controllType && !isPressedP4)
                    {
                        player4ControllsLRS.setNotSelected();
                        player4Controlls = Player.EPlayerControlls.none;
                        while (player4ControllsLRS.getDisplayedIndex() != (int)controllType)
                            player4ControllsLRS.switchLeft();
                        isPressedP4 = true;
                    }
                }

                //alle player auf unready setzen
                if (currentState.IsButtonDown(Buttons.B) && player1Controlls == controllType && !isPressedP1)
                {
                    isPressedP1 = true;
                    player1Icon.setNotSelected();
                }
                if (currentState.IsButtonDown(Buttons.B) && player2Controlls == controllType && !isPressedP2)
                {
                    isPressedP2 = true;
                    player2Icon.setNotSelected();
                }
                if (currentState.IsButtonDown(Buttons.B) && player3Controlls == controllType && !isPressedP3)
                {
                    isPressedP3 = true;
                    player3Icon.setNotSelected();
                }
                if (currentState.IsButtonDown(Buttons.B) && player4Controlls == controllType && !isPressedP4)
                {
                    isPressedP4 = true;
                    player4Icon.setNotSelected();
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
            //updateColtrolls(Keys.G, Keys.T, Player.EPlayerControlls.Keyboard2);
            //updateColtrolls(Keys.K, Keys.I, Player.EPlayerControlls.Keyboard3);
            //updateColtrolls(Keys.NumPad2, Keys.NumPad5, Player.EPlayerControlls.KeyboardNumPad);
            updateColtrolls(Keys.K, Keys.I, Player.EPlayerControlls.KeyboardNumPad);

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
           // spaceNote.draw();
            escapeNote.draw();
            keyboard1.draw();
            keyboard2.draw();
            gamepad.draw();
            
            if(GameModeSelected.getDisplayedIndex() == 1)
                rushHour.draw();

            if (GameModeSelected.getDisplayedIndex() == 0)
                needForIngredients.draw();

            if (startGame)
                infoText.draw();

        }



    }
}
