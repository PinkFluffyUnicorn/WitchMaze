﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WitchMaze.ItemStuff.Items;
using WitchMaze.MapStuff;
using WitchMaze.ItemStuff;
using WitchMaze.InterfaceObjects;
using WitchMaze.PlayerStuff;
using Microsoft.Xna.Framework.Media;
using WitchMaze.ownFunctions;
using Microsoft.Xna.Framework.Input;
using WitchMaze.PlayerStuff;

namespace WitchMaze.GameStates
{
    abstract class InGameState
    {
        KeyboardState keyboard;

        protected EInGameState currentInGameState; // has tu de set in the inGameState(MazeRun ect...)

        protected List<PlayerStuff.Player> playerList; 
        public  List<PlayerStuff.Player> getPlayerList() { return playerList; }//here to create the player in CharacterSelection and avalable for the in game

        static protected MapCreator mapCreator;
        static protected Map map;

        static protected ItemMap itemMap;
        static protected ItemSpawner itemSpawner;

        protected Minimap minimap;

        public static Map getMap() { return map; }
        public static ItemMap getItemMap() { return itemMap; }
        public static ItemSpawner getItemSpawner() { return itemSpawner; }

        /// <summary>
        /// Initializes the Map, Items, the Player, the PlayerInterface and the Minimap
        /// </summary>
        public virtual void initialize() 
        {
            mapCreator = new MapCreator();
            mapCreator.initialize();
            map = mapCreator.generateMap();

            itemMap = new ItemMap();
            itemSpawner = new ItemSpawner();
            itemSpawner.initialSpawn(itemMap, map, playerList);

            initializePlayer();

            minimap = new Minimap(new Vector2(0, 0), map);
            if (playerList.Count == 1)
                minimap.setPosition(new Vector2(Settings.getResolutionX() - minimap.getWidth(), 0));
            else
                minimap.setPosition(new Vector2(Settings.getResolutionX() / 2 - minimap.getWidth() / 2, Settings.getResolutionY() / 2 - minimap.getHeight() / 2));

            Game1.sounds.menuSound.Stop();
            Game1.sounds.inGameSound.Play();
        }

        public virtual void loadContent()
        {

        }

        public virtual void unloadContent() { }

        /// <summary>
        /// Updates Player, ItemMap and Minimap
        /// </summary>
        /// <param name="gameTime">GameTime</param>
        /// <returns>State of the Game</returns>
        public virtual EInGameState update(ownGameTime gameTime) 
        {
            keyboard = Keyboard.GetState();
            foreach (Player player in playerList)
            {
                player.update(gameTime);
                //are player colliding?
                foreach (Player p in playerList)
                {
                    if(p != player){
                        if (ownFunctions.Collision.circleCirlceCollision(new Vector2(p.getPosition().X, p.getPosition().Z), p.getRadius(), new Vector2(player.getPosition().X, player.getPosition().Z), player.getRadius()))
                        {
                            player.bounce(player.getPosition() - p.getPosition());
                            Game1.sounds.bounce.Play(Settings.getSoundVolume(), -1f, p.pan);

                        }
                        else
                        {

                        }
                    }
                }

                //delete all blocks but black holes
                List<MapStuff.Blocks.BlackHole> blackHoles = new List<MapStuff.Blocks.BlackHole>();
                List<MapStuff.Blocks.Block> blocksStandingOn = player.mapBlocksStandingOn(player.getPosition());
                foreach (MapStuff.Blocks.Block block in blocksStandingOn)
                {
                    if (block.name == MapStuff.MapCreator.tiles.blackhole)
                        blackHoles.Add((MapStuff.Blocks.BlackHole)block);
                }
                //reset player position for black holes
                foreach (MapStuff.Blocks.BlackHole blackHole in blackHoles)
                {
                    if (blackHole.transportable)
                    {
                        Vector3 transportPosition = blackHole.transportPosition;
                        player.setPosition(new Vector3(transportPosition.X, player.getPosition().Y, transportPosition.Z));
                        bool canPort = true;
                        Game1.sounds.teleport.Play(Settings.getSoundVolume(), 0, player.pan);
                        //festbuggen ist grad möglich...
                        
                    }
                        
                }

            }
            itemSpawner.update(itemMap, gameTime, playerList);
            minimap.update(itemMap, playerList);

            if (currentInGameState == null)
                throw new IndexOutOfRangeException();
            return currentInGameState; 
        }

        /// <summary>
        /// Draws Player, Map, Items and Interface
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Draw() 
        {           

            Viewport defaultViewport = Game1.getGraphics().GraphicsDevice.Viewport;
            //Game1.getGraphics().GraphicsDevice.Clear(Color.CornflowerBlue);
            //int count = 0;
            foreach (Player player in playerList)
            {
                player.updateCamera();
                //Viewport playerViewport = player.getViewport();
                Game1.getGraphics().GraphicsDevice.Viewport = player.getViewport();
                player.getSkybox().draw(player.getCamera(), player.getProjection(), player.getPosition());
                foreach (Player p in playerList)
                {
                    if (p != player)
                    {
                        p.draw();
                    }
                }
                //count++;//gibt player an in dem es fehler gibt;
                itemMap.draw(player.getProjection(), player.getCamera());
                map.draw(player.getProjection(), player.getCamera());

            }
            Game1.getGraphics().GraphicsDevice.Viewport = defaultViewport;
            minimap.draw();
        }


        /// <summary>
        /// final initialization of the Player created in the Menu
        /// </summary>
        private void initializePlayer()
        {
            Player player1;
            Player player2;
            Player player3;
            Player player4;
            List<Vector3> position = mapCreator.startPositions(4);
            Vector3 spawnPosition1 = position.ElementAt<Vector3>(0);
            Vector3 spawnPosition2 = /*new Vector3(5,(float) 0.5, 5);*/position.ElementAt<Vector3>(1);
            Vector3 spawnPosition3 = position.ElementAt<Vector3>(2);
            Vector3 spawnPosition4 = position.ElementAt<Vector3>(3);
            switch (playerList.Count)
            {
                case 1:
                    player1 = playerList.First();
                    playerList.RemoveAt(0);
                    player1.setFinalPlayer(spawnPosition1, Player.EPlayerViewportPosition.fullscreen);
                    playerList.Clear();
                    playerList.Add(player1);
                    break;
                case 2:
                    player1 = playerList.First();
                    playerList.RemoveAt(0);
                    player1.setFinalPlayer(spawnPosition1, Player.EPlayerViewportPosition.left);
                    player2 = playerList.First();
                    playerList.RemoveAt(0);
                    player2.setFinalPlayer(spawnPosition2, Player.EPlayerViewportPosition.right);
                    playerList.Clear();
                    playerList.Add(player1);
                    playerList.Add(player2);
                    break;
                case 3:
                    player1 = playerList.First();
                    playerList.RemoveAt(0);
                    player1.setFinalPlayer(spawnPosition1, Player.EPlayerViewportPosition.topLeft);
                    player2 = playerList.First();
                    playerList.RemoveAt(0);
                    player2.setFinalPlayer(spawnPosition2, Player.EPlayerViewportPosition.botLeft);
                    player3 = playerList.First();
                    playerList.RemoveAt(0);
                    player3.setFinalPlayer(spawnPosition3, Player.EPlayerViewportPosition.topRight);
                    playerList.Clear();
                    playerList.Add(player1);
                    playerList.Add(player2);
                    playerList.Add(player3);
                    break;
                case 4:
                    player1 = playerList.First();
                    playerList.RemoveAt(0);
                    player1.setFinalPlayer(spawnPosition1, Player.EPlayerViewportPosition.topLeft);
                    player2 = playerList.First();
                    playerList.RemoveAt(0);
                    player2.setFinalPlayer(spawnPosition2, Player.EPlayerViewportPosition.botLeft);
                    player3 = playerList.First();
                    playerList.RemoveAt(0);
                    player3.setFinalPlayer(spawnPosition3, Player.EPlayerViewportPosition.topRight);
                    player4 = playerList.First();
                    playerList.RemoveAt(0);
                    player4.setFinalPlayer(spawnPosition4, Player.EPlayerViewportPosition.botRight);
                    playerList.Clear();
                    playerList.Add(player1);
                    playerList.Add(player2);
                    playerList.Add(player3);
                    playerList.Add(player4);
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
    }
    public enum EInGameState
    {
        NeedForIngrediance,//test
        RushHour,
        Rumble,
        Exit,
    }

    public enum EWinCondition
    {
        RushHour,
        NeedForIngrediance,
    }

}
