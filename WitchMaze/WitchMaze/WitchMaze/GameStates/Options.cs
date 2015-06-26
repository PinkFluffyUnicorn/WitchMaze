using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace WitchMaze.GameStates
{
    class Options : GameState //, Microsoft.Xna.Framework.Game
    {
        //crappy fix
        public void initialize() { throw new NotImplementedException(); }

        public void loadContent() { throw new NotImplementedException(); }

        public void unloadContent() { throw new NotImplementedException(); }

        public EGameState update(GameTime gameTime) { throw new NotImplementedException(); }

        public void Draw(GameTime gameTime) { throw new NotImplementedException(); }

        //// Global variables
        //enum BState
        //{
        //    HOVER,
        //    UP,
        //    JUST_RELEASED,
        //    DOWN
        //}
        //const int NUMBER_OF_BUTTONS = 3,
        //    EASY_BUTTON_INDEX = 0,
        //    MEDIUM_BUTTON_INDEX = 1,
        //    HARD_BUTTON_INDEX = 2,
        //    BUTTON_HEIGHT = 40,
        //    BUTTON_WIDTH = 88;
        //Color background_color;
        //Color[] button_color = new Color[NUMBER_OF_BUTTONS];
        //Rectangle[] button_rectangle = new Rectangle[NUMBER_OF_BUTTONS];
        //BState[] button_state = new BState[NUMBER_OF_BUTTONS];
        //Texture2D[] button_texture = new Texture2D[NUMBER_OF_BUTTONS];
        //double[] button_timer = new double[NUMBER_OF_BUTTONS];
        ////mouse pressed and mouse just pressed
        //bool mpressed, prev_mpressed = false;
        ////mouse location in window
        //int mx, my;
        //double frame_time;

        //public void initialize()
        //{
        //    // starting x and y locations to stack buttons 
        //    // vertically in the middle of the screen
        //    int x = Window.ClientBounds.Width / 2 - BUTTON_WIDTH / 2;
        //    int y = Window.ClientBounds.Height / 2 -
        //        NUMBER_OF_BUTTONS / 2 * BUTTON_HEIGHT -
        //        (NUMBER_OF_BUTTONS % 2) * BUTTON_HEIGHT / 2;
        //    for (int i = 0; i < NUMBER_OF_BUTTONS; i++)
        //    {
        //        button_state[i] = BState.UP;
        //        button_color[i] = Color.White;
        //        button_timer[i] = 0.0;
        //        button_rectangle[i] = new Rectangle(x, y, BUTTON_WIDTH, BUTTON_HEIGHT);
        //        y += BUTTON_HEIGHT;
        //    }
        //    IsMouseVisible = true;
        //    background_color = Color.CornflowerBlue;
        //}

        //public void loadContent()
        //{
        //    button_texture[ON_BUTTON_INDEX] =
        //       Game1.getContent().Load<Texture2D>("option/onButton");
        //    button_texture[OFF_BUTTON_INDEX] =
        //        Game1.getContent().Load<Texture2D>("option/offButton");
        //}

        //public void unloadContent()
        //{
        //    throw new NotImplementedException();
        //}

        //public EGameState update(Microsoft.Xna.Framework.GameTime gameTime)
        //{
        //    throw new NotImplementedException();
        //}

        //public void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
