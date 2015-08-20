using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WitchMaze.ItemStuff.Items;
using WitchMaze.MapStuff;
using WitchMaze.ItemStuff;
using WitchMaze.InterfaceObjects;

namespace WitchMaze.GameStates
{
    abstract class InGameState
    {

        protected List<PlayerStuff.Player> playerList; public List<PlayerStuff.Player> getPlayerList() { return playerList; }//here to create the player in CharacterSelection and avalable for the in game

        static protected MapCreator mapCreator;
        static protected Map map;

        static protected ItemMap itemMap;
        static protected ItemSpawner itemSpawner;

        protected Minimap minimap;

        public static Map getMap() { return map; }
        public static ItemMap getItemMap() { return itemMap; }
        public static ItemSpawner getItemSpawner() { return itemSpawner; }
        //vllt ItemSpawner, Map, ect schon hier Updaten und Initialisieren, einfacher für folgende GameStates


        public virtual void initialize() { }

        public virtual void loadContent() { }

        public virtual void unloadContent() { }

        public virtual EInGameState update(GameTime gameTime) { throw new NotImplementedException(); }

        public virtual void Draw(GameTime gameTime) { }
    }

    public enum EInGameState
    {
        CharacterSelection,
        SingleTime,//test
        MazeRun,
        Rumble,
        Exit,
    }

    public enum EWinCondition
    {
        RushHour,
        NeedForIngrediance,
    }
}
