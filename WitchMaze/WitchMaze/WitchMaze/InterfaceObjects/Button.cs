using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WitchMaze.InterfaceObjects
{
    class Button : InterfaceObject
    {
        Texture2D buttonTextureNotSelected;
        Texture2D buttonTextureSelected;
        SpriteBatch spriteBatch;
        bool selected;

        /// <summary>
        /// Creates a Button that is Selectable
        /// </summary>
        /// <param name="_position">the position the Button should be</param>
        /// <param name="texturePathNotSelected">the not selected texturePath of the Button</param>
        /// <param name="TexturePathSelected">the selected texturePath of the Button</param>
        public Button(Vector2 _position, String texturePathNotSelected, String texturePathSelected)
        {
            position = _position;
            buttonTextureNotSelected = Game1.getContent().Load<Texture2D>(texturePathNotSelected);
            buttonTextureSelected = Game1.getContent().Load<Texture2D>(texturePathSelected);
            spriteBatch = new SpriteBatch(Game1.getGraphics().GraphicsDevice);
            selected = false;
        }

        /// <summary>
        /// sets the Button to Selected
        /// </summary>
        public void setSelected() 
        {
            selected = true;
        }
        /// <summary>
        /// sets the Button to not selected
        /// </summary>
        public void setNotSelected()
        {
            selected = false;
        }
        /// <summary>
        /// returns the state of the Button, if selected or not
        /// </summary>
        /// <returns>true if selected, false if not</returns>
        public bool isSelected()
        {
            return selected;
        }

        /// <summary>
        /// Returns the Width of the Button
        /// </summary>
        /// <returns></returns>
        public override float getWidth()
        {
            if(selected)
                return buttonTextureSelected.Width * scale;
            else
                return buttonTextureNotSelected.Width * scale;
        }
        /// <summary>
        /// Returns the Height of the Button
        /// </summary>
        /// <returns></returns>
        public override float getHeight()
        {
            if (selected)
                return buttonTextureSelected.Height * scale;
            else
                return buttonTextureNotSelected.Height * scale;
        }
        /// <summary>
        /// draws the Button
        /// </summary>
        public override void draw()
        {
            if (selected)
            {
                spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
                spriteBatch.Draw(buttonTextureSelected, position, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                spriteBatch.End();
            }
            else
            {//!selected
                spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
                spriteBatch.Draw(buttonTextureNotSelected, position, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                spriteBatch.End();
            }
        }
    }
}
