using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WitchMaze.InterfaceObjects
{
    class Text : InterfaceObject
    {                
        SpriteBatch spriteBatch;
        SpriteFont Font1;
        string text;

        /// <summary>
        /// creates a Text displayed on the screen
        /// </summary>
        /// <param name="_text">the text do be displayed</param>
        /// <param name="_position">the position the text should be displayed</param>
        public Text(string _text, Vector2 _position)
        {
            text = _text;
            position = _position;

            spriteBatch = new SpriteBatch(Game1.getGraphics().GraphicsDevice);
            Font1 = Game1.getContent().Load<SpriteFont>("Font/Courier New");

            // TODO: Load your game content here            
            //fontPos = new Vector2(Game1.getGraphics().GraphicsDevice.Viewport.Width / 2,
            //    Game1.getGraphics().GraphicsDevice.Viewport.Height / 2);
        }

        /// <summary>
        /// updates the text so that a other Text is Displayed
        /// </summary>
        /// <param name="_text">new Text</param>
        public void updateText(string _text)
        {
            text = _text;
        }

        public override float getHeight()
        {
            return Font1.MeasureString(text).Y;
        }

        public override float getWidth()
        {
            return Font1.MeasureString(text).X;
        }

        /// <summary>
        /// draws the Text
        /// </summary>
        public override void draw()
        {
            spriteBatch.Begin();

            //Vector2 FontOrigin = Font1.MeasureString(text) / 2; //punkt in der mitte

            spriteBatch.DrawString(Font1, text, position, Settings.acaOrange,
                0, position, scale, SpriteEffects.None, 0.5f);

            spriteBatch.End();
        }
    }
}
