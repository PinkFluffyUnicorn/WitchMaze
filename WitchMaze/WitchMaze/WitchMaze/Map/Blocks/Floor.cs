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

namespace WitchMaze.Map.Blocks
{
    class Floor : Block
    {
        public 

        VertexPositionColor[] plane;

        public override void initialize()
        {
            plane = new VertexPositionColor[4];
            plane[0] = new VertexPositionColor(base.position + new Vector3(Settimgs.blockSizeX / 2, 0f, Settimgs.blockSizeZ / 2f), Settimgs.floorColor);
            plane[1] = new VertexPositionColor(base.position + new Vector3(Settimgs.blockSizeX / 2, 0f, -1 * Settimgs.blockSizeZ / 2f), Settimgs.floorColor);
            plane[2] = new VertexPositionColor(base.position + new Vector3(-1 * Settimgs.blockSizeX / 2, 0f, Settimgs.blockSizeZ / 2f), Settimgs.floorColor);
            plane[3] = new VertexPositionColor(base.position + new Vector3(-1 * Settimgs.blockSizeX / 2f, 0f, -1 * Settimgs.blockSizeZ / 2f), Settimgs.floorColor);
        }

        public override void draw()
        {
            GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleStrip, plane, 0, 2);
        }
    }
}
