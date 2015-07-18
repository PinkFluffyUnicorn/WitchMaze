using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace WitchMaze.Player
{
    class Player
    {
        Vector3 newPosition;
        Vector3 position;
        Vector3 lookAt;
        Vector3 upDirection;
        KeyboardState keyboard;
        float aspectRatio;
        float timeSinceLastMove;
        float timescale = 900;
        Vector3 direction; //für bewegung vorne hinten
        Vector3 ortoDirection; //für bewegung links rechts
        Model model;
        Vector2 playerMapPosition;
        List<Items.Item> itemsCollected; 
        

        //Shader
        BasicEffect effect = Game1.getEffect();

        private static Matrix projection, camera, world ;

        /// <summary>
        /// Writes the Player Status in the Console
        /// </summary>
        private void reportStatus() {
            Console.Clear();
            Console.WriteLine("X: " + (int)position.X + " Z: " + (int)position.Z);
            //Console.WriteLine(position);
            Console.WriteLine(itemsCollected.Count);
        }

        public static Matrix getProjection() { return projection; }
        public static Matrix getCamera() { return camera; }
        public static Matrix getWorld() { return world; }
        public Vector3 getPosition() { return this.position; }
        //public Vector2[,] getBoundingBox { return null;}
        public Model getModel() { return this.model; }


        public Player()
        {
            itemsCollected = new List<Items.Item>();
            keyboard = Keyboard.GetState();
            aspectRatio = Game1.getGraphics().GraphicsDevice.Viewport.AspectRatio;
            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspectRatio, 0.5f, 1000.0f);
             // params : position, forward,up, matrix out 

            //werte sollten später für jeden Spieler einzeln angepasst werden
            position = new Vector3(10, 1, 10);
            GamePadState currentState = GamePad.GetState(PlayerIndex.One);
            lookAt = new Vector3(0, 1, 1);
            upDirection = new Vector3(0, 1, 0);

            //camera = Matrix.CreateLookAt(position, lookAt, upDirection);
            camera = Matrix.CreateWorld(position,lookAt , upDirection);
            world = Matrix.Identity;
            model = Game1.getContent().Load<Model>("cube");
            direction = lookAt - position;
            ortoDirection = Vector3.Cross(direction, upDirection);
        }



        public void update(GameTime gameTime)
        {
            this.reportStatus();

            this.moveG(gameTime); //GamePad
            this.moveK(gameTime); //Keyboard

            this.itemCollision();
           
        }

        /// <summary>
        /// Keyboard Movement
        /// </summary>
        /// <param name="gameTime">GameTime for correct movement dependent on Time not FPS</param>
        private void moveK(GameTime gameTime)
        {
            timeSinceLastMove = gameTime.ElapsedGameTime.Milliseconds;
            keyboard = Keyboard.GetState();
            direction = lookAt - position;
            ortoDirection = Vector3.Cross(direction, upDirection);
            ///kack steuerung auf mehr hatte ich grad keine lust
            if (keyboard.IsKeyDown(Keys.W) && !keyboard.IsKeyDown(Keys.S))
            {//forward
                //position.Z = position.Z * (timeSinceLastMove / timescale);
                newPosition = position + direction * (timeSinceLastMove / timescale);
                if (!this.mapCollision(newPosition))
                {
                    position = newPosition;
                    lookAt = position + direction;
                }
            }
            if (keyboard.IsKeyDown(Keys.S) && !keyboard.IsKeyDown(Keys.W))
            {//backward
                newPosition = position - direction * (timeSinceLastMove / timescale);
                if (!this.mapCollision(newPosition))
                {
                    position = newPosition;
                    lookAt = position + direction;
                }
            }
            if (keyboard.IsKeyDown(Keys.D) && !keyboard.IsKeyDown(Keys.A))
            {//right
                newPosition = position + ortoDirection * (timeSinceLastMove / timescale);
                if (!this.mapCollision(newPosition))
                {
                    position = newPosition;
                    lookAt = position + direction;
                }
            }
            if (keyboard.IsKeyDown(Keys.A) && !keyboard.IsKeyDown(Keys.D))
            {//left
                newPosition = position - ortoDirection * (timeSinceLastMove / timescale);
                if (!this.mapCollision(newPosition))
                {
                    position = newPosition;
                    lookAt = position + direction;
                }
            }
            if (keyboard.IsKeyDown(Keys.Q) && !keyboard.IsKeyDown(Keys.E) || keyboard.IsKeyDown(Keys.Left) && !keyboard.IsKeyDown(Keys.Right))
            {//rotate left / look left
                lookAt = position + (Vector3.Transform((lookAt - position), Matrix.CreateRotationY(timeSinceLastMove /  timescale)));
            }
            if (keyboard.IsKeyDown(Keys.E) && !keyboard.IsKeyDown(Keys.Q) || keyboard.IsKeyDown(Keys.Right) && !keyboard.IsKeyDown(Keys.Left))
            {//rotate rigth / look right
                lookAt = position + (Vector3.Transform((lookAt - position), Matrix.CreateRotationY(-timeSinceLastMove /  timescale)));
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

        //GamePad
        private void moveG(GameTime gameTime)
        {
            // Get the current gamepad state.
            GamePadState currentState = GamePad.GetState(PlayerIndex.One);
            
            if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y > 0.0f)
            {// Player one has pressed the left thumbstick up.

                position = position + direction * (currentState.ThumbSticks.Left.Y/20);
                lookAt = position + direction;
            }

            if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y < 0.0f)
            {// Player one has pressed the left thumbstick down.

                position = position + direction * (currentState.ThumbSticks.Left.Y/20);
                lookAt = position + direction;
            }

            if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X > 0.0f)
            {// Player one has pressed the left thumbstick right.

                position = position + ortoDirection * (currentState.ThumbSticks.Left.X/20);
                lookAt = position + direction;
            }

            if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X < 0.0f)
            {// Player one has pressed the left thumbstick left.

                position = position + ortoDirection * (currentState.ThumbSticks.Left.X/20);
                lookAt = position + direction;
            }
            
            if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.X > 0.0f)
            {// Player one has pressed the right thumbstick right.

                lookAt = position + (Vector3.Transform((direction), Matrix.CreateRotationY(-currentState.ThumbSticks.Right.X/50)));
            }

            if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.X < 0.0f)
            {// Player one has pressed the right thumbstick left.

                lookAt = position + (Vector3.Transform((direction), Matrix.CreateRotationY(-currentState.ThumbSticks.Right.X/50)));
            }

        }

        /// <summary>
        /// handles the collision for the player by checking if the maptiles near him are walkable
        /// </summary>
        /// <returns>Returns if Player will Collide if moved to p</returns>
        public bool mapCollision(Vector3 p)
        {
            //maping Player position to MapTile position
            playerMapPosition = new Vector2(p.X , p.Z ); //prototyp, später muss genau ermittelt werden auf welchen tiles der Player genau steht
            Console.WriteLine(playerMapPosition);
            //PlayerMapCollision
            if (WitchMaze.GameStates.InGameState.getMap().getTileWalkableAt(playerMapPosition))
                return false;
            else
                return true;
        }
        /// <summary>
        /// handles player item collision
        /// </summary>
        private void itemCollision()
        {
            if (!GameStates.InGameState.getItemMap().isEmpty((int)position.X, (int)position.Z))
            {
                itemsCollected.Add(GameStates.InGameState.getItemMap().getItem((int)position.X, (int)position.Z));
                GameStates.InGameState.getItemMap().deleteItem((int)position.X, (int)position.Z);
            }
        }



        /// <summary>
        /// handles the player draw
        /// </summary>
        /// <param name="gametime"></param>
        public void draw(GameTime gametime)
        {
            camera = Matrix.CreateLookAt(position, lookAt, upDirection);
            effect.VertexColorEnabled = true;
            effect.View = camera;
            effect.Projection = projection;
            effect.World = world;
            effect.CurrentTechnique.Passes[0].Apply();
            //model.Draw(Matrix.CreateScale(0.05f) * Matrix.CreateTranslation(position), camera, projection); //player model (temporary)
            Game1.getEffect().World = Matrix.Identity;
            Game1.getEffect().CurrentTechnique.Passes[0].Apply();
        }

    }
}
