﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WitchMaze.ItemStuff;
using WitchMaze.MapStuff;
using WitchMaze.PlayerStuff;
using WitchMaze.InterfaceObjects;

namespace WitchMaze.GameStates.InGameStates
{
    class MazeRun : InGameState
    {
        //Spielmodus in dem man durch ein Labyrinth rennen muss und Items einsammeln kann, siegbedinungen können später so hinzugefügt werden(mach ne eigenne klasse dafür)

        public MazeRun(List<Player> _playerList)
        {
            playerList = _playerList;
        }


        //man hat 60 sekunden um 10 Items zu sammeln, schafft man es gewinnt man, schafft man es nicht so verliert man
        float timer;

        public override void initialize()
        {
            mapCreator = new MapCreator();
            mapCreator.initialize();
            map = mapCreator.generateMap();

            itemMap = new ItemMap();
            itemSpawner = new ItemSpawner();
            itemSpawner.initialSpawn(itemMap);

            initializePlayer();

            minimap = new Minimap(new Vector2(0, 0), map);
            if (playerList.Count == 1)
                minimap.setPosition(new Vector2(Settings.getResolutionX() - minimap.getWidth(), 0));
            else
                minimap.setPosition(new Vector2(Settings.getResolutionX() / 2 - minimap.getWidth() / 2, Settings.getResolutionY() / 2 - minimap.getHeight() / 2));
            

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
            Vector3 spawnPosition1 = new Vector3(5,1,5);
            Vector3 spawnPosition2 = new Vector3(5, 1, 5);
            Vector3 spawnPosition3 = new Vector3(5, 1,5);
            Vector3 spawnPosition4 = new Vector3(5, 1, 5);
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
                    player2.setFinalPlayer(spawnPosition1, Player.EPlayerViewportPosition.right);
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
                    player2.setFinalPlayer(spawnPosition1, Player.EPlayerViewportPosition.botLeft);
                    player3 = playerList.First();
                    playerList.RemoveAt(0);
                    player3.setFinalPlayer(spawnPosition1, Player.EPlayerViewportPosition.topRight);
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
                    player2.setFinalPlayer(spawnPosition1, Player.EPlayerViewportPosition.botLeft);
                    player3 = playerList.First();
                    playerList.RemoveAt(0);
                    player3.setFinalPlayer(spawnPosition1, Player.EPlayerViewportPosition.topRight);
                    player4 = playerList.First();
                    playerList.RemoveAt(0);
                    player4.setFinalPlayer(spawnPosition1, Player.EPlayerViewportPosition.botRight);
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

        public override void loadContent()
        {

        }

        public override void unloadContent()
        {

        }

        public override EInGameState update(GameTime gameTime)
        {
            timer += gameTime.ElapsedGameTime.Milliseconds; //1s = 1000ms

            foreach(Player player in playerList){
                player.update(gameTime);
            }
            itemSpawner.update(itemMap, gameTime);
            minimap.update(itemMap, playerList);
            //if(notWon)
            //if (timer >= 60000)//für 60 sekunden
            //    return EInGameState.Exit;
            //if(won)
            //if (player1.getItemCount() >= 10)
            //    return EInGameState.Exit;
            return EInGameState.MazeRun;
        }

        public override void Draw(GameTime gameTime)
        {
            Viewport defaultViewport = Game1.getGraphics().GraphicsDevice.Viewport;
            //Game1.getGraphics().GraphicsDevice.Clear(Color.CornflowerBlue);
            int count = 0;
            foreach (Player player in playerList)
            {
                foreach (Player p in playerList) {
                    if (p == player)
                        p.draw();
                }
                count++;//gibt player an in dem es fehler gibt;
                Viewport playerViewport = player.getViewport();
                Game1.getGraphics().GraphicsDevice.Viewport = player.getViewport();
                player.doStuff();
                itemMap.draw(player.getProjection(), player.getCamera());
                map.draw(player.getProjection(), player.getCamera());
            }
            Game1.getGraphics().GraphicsDevice.Viewport = defaultViewport;
            minimap.draw();

        }
    }
}