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

namespace WitchMaze.MapStuff.Blocks
{
    class BlackHole : Block
    {

        public VertexPositionColor[] plane;

        Model model;

        public BlackHole(Vector3 _position/*, Color color*/, Model _model)
        {
            /*plane = new VertexPositionColor[4];
            plane[0] = new VertexPositionColor(position + new Vector3(Settings.blockSizeX / 2, 0f, Settings.blockSizeZ / 2f), color);
            plane[1] = new VertexPositionColor(position + new Vector3(Settings.blockSizeX / 2, 0f, -1 * Settings.blockSizeZ / 2f), color);
            plane[2] = new VertexPositionColor(position + new Vector3(-1 * Settings.blockSizeX / 2, 0f, Settings.blockSizeZ / 2f), color);
            plane[3] = new VertexPositionColor(position + new Vector3(-1 * Settings.blockSizeX / 2f, 0f, -1 * Settings.blockSizeZ / 2f), color);*/
            model = _model;
            position = _position;
            transportable = true;
            walkable = true;
        }
        public override void draw(GameTime gameTime)
        {
           /* Game1.getEffect().World = Matrix.Identity;
            Game1.getGraphics().GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleStrip, plane, 0, 2);*/
            model.Draw(Matrix.CreateScale(0.05f) * Matrix.CreateScale(Settings.blockSizeX, 1, Settings.blockSizeZ) * Matrix.CreateTranslation(position), Player.Player.getCamera(), Player.Player.getProjection());
            //Game1.getEffect().World = Matrix.Identity;
            Game1.getEffect().CurrentTechnique.Passes[0].Apply();
        }

        
    }
}
