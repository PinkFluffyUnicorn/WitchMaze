using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WitchMaze.Player
{
    class Player
    {
        Vector3 position;
        Vector3 lookAt;
        Vector3 upDirection;
        KeyboardState keyboard;
        float aspectRatio;
        float timeSinceLastMove;
        float timescale = 200;
        Vector3 direction;

        //Shader



        BasicEffect effect = Game1.getEffect();

        public static Matrix projection, camera, world ; 
        public Player()
        {
            keyboard = Keyboard.GetState();
            aspectRatio = Game1.getGraphics().GraphicsDevice.Viewport.AspectRatio;
            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspectRatio, 0.5f, 1000.0f);
             // params : position, forward,up, matrix out 

            //werte sollten später für jeden Spieler einzeln angepasst werden
            position = new Vector3(0, 1, 0);
            lookAt = new Vector3(0, 1, 1);
            upDirection = new Vector3(0, 1, 0);
            camera = Matrix.CreateLookAt(position, lookAt, upDirection);
            world = Matrix.Identity;
            
        }



        public void update(GameTime gameTime)
        {
            this.move(gameTime);
        }

        private void move(GameTime gameTime)
        {
            timeSinceLastMove = gameTime.ElapsedGameTime.Milliseconds;
            keyboard = Keyboard.GetState();
            direction = lookAt - position;
            //kack steuerung auf mehr hatte ich grad keine lust
            //forward
            if (keyboard.IsKeyDown(Keys.W) && !keyboard.IsKeyDown(Keys.S))
            {
                position = position + direction * (timeSinceLastMove / timescale);
                lookAt = position + direction;
            }

            //backward
            if (keyboard.IsKeyDown(Keys.S) && !keyboard.IsKeyDown(Keys.W))
            {
                position = position - direction * (timeSinceLastMove / timescale);
                lookAt = position + direction;
            }

            //rotate left
            if (keyboard.IsKeyDown(Keys.Q) && !keyboard.IsKeyDown(Keys.E))
            {
                lookAt = position + (Vector3.Transform((lookAt - position), Matrix.CreateRotationY(timeSinceLastMove / 4 / timescale)));
            }

            //rotate rigth
            if (keyboard.IsKeyDown(Keys.E) && !keyboard.IsKeyDown(Keys.Q))
            {
                lookAt = position + (Vector3.Transform((lookAt - position), Matrix.CreateRotationY(-timeSinceLastMove / 4 / timescale)));
            }
        }


        public void draw(GameTime gametime)
        {
            camera = Matrix.CreateLookAt(position, lookAt, upDirection);
            effect.VertexColorEnabled = true;
            effect.View = camera;
            effect.Projection = projection;
            effect.CurrentTechnique.Passes[0].Apply();
        }

    }
}
