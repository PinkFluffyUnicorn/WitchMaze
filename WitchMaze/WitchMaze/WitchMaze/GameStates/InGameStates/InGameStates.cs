using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WitchMaze.ItemStuff.Items;
using WitchMaze.MapStuff;
using WitchMaze.ItemStuff;

namespace WitchMaze.GameStates
{
    abstract class InGameState
    {

        static protected MapCreator mapCreator;
        static protected Map map;

        static protected ItemMap itemMap;
        static protected ItemSpawner itemSpawner;

        public static Map getMap() { return map; }
        public static ItemMap getItemMap() { return itemMap; }
        public static ItemSpawner getItemSpawner() { return itemSpawner; }
        //vllt ItemSpawner, Map, ect schon hier Updaten und Initialisieren, einfacher für folgende GameStates





        //standart klassen zum überschreiben
        public abstract void initialize();

        public abstract void loadContent();

        public abstract void unloadContent();

        public abstract EInGameState update(GameTime gameTime);

        public abstract void Draw(GameTime gameTime);
    }

    public enum EInGameState
    {
        SingleTime,
        MultiTime,
        MultiNotTime, //dunno good name
        ExitInGame,
        ExitGame,
        GameWon,
    }
}
