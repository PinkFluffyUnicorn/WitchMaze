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
    class Wall : Block
    {
        Model model;
         /*public VertexPositionColor[] planeBottom;
        public VertexPositionColor[] planeTop;
        public VertexPositionColor[] planeLeft;
        public VertexPositionColor[] planeRight;
        public VertexPositionColor[] planeFront;
        public VertexPositionColor[] planeBack;

        Color color;

        Vector3 BottomFrontLeft;
        Vector3 BottomFrontRight;
        Vector3 BottomBackLeft;
        Vector3 BottomBacktRight;

        Vector3 TopFrontLeft;
        Vector3 TopFrontRight;
        Vector3 TopBackLeft;
        Vector3 TopBackRight; */ 

        





        public Wall(Vector3 _position, Model _model)
        {

            model = _model;
            position = _position;
           /* color = Settings.WallColor;

            float hBSX = Settings.blockSizeX / 2.0f;// half Block Size in x-Direction
            float hBSY = Settings.blockSizeY / 2.0f;// half Block Size in y-Direction


            BottomFrontLeft = new Vector3(position.X - hBSX, 0 ,position.Y - hBSY);
            BottomFrontRight = new Vector3(position.X - hBSX, 0, position.Y + hBSY);
            BottomBackLeft = new Vector3(position.X + hBSX, 0, position.Y - hBSY);
            BottomBacktRight = new Vector3(position.X + hBSX, 0, position.Y + hBSY);

            TopFrontLeft = new Vector3(position.X - hBSX, 1, position.Y - hBSY);
            TopFrontRight = new Vector3(position.X - hBSX, 1, position.Y + hBSY);
            TopBackLeft = new Vector3(position.X + hBSX, 1, position.Y - hBSY);
            TopBackRight = new Vector3(position.X + hBSX, 1, position.Y + hBSY);


           

            planeBottom = new VertexPositionColor[4];
            planeBottom[0] = new VertexPositionColor(BottomFrontLeft, color);
            planeBottom[1] = new VertexPositionColor(BottomFrontRight, color);
            planeBottom[2] = new VertexPositionColor(BottomBackLeft, color);
            planeBottom[3] = new VertexPositionColor(BottomBacktRight, color);

             planeTop = new VertexPositionColor[4];
            planeTop[0] = new VertexPositionColor(TopFrontLeft, color);
            planeTop[1] = new VertexPositionColor(TopFrontRight, color);
            planeTop[2] = new VertexPositionColor(TopBackLeft, color);
            planeTop[3] = new VertexPositionColor(TopBackRight, color);

             planeLeft = new VertexPositionColor[4];
            planeLeft[0] = new VertexPositionColor(BottomFrontLeft, color);
            planeLeft[1] = new VertexPositionColor(BottomBackLeft, color);
            planeLeft[2] = new VertexPositionColor(TopFrontLeft, color);
            planeLeft[3] = new VertexPositionColor(TopBackLeft, color);

             planeRight = new VertexPositionColor[4];
            planeRight[0] = new VertexPositionColor(BottomFrontRight, color);
            planeRight[1] = new VertexPositionColor(BottomBacktRight, color);
            planeRight[2] = new VertexPositionColor(TopFrontRight, color);
            planeRight[3] = new VertexPositionColor(TopBackRight, color);

             planeFront = new VertexPositionColor[4];
            planeFront[0] = new VertexPositionColor(BottomFrontLeft, color);
            planeFront[1] = new VertexPositionColor(BottomFrontRight, color);
            planeFront[2] = new VertexPositionColor(TopFrontLeft, color);
            planeFront[3] = new VertexPositionColor(TopFrontRight, color);

            planeBack = new VertexPositionColor[4];
            planeBack[0] = new VertexPositionColor(BottomBackLeft, color);
            planeBack[1] = new VertexPositionColor(BottomBacktRight, color);
            planeBack[2] = new VertexPositionColor(TopBackLeft, color);
            planeBack[3] = new VertexPositionColor(TopBackRight, color);*/
            

        }
        
        public override void draw(GameTime gameTime)
        {
            
            model.Draw(Matrix.CreateScale(0.05f) * Matrix.CreateTranslation(position), Player.Player.getCamera(), Player.Player.getProjection());
            Game1.getEffect().World = Matrix.Identity;
            Game1.getEffect().CurrentTechnique.Passes[0].Apply();
            /*Game1.getGraphics().GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleStrip, planeBottom, 0, 2);
            Game1.getGraphics().GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleStrip, planeTop, 0, 2);
            Game1.getGraphics().GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleStrip, planeLeft, 0, 2);
            Game1.getGraphics().GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleStrip, planeRight, 0, 2);
            Game1.getGraphics().GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleStrip, planeFront, 0, 2);
            Game1.getGraphics().GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleStrip, planeBack, 0, 2);*/
            
        }

       

    }
}
