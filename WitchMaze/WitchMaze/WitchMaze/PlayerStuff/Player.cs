﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WitchMaze.ItemStuff.Items;
using WitchMaze.InterfaceObjects;
using WitchMaze.ownFunctions;
using Microsoft.Xna.Framework.Input;

namespace WitchMaze.PlayerStuff
{
    class Player
    {
        public enum EPlayerViewportPosition
        {
            fullscreen,

            left, //or up/down ist better?
            right,

            topLeft,
            topRight,
            botLeft,
            botRight,
        }

        public enum EPlayerControlls
        {
            none,

            Keyboard1,
            //Keyboard2,
            //Keyboard3,
            KeyboardNumPad,

            Gamepad1,
            Gamepad2,
            Gamepad3,
            Gamepad4,
        }

        //for debugging purpose ->
        public enum EPlayerIndex
        {
            one,
            two,
            three,
            vour,
        }
        public EPlayerIndex eplayerIndex;
        //<- for debugging

        //Player Variablen
        public EPlayerControlls playerControlls { get; protected set; }

        //Zeichnen
        EPlayerViewportPosition playerViewportPosition;
        Viewport viewport;
        float timeSinceLastMove;
        float timescale = 500;//more is slower
        float turningScale = 1;//scale for head turning, more is faster
        Vector3 direction; //für bewegung vorne hinten
        Vector3 ortoDirection; //für bewegung links rechts
        Vector3 position;
        float radius = 0.35f; public float getRadius() { return radius; }
        Vector3 lookAt;
        Vector3 upDirection;
        KeyboardState keyboard;
        float aspectRatio;

        public bool hasWon = false;

        //Move
        //Move movePlayer;
        //Keys up;
        //Keys down;
        //Keys left;
        //Keys right;
        //Keys lookLeft;
        //Keys kookRight;

        //Shader
        BasicEffect effect = Game1.getEffect();
        float cameraOffset = 0;//0.4f;//0.025f;
        float camerOffsetY = 0.2f;
        private Vector3 cameraPosition;//should be a little behind the actual position
        private static Matrix projection, camera, world;

        //to Draw
        Model model;
        Skybox skybox;
        private Matrix scale = Matrix.CreateScale((float)0.002);
        double lookatRotation;//radius
        float movementRotationX;
        float movementRotationZ;

        //bouncing
        bool isBouncing;
        Vector3 bounceDirection;
        float bouncingTimeLeft;
        float bouncingTime;

        protected Vector3 ambient, emissive, specularColor, directionalDiffuse, directionalDirection, directionalSpecular;
        protected float specularPower;

        //other
        List<Item> itemsCollected;
        public float pan{get; protected set;}//gibt an wo der PlayerViewpoet ist
        public Icon playerIcon { get; protected set; }
        

        //get methods
        public Viewport getViewport() { return viewport; }
        public EPlayerViewportPosition getViewportPosition() { return playerViewportPosition; }
        public Skybox getSkybox(){return skybox;}
        public Matrix getProjection() { return projection; }
        public Matrix getCamera() { return camera; }
        public Matrix getWorld() { return world; }
        public Vector3 getPosition() { return this.position; }
        //public Vector2[,] getBoundingBox { return null;}
        public Model getModel() { return this.model; }
        public List<Item> getItemsCollected() { return itemsCollected; }


        //set
        /// <summary>
        /// Creates a Player that already has the possible Controlls Set for him, he misses everything else, set that too!
        /// </summary>
        /// <param name="_playerControlls">sets the way of conrolling the Player</param>
        public Player(EPlayerControlls _playerControlls, EPlayerIndex pi)
        {
            playerControlls = _playerControlls;
            eplayerIndex = pi;

        }
        public void setFinalPlayer(Vector3 spawnPosition, EPlayerViewportPosition viewportPosition){
            playerViewportPosition = viewportPosition;
            this.setViewport();

            itemsCollected = new List<Item>();
            keyboard = Keyboard.GetState();
            aspectRatio = viewport.AspectRatio;
            // params : position, forward,up, matrix out 

            //werte sollten später für jeden Spieler einzeln angepasst werden

            playerIcon = new Icon(new Vector2(0, 0), "witchIcon");

            position = spawnPosition;
            lookAt = new Vector3(Settings.getMapSizeX() / 2, (float)0.22, Settings.getMapSizeZ() / 2);//sollte neu berechnet werden //immer zur mitte der Map?
            upDirection = new Vector3(0, 1, 0);

            //draufsicht
            /* position = new Vector3(5, -40, 5);
             lookAt = new Vector3(0, 1, 1);
             upDirection = new Vector3(0, 0, 1);*/

            GamePadState currentState = GamePad.GetState(PlayerIndex.One); //do we need this and why? :O

            //camera = Matrix.CreateLookAt(new Vector3(position.X - Settings.getResolutionX() / 2, position.Y, position.Z), lookAt, upDirection);
            Vector3 h = (lookAt - position);
            h.Normalize();
            cameraPosition = position - (h) * cameraOffset;
            cameraPosition.Y += camerOffsetY;
            camera = Matrix.CreateWorld(cameraPosition, lookAt, upDirection);
            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspectRatio, 0.1f, 1000.0f);
            world = Matrix.Identity;
            direction = lookAt - position;
            ortoDirection = Vector3.Cross(direction, upDirection);
            effect.LightingEnabled = true;

            movementRotationX = 0;
            movementRotationZ = 0;

            isBouncing = false;
            bouncingTime = 3;
            bouncingTimeLeft = 0;

            skybox = new Skybox(Game1.getContent().Load<Texture2D>("Models/SkyboxTexture"), Game1.getContent().Load<Model>("Models/skybox"));
        }

        public void setPosition(Vector3 p) {position = p;}

        /// <summary>
        /// this method sets the viewport of the player screen dependent on the ViewportPosition, aswell as the model for the player
        /// </summary>
        private void setViewport()
        {
            Viewport defaultViewport = Game1.getGraphics().GraphicsDevice.Viewport;
            switch (playerViewportPosition)
            {
                case EPlayerViewportPosition.fullscreen:
                    viewport = defaultViewport;
                    model = Game1.getContent().Load<Model>("Models/Player/player1");
                    lookatRotation = 5* Math.PI/4;//start rotation of the player, needs to be set here for adjustments
                    emissive = new Vector3(0.2f, 0f, 0f);
                    ambient = new Vector3(0.7f, 0.7f, 0.7f);
                    specularColor = new Vector3(0.8f, 0.7f, 0.7f);
                    specularPower = 0.1f;
                    directionalDiffuse = new Vector3(0.15f, 0.15f, 0.15f);
                    directionalDirection = new Vector3(0f, 1f, 0f);
                    directionalSpecular = new Vector3(0.2f, 0.15f, 0.15f);
                    pan = 0;
                    break;
                case EPlayerViewportPosition.left:
                    viewport = defaultViewport;
                    viewport.Width = viewport.Width / 2;
                    model = Game1.getContent().Load<Model>("Models/Player/player1");
                    lookatRotation = 5 * Math.PI / 4;
                    emissive = new Vector3(0.2f, 0f, 0f);
                    ambient = new Vector3(0.7f, 0.7f, 0.7f);
                    specularColor = new Vector3(0.8f, 0.7f, 0.7f);
                    specularPower = 0.1f;
                    directionalDiffuse = new Vector3(0.15f, 0.15f, 0.15f);
                    directionalDirection = new Vector3(0f, 1f, 0f);
                    directionalSpecular = new Vector3(0.2f, 0.15f, 0.15f);
                    pan = -1;
                    break;
                case EPlayerViewportPosition.right:
                    viewport = defaultViewport;
                    viewport.Width = viewport.Width / 2;
                    viewport.X = defaultViewport.Width / 2;
                    model = Game1.getContent().Load<Model>("Models/Player/player2");
                    lookatRotation = Math.PI / 4;
                    emissive = new Vector3(0.1f, 0f, 0f);
                    ambient = new Vector3(0.8f, 0.8f, 0.8f);
                    specularColor = new Vector3(0.8f, 0.8f, 0.8f);
                    specularPower = 0.1f;
                    directionalDiffuse = new Vector3(0.2f, 0.2f, 0.2f);
                    directionalDirection = new Vector3(0f, 1f, 0f);
                    directionalSpecular = new Vector3(0.2f, 0.2f, 0.2f);
                    pan = 1;
                    break;
                case EPlayerViewportPosition.topLeft:
                    viewport = defaultViewport;
                    viewport.Width = viewport.Width / 2;
                    viewport.Height = viewport.Height / 2;
                    model = Game1.getContent().Load<Model>("Models/Player/player1");
                    lookatRotation = 5 * Math.PI / 4;
                    emissive = new Vector3(0.2f, 0f, 0f);
                    ambient = new Vector3(0.7f, 0.7f, 0.7f);
                    specularColor = new Vector3(0.8f, 0.7f, 0.7f);
                    specularPower = 0.1f;
                    directionalDiffuse = new Vector3(0.15f, 0.15f, 0.15f);
                    directionalDirection = new Vector3(0f, 1f, 0f);
                    directionalSpecular = new Vector3(0.2f, 0.15f, 0.15f);
                    pan = -1;
                    break;
                case EPlayerViewportPosition.botLeft:
                    viewport = defaultViewport;
                    viewport.Width = viewport.Width / 2;
                    viewport.Height = viewport.Height / 2;
                    viewport.Y = defaultViewport.Height / 2;
                    model = Game1.getContent().Load<Model>("Models/Player/player2");
                    lookatRotation = Math.PI / 4;
                    emissive = new Vector3(0.1f, 0f, 0f);
                    ambient = new Vector3(0.8f, 0.8f, 0.8f);
                    specularColor = new Vector3(0.8f, 0.8f, 0.8f);
                    specularPower = 0.1f;
                    directionalDiffuse = new Vector3(0.2f, 0.2f, 0.2f);
                    directionalDirection = new Vector3(0f, 1f, 0f);
                    directionalSpecular = new Vector3(0.2f, 0.2f, 0.2f);
                    pan = -1;
                    break;
                case EPlayerViewportPosition.topRight:
                    viewport = defaultViewport;
                    viewport.Width = viewport.Width / 2;
                    viewport.Height = viewport.Height / 2;
                    viewport.X = defaultViewport.Width / 2;
                    model = Game1.getContent().Load<Model>("Models/Player/player3");
                    lookatRotation = 7 * Math.PI / 4;
                    emissive = new Vector3(0.1f, 0f, 0f);
                    ambient = new Vector3(1f, 0.84f, 0f);
                    specularColor = new Vector3(0.8f, 0.8f, 0.8f);
                    specularPower = 0.1f;
                    directionalDiffuse = new Vector3(0.2f, 0.2f, 0.2f);
                    directionalDirection = new Vector3(0f, 1f, 0f);
                    directionalSpecular = new Vector3(0.2f, 0.2f, 0.2f);
                    pan = 1;
                    break;
                case EPlayerViewportPosition.botRight:
                    viewport = defaultViewport;
                    viewport.Width = viewport.Width / 2;
                    viewport.Height = viewport.Height / 2;
                    viewport.X = defaultViewport.Width / 2;
                    viewport.Y = defaultViewport.Height / 2;
                    model = Game1.getContent().Load<Model>("Models/Player/player4");
                    lookatRotation = 3 * Math.PI / 4;
                    emissive = new Vector3(0.1f, 0f, 0.1f);
                    ambient = new Vector3(1f, 0.51f, 0.99f);
                    specularColor = new Vector3(0.8f, 0.8f, 0.8f);
                    specularPower = 0.1f;
                    directionalDiffuse = new Vector3(0.2f, 0.2f, 0.2f);
                    directionalDirection = new Vector3(0f, 1f, 0f);
                    directionalSpecular = new Vector3(0.2f, 0.2f, 0.2f);
                    pan = 1;
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        //update
        public void update(ownGameTime gameTime)
        {
            //this.reportStatus();
            this.move(gameTime);



            this.itemCollision(position);
        }

        
        public void bounce(Vector3 direction)
        {
            isBouncing = true;
            bounceDirection = direction;
            bounceDirection.Normalize();
        }

        private void move(ownGameTime gameTime)
        {
            //ToDo kollieion dierekt hier einbauen...
            switch (playerControlls)
            {
                case EPlayerControlls.Keyboard1:
                    this.moveK(gameTime, Keys.W, Keys.S, Keys.A, Keys.D, Keys.Q, Keys.E);
                    break;
                //case EPlayerControlls.Keyboard2:
                //    this.moveK(gameTime, Keys.T, Keys.F, Keys.G, Keys.H, Keys.R, Keys.Z);
                //    break;
                //case EPlayerControlls.Keyboard3:
                //    this.moveK(gameTime, Keys.I, Keys.J, Keys.K, Keys.L, Keys.U, Keys.O);
                    break;
                case EPlayerControlls.KeyboardNumPad:
                    this.moveK(gameTime, Keys.NumPad5, Keys.NumPad2, Keys.NumPad1, Keys.NumPad3, Keys.NumPad4, Keys.NumPad6);
                    //this.moveK(gameTime, Keys.I, Keys.J, Keys.K, Keys.L, Keys.U, Keys.O);
                    break;
                case EPlayerControlls.Gamepad1:
                    moveG(gameTime, PlayerIndex.One);
                    break;
                case EPlayerControlls.Gamepad2:
                    moveG(gameTime, PlayerIndex.Two);
                    break;
                case EPlayerControlls.Gamepad3:
                    moveG(gameTime, PlayerIndex.Three);
                    break;
                case EPlayerControlls.Gamepad4:
                    moveG(gameTime, PlayerIndex.Four);
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Keyboard Movement
        /// </summary>
        /// <param name="gameTime">GameTime for correct movement dependent on Time not FPS</param>
        private void moveK(ownGameTime gameTime, Keys moveUp, Keys moveDown, Keys moveLeft, Keys moveRight, Keys lookLeft, Keys lookRight)
        {
            Vector3 newPosition = position;
            timeSinceLastMove = gameTime.getElapsedGameTime();
            keyboard = Keyboard.GetState();
            direction = lookAt - position;
            ortoDirection = Vector3.Cross(direction, upDirection);
            bool up = keyboard.IsKeyDown(moveUp) && !keyboard.IsKeyDown(moveDown);
            bool down = keyboard.IsKeyDown(moveDown) && !keyboard.IsKeyDown(moveUp);
            bool left = keyboard.IsKeyDown(moveLeft) && !keyboard.IsKeyDown(moveRight);
            bool right = keyboard.IsKeyDown(moveRight) && !keyboard.IsKeyDown(moveLeft);
            if (isBouncing)
            {
                newPosition += bounceDirection * (timeSinceLastMove / timescale) * bouncingTimeLeft;

                if (/*mapCollision(newPosition) ||*/ bouncingTimeLeft < 0){
                    isBouncing = false;
                    bouncingTimeLeft = bouncingTime;
                }    
                else
                {
                    movementRotationX -= (position - newPosition).X;
                    movementRotationZ += (position - newPosition).Z;

                    if (!this.mapCollision(new Vector3(newPosition.X, position.Y, position.Z)))
                    {
                        position.X = newPosition.X;
                    }

                    if (!this.mapCollision(new Vector3(position.X, position.Y, newPosition.Z)))
                    {
                        position.Z = newPosition.Z;
                    }
                    bouncingTimeLeft -= (timeSinceLastMove / timescale);

                    //movementRotationX -= (position - newPosition).X;

                    //movementRotationZ += (position - newPosition).Z;
                    //position = newPosition;
                    //bouncingTimeLeft -= (timeSinceLastMove / timescale);
                    //Console.WriteLine(bouncingTimeLeft);

                }   
            }
            else
            {
                //compute the "new position" for the player after keyboard inputs
                if (up)
                {//forward
                    newPosition = newPosition + direction;// * (timeSinceLastMove / timescale);
                }
                if (down)
                {//backward
                    newPosition = newPosition - direction;// * (timeSinceLastMove / timescale);
                }
                if (right)
                {//right
                    newPosition = newPosition + ortoDirection;// * (timeSinceLastMove / timescale);
                }
                if (left)
                {//left
                    newPosition = newPosition - ortoDirection;// *(timeSinceLastMove / timescale);
                }
            }

            //compute the move Vector and ormalize it
            Vector2 moveVector = new Vector2(newPosition.X - position.X, newPosition.Z - position.Z);//darf nur vector 2 sein, sonst führt die y coordinate beim normalisieren zu problemen

            //normalize the move Vector if needet and add the timeScaling bevore collision
            if (moveVector.Length() > 1)
                moveVector.Normalize();
            moveVector *= (timeSinceLastMove / timescale);

            //Hier nen dicken Player drehen
            //Roll, Roll, Roll A Witch, Twist It At The End. Light It Up And Take A Puff, And Pass It To Your Friends
            //The Witch in the Maze goes round and round, round and round, round and round, the Witch in the Maze goes round and round all throung Game
            if (left)
                movementRotationX -= moveVector.Length();
            if (right)
                movementRotationX += moveVector.Length();
            if (up)
                movementRotationZ -= moveVector.Length();
            if (down)
                movementRotationZ += moveVector.Length();


            //move at the x coordinates and roll the Player
            if (!this.mapCollision(new Vector3(position.X + moveVector.X, position.Y, position.Z)))
            {
                //movementRotationX += moveVector.X;
                position.X += moveVector.X;
                lookAt = position + direction;
            }
            //move at the z coordinates
            if (!this.mapCollision(new Vector3(position.X, position.Y, position.Z + moveVector.Y)))
            {
                //movementRotationZ += moveVector.Y;
                position.Z += moveVector.Y;
                lookAt = position + direction;
            }


            //rotate the player
            if (keyboard.IsKeyDown(lookLeft) && !keyboard.IsKeyDown(lookRight))
            {//rotate left / look left
                Vector3 newLookAt = position + (Vector3.Transform((lookAt - position), Matrix.CreateRotationY(turningScale * timeSinceLastMove / timescale)));

                //inner schön vom player mittelpunkt aus rechnen du pfeife...
                Vector3 playerToLookAt = lookAt - position;
                Vector3 playerToNewLookAt = newLookAt - position;

                //lookatRotation += (float)Math.Acos(Math.Abs(Vector3.Dot(playerToNewLookAt, playerToLookAt)) / (playerToNewLookAt.Length() * playerToLookAt.Length()));
                lookatRotation += (float)Math.Asin(Math.Abs(Vector3.Cross(playerToNewLookAt, playerToLookAt).Length()) / (playerToNewLookAt.Length() * playerToLookAt.Length()));
                lookAt = newLookAt;
            }
            if (keyboard.IsKeyDown(lookRight) && !keyboard.IsKeyDown(lookLeft))
            {//rotate rigth / look right
                Vector3 newLookAt = position + (Vector3.Transform((lookAt - position), Matrix.CreateRotationY(turningScale * (-timeSinceLastMove) / timescale)));

                //inner schön vom player mittelpunkt aus rechnen du pfeife...
                Vector3 playerToLookAt = lookAt - position;
                Vector3 playerToNewLookAt = newLookAt - position;

                //lookatRotation -= (float)Math.Acos(Math.Abs(Vector3.Dot(playerToNewLookAt, playerToLookAt)) / (playerToNewLookAt.Length() * playerToLookAt.Length()));
                lookatRotation -= (float)Math.Asin(Math.Abs(Vector3.Cross(playerToNewLookAt, playerToLookAt).Length()) / (playerToNewLookAt.Length() * playerToLookAt.Length()));
                lookAt = newLookAt;
            }
            //reset if bigger than 2OI or smaller then 0
            while (lookatRotation > 2 * Math.PI)
                lookatRotation %= 2 * Math.PI;
            while (lookatRotation < 0)
                lookatRotation += 2 * Math.PI;

            ////fliegen
            //if (!keyboard.IsKeyDown(Keys.LeftControl) && keyboard.IsKeyDown(Keys.LeftShift))
            //{
            //    position += new Vector3(0,timeSinceLastMove / timescale,0);
            //    lookAt += new Vector3(0, timeSinceLastMove / timescale, 0);
            //}
            //if (keyboard.IsKeyDown(Keys.LeftControl) && !keyboard.IsKeyDown(Keys.LeftShift))
            //{
            //    position -= new Vector3(0, timeSinceLastMove / timescale, 0);
            //    lookAt -= new Vector3(0, timeSinceLastMove / timescale, 0);
            //}

        }

        //GamePad
        private void moveG(ownGameTime gameTime, PlayerIndex playerIndex)
        {

                // Get the current gamepad state.
                GamePadState currentState = GamePad.GetState(playerIndex);

                Vector3 newPosition = position;
                timeSinceLastMove = gameTime.getElapsedGameTime();
                keyboard = Keyboard.GetState();
                direction = lookAt - position;
                ortoDirection = Vector3.Cross(direction, upDirection);
                bool up = currentState.ThumbSticks.Left.Y > 0.1f;
                bool down = currentState.ThumbSticks.Left.Y < -0.1f;
                bool left = currentState.ThumbSticks.Left.X < -0.1f;
                bool right = currentState.ThumbSticks.Left.X > 0.0f;
                bool lookLeft = currentState.ThumbSticks.Right.X < -0.1f;
                bool lookRight = currentState.ThumbSticks.Right.X > 0.1f;
                float leftMoveScale = 1;
                float rightMoveScale = 1;
                if (isBouncing)
                {
                    newPosition += bounceDirection * (timeSinceLastMove / timescale) * bouncingTimeLeft;

                    if (/*mapCollision(newPosition) ||*/ bouncingTimeLeft < 0)
                    {
                        isBouncing = false;
                        bouncingTimeLeft = bouncingTime;
                        if (playerControlls == EPlayerControlls.Gamepad1)
                            GamePad.SetVibration(PlayerIndex.One, 0,0);
                        if (playerControlls == EPlayerControlls.Gamepad2)
                            GamePad.SetVibration(PlayerIndex.Two, 0, 0);
                        if (playerControlls == EPlayerControlls.Gamepad3)
                            GamePad.SetVibration(PlayerIndex.Three, 0, 0);
                        if (playerControlls == EPlayerControlls.Gamepad4)
                            GamePad.SetVibration(PlayerIndex.Four, 0, 0);
                    }
                    else
                    {
                        movementRotationX -= (position - newPosition).X;
                        movementRotationZ += (position - newPosition).Z;

                        if (!this.mapCollision(new Vector3(newPosition.X, position.Y, position.Z)))
                        {
                            position.X = newPosition.X;
                        }

                        if (!this.mapCollision(new Vector3(position.X, position.Y, newPosition.Z)))
                        {
                            position.Z = newPosition.Z;
                        }

                        if (playerControlls == EPlayerControlls.Gamepad1)
                            GamePad.SetVibration(PlayerIndex.One, bouncingTimeLeft / bouncingTime, bouncingTimeLeft / bouncingTime);
                        if (playerControlls == EPlayerControlls.Gamepad2)
                            GamePad.SetVibration(PlayerIndex.Two, bouncingTimeLeft / bouncingTime, bouncingTimeLeft / bouncingTime);
                        if (playerControlls == EPlayerControlls.Gamepad3)
                            GamePad.SetVibration(PlayerIndex.Three, bouncingTimeLeft / bouncingTime, bouncingTimeLeft / bouncingTime);
                        if (playerControlls == EPlayerControlls.Gamepad4)
                            GamePad.SetVibration(PlayerIndex.Four, bouncingTimeLeft / bouncingTime, bouncingTimeLeft / bouncingTime);

                        bouncingTimeLeft -= (timeSinceLastMove / timescale);

                    }
                }
                else
                {
                    //compute the "new position" for the player after keyboard inputs
                    if (up)
                    {//forward
                        newPosition = newPosition + direction;
                        leftMoveScale = Math.Abs(currentState.ThumbSticks.Left.Y);
                    }
                    if (down)
                    {//backward
                        newPosition = newPosition - direction;
                        leftMoveScale = Math.Abs(currentState.ThumbSticks.Left.Y);
                    }
                    if (right)
                    {//right
                        newPosition = newPosition + ortoDirection;
                        rightMoveScale = Math.Abs(currentState.ThumbSticks.Left.X);
                    }
                    if (left)
                    {//left
                        newPosition = newPosition - ortoDirection;
                        rightMoveScale = Math.Abs(currentState.ThumbSticks.Left.X);
                    }
                }

                //compute the move Vector and ormalize it
                Vector2 moveVector = new Vector2(newPosition.X - position.X, newPosition.Z - position.Z);//darf nur vector 2 sein, sonst führt die y coordinate beim normalisieren zu problemen

                //normalize the move Vector if needet and add the timeScaling bevore collision
                Console.WriteLine(moveVector);
                if (moveVector.Length() > 1)
                    moveVector.Normalize();
                float h = (leftMoveScale + rightMoveScale);//h scales the movescale back if its too high
                if(h > 1)
                    h = 1;
                moveVector *= (timeSinceLastMove / timescale) * h; 
                    //moveVector.Y  *= Math.Abs(currentState.ThumbSticks.Left.Y);
                    //moveVector.X *= Math.Abs(currentState.ThumbSticks.Left.X);

                //Hier nen dicken Player drehen
                //Roll, Roll, Roll A Witch, Twist It At The End. Light It Up And Take A Puff, And Pass It To Your Friends
                //The Witch in the Maze goes round and round, round and round, round and round, the Witch in the Maze goes round and round all throung Game
                if (left)
                    movementRotationX -= moveVector.Length();
                if (right)
                    movementRotationX += moveVector.Length();
                if (up)
                    movementRotationZ -= moveVector.Length();
                if (down)
                    movementRotationZ += moveVector.Length();


                //move at the x coordinates and roll the Player
                if (!this.mapCollision(new Vector3(position.X + moveVector.X, position.Y, position.Z)))
                {
                    //movementRotationX += moveVector.X;
                    position.X += moveVector.X;
                    lookAt = position + direction;
                }
                //move at the z coordinates
                if (!this.mapCollision(new Vector3(position.X, position.Y, position.Z + moveVector.Y)))
                {
                    //movementRotationZ += moveVector.Y;
                    position.Z += moveVector.Y;
                    lookAt = position + direction;
                }


                //rotate the player
                if (lookLeft)
                {//rotate left / look left
                    Vector3 newLookAt = position + (Vector3.Transform((lookAt - position), Matrix.CreateRotationY(turningScale * timeSinceLastMove / timescale)));
                    Vector3 moveLookAt = newLookAt - lookAt;//to scale toth the controller
                    moveLookAt *= Math.Abs(currentState.ThumbSticks.Right.X);
                    newLookAt = lookAt + moveLookAt;
                    //inner schön vom player mittelpunkt aus rechnen du pfeife...
                    Vector3 playerToLookAt = lookAt - position;
                    Vector3 playerToNewLookAt = newLookAt - position;


                    //lookatRotation += (float)Math.Acos(Math.Abs(Vector3.Dot(playerToNewLookAt, playerToLookAt)) / (playerToNewLookAt.Length() * playerToLookAt.Length()));
                    lookatRotation += (float)Math.Asin(Math.Abs(Vector3.Cross(playerToNewLookAt, playerToLookAt).Length()) / (playerToNewLookAt.Length() * playerToLookAt.Length()));
                    lookAt = newLookAt;
                }
                if (lookRight)
                {//rotate rigth / look right
                    Vector3 newLookAt = position + (Vector3.Transform((lookAt - position), Matrix.CreateRotationY(turningScale * (-timeSinceLastMove) / timescale)));
                    Vector3 moveLookAt = newLookAt - lookAt;//to scale toth the controller
                    moveLookAt *= Math.Abs(currentState.ThumbSticks.Right.X);
                    newLookAt = lookAt + moveLookAt;
                    //inner schön vom player mittelpunkt aus rechnen du pfeife...
                    Vector3 playerToLookAt = lookAt - position;
                    Vector3 playerToNewLookAt = newLookAt - position;


                    //lookatRotation -= (float)Math.Acos(Math.Abs(Vector3.Dot(playerToNewLookAt, playerToLookAt)) / (playerToNewLookAt.Length() * playerToLookAt.Length()));
                    lookatRotation -= (float)Math.Asin(Math.Abs(Vector3.Cross(playerToNewLookAt, playerToLookAt).Length()) / (playerToNewLookAt.Length() * playerToLookAt.Length()));
                    lookAt = newLookAt;
                }
                //reset if bigger than 2OI or smaller then 0
                while (lookatRotation > 2 * Math.PI)
                    lookatRotation %= 2 * Math.PI;
                while (lookatRotation < 0)
                    lookatRotation += 2 * Math.PI;

        }

        public bool mapCollision(Vector3 p)
        {
            List<MapStuff.Blocks.Block> blocksStandingOn = mapBlocksStandingOn(p);
            //compute if collision
            foreach(MapStuff.Blocks.Block block in blocksStandingOn){
                if (!block.walkable)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Computes a List of every MapTile the player is standing on
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public List<MapStuff.Blocks.Block> mapBlocksStandingOn(Vector3 p)
        {//Problem: mittelpunkt in der mitte... daher zurückrechnen...
            Vector2 playerMapPosition = new Vector2((int)Math.Round(p.X), (int)Math.Round(p.Z));           
            List<MapStuff.Blocks.Block> collisionList = new List<MapStuff.Blocks.Block>();
            Vector2 toCheck; //the next tile to ckeck
            //Vector2 topLeft; //the top left point of the tie to check

            //top left tile
            toCheck = new Vector2(playerMapPosition.X - 1, playerMapPosition.Y + 1);
            if (checkBlockAt(toCheck, p))
                    collisionList.Add(WitchMaze.GameStates.InGameState.getMap().getBlockAt((int)toCheck.X, (int)toCheck.Y));


            //top middle tile
            toCheck = new Vector2(playerMapPosition.X, playerMapPosition.Y + 1);
            if(checkBlockAt(toCheck, p))
                collisionList.Add(WitchMaze.GameStates.InGameState.getMap().getBlockAt((int)toCheck.X, (int)toCheck.Y));

            //top right tile
            toCheck = new Vector2(playerMapPosition.X + 1, playerMapPosition.Y + 1);
            if (checkBlockAt(toCheck, p))
                collisionList.Add(WitchMaze.GameStates.InGameState.getMap().getBlockAt((int)toCheck.X, (int)toCheck.Y));

            //middel left tile
            toCheck = new Vector2(playerMapPosition.X - 1, playerMapPosition.Y);
            if (checkBlockAt(toCheck, p))
                collisionList.Add(WitchMaze.GameStates.InGameState.getMap().getBlockAt((int)toCheck.X, (int)toCheck.Y));

            //middle middle tile //Tile i am standing on
            toCheck = playerMapPosition;
            collisionList.Add(WitchMaze.GameStates.InGameState.getMap().getBlockAt((int)toCheck.X, (int)toCheck.Y));


            //middle right tile
            toCheck = new Vector2(playerMapPosition.X + 1, playerMapPosition.Y);
            if (checkBlockAt(toCheck, p))
                collisionList.Add(WitchMaze.GameStates.InGameState.getMap().getBlockAt((int)toCheck.X, (int)toCheck.Y));

            //bot left tile
            toCheck = new Vector2(playerMapPosition.X - 1, playerMapPosition.Y - 1);
            if (checkBlockAt(toCheck, p))
                collisionList.Add(WitchMaze.GameStates.InGameState.getMap().getBlockAt((int)toCheck.X, (int)toCheck.Y));

            //bot middle tile
            toCheck = new Vector2(playerMapPosition.X, playerMapPosition.Y - 1);
            if (checkBlockAt(toCheck, p))
                collisionList.Add(WitchMaze.GameStates.InGameState.getMap().getBlockAt((int)toCheck.X, (int)toCheck.Y));

            //bot right tile
            toCheck = new Vector2(playerMapPosition.X + 1, playerMapPosition.Y - 1);
            if (checkBlockAt(toCheck, p))
                collisionList.Add(WitchMaze.GameStates.InGameState.getMap().getBlockAt((int)toCheck.X, (int)toCheck.Y));

            return collisionList;
        }

        /// <summary>
        /// returns if the block is colliding
        /// </summary>
        /// <param name="toCheck"></param>
        /// <returns></returns>
        private bool checkBlockAt(Vector2 toCheck, Vector3 p)
        {
            Vector2 topLeft = new Vector2(GameStates.InGameState.getMap().getBlockAt((int)toCheck.X, (int)toCheck.Y).position.X - Settings.getBlockSizeX() / 2,
                        GameStates.InGameState.getMap().getBlockAt((int)toCheck.X, (int)toCheck.Y).position.Z + Settings.getBlockSizeZ() / 2);

            if (ownFunctions.Collision.circleRectangleCollision(new Vector2(p.X, p.Z), radius, topLeft, Settings.getBlockSizeX(), Settings.getBlockSizeZ()))
                if (!WitchMaze.GameStates.InGameState.getMap().getTileWalkableAt(toCheck))
                    return true;
            return false;
        }

        /// <summary>
        /// handles player item collision
        /// </summary>
        private void itemCollision(Vector3 p)
        {
            List<MapStuff.Blocks.Block> blocksStandingOn = mapBlocksStandingOn(p);
            foreach (MapStuff.Blocks.Block block in blocksStandingOn)
            {
                Vector2 playerMapPosition = new Vector2((int)Math.Round(p.X), (int)Math.Round(p.Z));
                if (!GameStates.InGameState.getItemMap().isEmpty((int)playerMapPosition.X, (int)playerMapPosition.Y))
                {
                    Game1.sounds.itemCollected.Play(Settings.getSoundVolume(), 0, pan);
                    itemsCollected.Add(GameStates.InGameState.getItemMap().getItem((int)playerMapPosition.X, (int)playerMapPosition.Y));
                    GameStates.InGameState.getItemMap().deleteItem((int)playerMapPosition.X, (int)playerMapPosition.Y);
                }
            }

        }


        public void updateCamera()
        {
            //update camera position
            Vector3 h = (lookAt - position);
            h.Normalize();
            cameraPosition = position - (h) * cameraOffset;
            cameraPosition.Y += camerOffsetY;

            //update camera matrix
            camera = Matrix.CreateLookAt(cameraPosition, lookAt, upDirection);
        }


        /// <summary>
        /// draws the player
        /// </summary>
        public void draw()
        {
            

            //model.Draw(Matrix.CreateScale(0.05f) * Matrix.CreateTranslation(position), camera, projection); //player model (temporary)
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect _effect in mesh.Effects)
                {
                    _effect.LightingEnabled = true;

                    _effect.AmbientLightColor = ambient;
                    _effect.EmissiveColor = emissive;
                    _effect.SpecularColor = specularColor;
                    _effect.SpecularPower = specularPower;

                    _effect.DirectionalLight0.DiffuseColor = directionalDiffuse; 
                    _effect.DirectionalLight0.Direction = directionalDirection;  
                    _effect.DirectionalLight0.SpecularColor = directionalSpecular;

                    //Matrix.CreateFromAxisAngle(ortoDirection, blabla); //for player roll try this
                    //Matrix.CreateRotationZ(movementRotationX) *

                    Matrix rotation = /*Matrix.CreateFromAxisAngle(ortoDirection, movementRotationX)*/Matrix.CreateRotationX(movementRotationX) * Matrix.CreateRotationZ(movementRotationZ) * Matrix.CreateRotationY((float)lookatRotation);
                    _effect.World = mesh.ParentBone.Transform * rotation *  scale * Matrix.CreateTranslation(position);
                    _effect.View = camera;
                    _effect.Projection = projection;
                }
                
                mesh.Draw();
            }
        }
    }
}
