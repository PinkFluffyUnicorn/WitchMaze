﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
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
        /// sets the Button to Selected with a klick if it was not selected bevore
        /// </summary>
        public void setSelectedKlicked() 
        {
            if(!selected)
                Game1.sounds.klick.Play(Settings.getSoundVolume(), 0, 0);
            selected = true;
        }

        /// <summary>
        /// sets a button silently to selected
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
                return buttonTextureSelected.Width * globalScale * individualScale;
            else
                return buttonTextureNotSelected.Width * globalScale * individualScale;
        }
        /// <summary>
        /// Returns the Height of the Button
        /// </summary>
        /// <returns></returns>
        public override float getHeight()
        {
            if (selected)
                return buttonTextureSelected.Height * globalScale * individualScale;
            else
                return buttonTextureNotSelected.Height * globalScale * individualScale;
        }
        /// <summary>
        /// draws the Button
        /// </summary>
        public override void draw()
        {
            if (selected)
            {
                spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
                spriteBatch.Draw(buttonTextureSelected, position, null, Color.White, 0f, Vector2.Zero, globalScale * individualScale, SpriteEffects.None, 0f);
                spriteBatch.End();
            }
            else
            {//!selected
                spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
                spriteBatch.Draw(buttonTextureNotSelected, position, null, Color.White, 0f, Vector2.Zero, globalScale * individualScale, SpriteEffects.None, 0f);
                spriteBatch.End();
            }
        }
    }
}
