using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WitchMaze.ItemStuff;
using WitchMaze.MapStuff;
using WitchMaze.PlayerStuff;

namespace WitchMaze.GameStates.InGameStates
{
    class MazeRun : InGameState
    {
        //Spielmodus in dem man durch ein Labyrinth rennen muss und Items einsammeln kann, siegbedinungen können später so hinzugefügt werden(mach ne eigenne klasse dafür)
        //public MazeRun(Player player1, Player player2, Player player3, Player player4)
        //{

        //}
        //public MazeRun(Player player1, Player player2, Player player3)
        //{

        //}
        //public MazeRun(Player player1, Player player2)
        //{

        //}
        //public MazeRun(Player player1)
        //{

        //}

        public MazeRun(List<Player> playerList)
        {

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

            //foreach(Player player in playerList){
            //    player.update(gameTime);
            //}
            itemSpawner.update(itemMap, gameTime);
            //if(notWon)
            if (timer >= 60000)//für 60 sekunden
                return EInGameState.Exit;
            //if(won)
            //if (player1.getItemCount() >= 10)
            //    return EInGameState.Exit;
            return EInGameState.MazeRun;
        }

        public override void Draw(GameTime gameTime)
        {
            //foreach (Player player in playerList)
            //{
            //    player.draw(gameTime);
            //}
            itemMap.draw();
            map.draw();
        }
    }
}
