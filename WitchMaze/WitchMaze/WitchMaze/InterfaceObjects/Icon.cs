﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WitchMaze.InterfaceObjects
{
    class Icon : InterfaceObject
    {
        Texture2D iconTexture;
        SpriteBatch spriteBatch;

        /// <summary>
        /// Creates a simple Icon
        /// </summary>
        /// <param name="_position">position the Icon should be</param>
        /// <param name="texturePath">texturePath for the icon</param>
        public Icon(Vector2 _position, String texturePath)
        {
            position = _position;
            iconTexture = Game1.getContent().Load<Texture2D>(texturePath);
            spriteBatch = new SpriteBatch(Game1.getGraphics().GraphicsDevice);
        }

        /// <summary>
        /// returns Icon Width
        /// </summary>
        /// <returns></returns>
        public override float getWidth()
        {
            return iconTexture.Width * globalScale * individualScale;
        }
        /// <summary>
        /// returns Icon Height
        /// </summary>
        /// <returns></returns>
        public override float getHeight()
        {
            return iconTexture.Height * globalScale * individualScale;
        }
        /// <summary>
        /// draws the Icon
        /// </summary>
        public override void draw()
        {
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            spriteBatch.Draw(iconTexture, position, null, Color.White, 0f, Vector2.Zero, globalScale * individualScale, SpriteEffects.None, 0f);
            spriteBatch.End();        
        }
    }
}
