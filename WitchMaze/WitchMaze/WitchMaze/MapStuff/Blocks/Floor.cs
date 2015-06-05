using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace WitchMaze.MapStuff.Blocks
{
    class Floor : Block
    {
        public VertexPositionColor[] plane;

        public Floor(Vector3 position, Color color)
        {
            plane = new VertexPositionColor[4];
            plane[0] = new VertexPositionColor(position + new Vector3(Settings.blockSizeX / 2, 0f, Settings.blockSizeZ / 2f), color);
            plane[1] = new VertexPositionColor(position + new Vector3(Settings.blockSizeX / 2, 0f, -1 * Settings.blockSizeZ / 2f), color);
            plane[2] = new VertexPositionColor(position + new Vector3(-1 * Settings.blockSizeX / 2, 0f, Settings.blockSizeZ / 2f), color);
            plane[3] = new VertexPositionColor(position + new Vector3(-1 * Settings.blockSizeX / 2f, 0f, -1 * Settings.blockSizeZ / 2f), color);

        }


        public override void draw(GameTime gameTime)
        {
            Game1.getGraphics().GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleStrip, plane, 0, 2);
        }
    }
}
