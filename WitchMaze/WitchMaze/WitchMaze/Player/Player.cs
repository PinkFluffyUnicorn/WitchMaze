﻿using Microsoft.Xna.Framework;
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
        float timescale = 400;
        Vector3 direction; //für bewegung vorne hinten
        Vector3 ortoDirection; //für bewegung links rechts
        Model model;

        //Shader

        BasicEffect effect = Game1.getEffect();

        private static Matrix projection, camera, world ;




        public static Matrix getProjection() { return projection; }
        public static Matrix getCamera() { return camera; }
        public static Matrix getWorld() { return world; }
        public Vector3 getPosition() { return this.position; }
        //public Vector2[,] getBoundingBox { return null;}
        public Model getModel() { return this.model; }


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

            //camera = Matrix.CreateLookAt(position, lookAt, upDirection);
            camera = Matrix.CreateWorld(position,lookAt , upDirection);
            world = Matrix.Identity;
            model = Game1.getContent().Load<Model>("cube");
            
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
            ortoDirection = Vector3.Cross(direction, upDirection);
            //kack steuerung auf mehr hatte ich grad keine lust
            //forward
            if (keyboard.IsKeyDown(Keys.W) && !keyboard.IsKeyDown(Keys.S))
            {
                //position.Z = position.Z * (timeSinceLastMove / timescale);
                position = position + direction * (timeSinceLastMove / timescale);
                lookAt = position + direction;
            }
            //backward
            if (keyboard.IsKeyDown(Keys.S) && !keyboard.IsKeyDown(Keys.W))
            {
                position = position - direction * (timeSinceLastMove / timescale);
                lookAt = position + direction;
            }


            //right
            if (keyboard.IsKeyDown(Keys.D) && !keyboard.IsKeyDown(Keys.A))
            {
                position = position + ortoDirection * (timeSinceLastMove / timescale);
                lookAt = position + direction;
            }

            //left
            if (keyboard.IsKeyDown(Keys.A) && !keyboard.IsKeyDown(Keys.D))
            {
                position = position - ortoDirection * (timeSinceLastMove / timescale);
                lookAt = position + direction;
            }

            //rotate left
            if (keyboard.IsKeyDown(Keys.Q) && !keyboard.IsKeyDown(Keys.E) || keyboard.IsKeyDown(Keys.Left) && !keyboard.IsKeyDown(Keys.Right))
            {
                lookAt = position + (Vector3.Transform((lookAt - position), Matrix.CreateRotationY(timeSinceLastMove / 4 / timescale)));
            }

            //rotate rigth
            if (keyboard.IsKeyDown(Keys.E) && !keyboard.IsKeyDown(Keys.Q) || keyboard.IsKeyDown(Keys.Right) && !keyboard.IsKeyDown(Keys.Left))
            {
                lookAt = position + (Vector3.Transform((lookAt - position), Matrix.CreateRotationY(-timeSinceLastMove / 4 / timescale)));
            }

            //ToDo:
            //Springen
            //Hoch schaun
            //up direction verschieben...
            //if (keyboard.IsKeyDown(Keys.Up) && !keyboard.IsKeyDown(Keys.Down))
            //{
            //    upDirection = upDirection + Matrix.CreateRotationX((timeSinceLastMove/timescale));
            //}
            //Runter schaun

            //fliegen
            if (!keyboard.IsKeyDown(Keys.LeftControl) && keyboard.IsKeyDown(Keys.LeftShift))
            {
                position += new Vector3(0,timeSinceLastMove / timescale,0);
                lookAt += new Vector3(0, timeSinceLastMove / timescale, 0);
            }
            if (keyboard.IsKeyDown(Keys.LeftControl) && !keyboard.IsKeyDown(Keys.LeftShift))
            {
                position -= new Vector3(0, timeSinceLastMove / timescale, 0);
                lookAt -= new Vector3(0, timeSinceLastMove / timescale, 0);
            }

        }


        public void draw(GameTime gametime)
        {
            camera = Matrix.CreateLookAt(position, lookAt, upDirection);
            effect.VertexColorEnabled = true;
            effect.View = camera;
            effect.Projection = projection;
            effect.World = world;
            effect.CurrentTechnique.Passes[0].Apply();
            //model.Draw(Matrix.CreateScale(0.05f) * Matrix.CreateTranslation(position), camera, projection);
            Game1.getEffect().World = Matrix.Identity;
            Game1.getEffect().CurrentTechnique.Passes[0].Apply();
        }

    }
}
