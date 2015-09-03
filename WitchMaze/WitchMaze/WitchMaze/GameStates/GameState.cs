using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using WitchMaze.GameStates;
using WitchMaze.ownFunctions;

namespace WitchMaze
{

    abstract class GameState
    {
        protected List<PlayerStuff.Player> playerList= new List<PlayerStuff.Player>();
        public List<PlayerStuff.Player> getPlayerList() { return playerList; }
        public EInGameState eInGameState;

        public abstract void initialize();

        public abstract void loadContent();

        public abstract void unloadContent();

        public abstract EGameState update(ownGameTime gameTime);

        public abstract void Draw();
    }

    public enum EGameState
    {
        MainMenu,
        CharacterSelection,
        Credits,
        Options,
        Help,
        Exit,
        InGame,
    }
}
