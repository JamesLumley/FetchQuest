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

namespace FetchQuest
{
    static class Player
    {
        //CLASS CREATED BY JAMES
        //CLASS EDITED BY CHRIS
        //ART CREATED BY DAN
        #region Declarations

        public static Sprite CharacterSprite;


        private static Vector2 CharacterAngle = Vector2.Zero;
        private static Vector2 ShotgunAngle = Vector2.Zero;
        public static float playerSpeed = 300f;
        public static int maxHealth = 3;
        public static int health = maxHealth;
        private static int healthTimer = 0;
        private static double CharacterAngleDegrees = 0f;
        private static Vector2 fireAngle = new Vector2(0,0);
        public static SoundEffectInstance stepInstance;
        public static SoundEffectInstance shootInstance;
        public static int Delay = 0;
        public static bool GodMode = false;

        private static Rectangle scrollArea = new Rectangle(150, 100, 500, 400);
      
        #endregion

        #region Initialization

        public static void Initialize(Texture2D texture,
            Rectangle characterInitialFrame, int characterFrameCount,
            Rectangle shotgunInitialFrame, int shotgunFrameCount,
            Vector2 worldLocation, SoundEffect soundStep, SoundEffect soundShoot)
        {
            int frameWidth = characterInitialFrame.Width;
            int frameHeight = characterInitialFrame.Height;


            CharacterSprite = new Sprite(worldLocation, texture, characterInitialFrame, Vector2.Zero);
            CharacterSprite.BoundingXPadding = 4;
            CharacterSprite.BoundingYPadding = 4;
            CharacterSprite.AnimateWhenStopped = true;
            stepInstance = soundStep.CreateInstance();
            shootInstance = soundShoot.CreateInstance();
            //Adds the characters animation frames
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    CharacterSprite.AddFrame(new Rectangle(j * 80, i * 80, 80, 80));
                }
            }

            for (int x = 1; x < characterFrameCount; x++)
            {
                CharacterSprite.AddFrame(new Rectangle(
                    characterInitialFrame.X + (frameHeight * x),
                    characterInitialFrame.Y,
                    frameWidth, frameHeight));
            }

            //   ShotgunSprite = new Sprite(worldLocation, texture, shotgunInitialFrame, Vector2.Zero);
            // ShotgunSprite.Animate = false;

            for (int x = 1; x < shotgunFrameCount; x++)
            {
                CharacterSprite.AddFrame(new Rectangle(
                    shotgunInitialFrame.X + (frameHeight * x),
                    shotgunInitialFrame.Y,
                    frameWidth, frameHeight));
            }

            
        }


        #endregion

        #region Input Handling

        private static Vector2 handleKeyboardMovement(KeyboardState keystate)
        {
            Vector2 keyMovement = Vector2.Zero;
            if (keystate.IsKeyDown(Keys.W))
                keyMovement.Y--;
            if (keystate.IsKeyDown(Keys.A))
                keyMovement.X--;
            if (keystate.IsKeyDown(Keys.S))
                keyMovement.Y++;
            if (keystate.IsKeyDown(Keys.D))
                keyMovement.X++;
            return keyMovement;
        }

        private static Vector2 handleGamePadMovement(GamePadState gamepadState)
        {
            return new Vector2(gamepadState.ThumbSticks.Left.X, -gamepadState.ThumbSticks.Left.Y);
        }

        private static Vector2 handleKeyboardShots(KeyboardState keystate)
        {
            Vector2 keyShots = Vector2.Zero;
            if ((keystate.IsKeyDown(Keys.NumPad1))||(keystate.IsKeyDown(Keys.Down)&&keystate.IsKeyDown(Keys.Left)))
                keyShots = new Vector2(-1, 1);
            else if ((keystate.IsKeyDown(Keys.NumPad3)||(keystate.IsKeyDown(Keys.Down)&&keystate.IsKeyDown(Keys.Right))))
                keyShots = new Vector2(1, 1);
            else if ((keystate.IsKeyDown(Keys.NumPad7))||(keystate.IsKeyDown(Keys.Up)&&keystate.IsKeyDown(Keys.Left)))
                keyShots = new Vector2(-1, -1);
            else if ((keystate.IsKeyDown(Keys.NumPad9))||(keystate.IsKeyDown(Keys.Up)&&keystate.IsKeyDown(Keys.Right)))
                keyShots = new Vector2(1, -1);
            else if (keystate.IsKeyDown(Keys.NumPad2)||keystate.IsKeyDown(Keys.Down))
                keyShots = new Vector2(0, 1);
            else if (keystate.IsKeyDown(Keys.NumPad4)||keystate.IsKeyDown(Keys.Left))
                keyShots = new Vector2(-1, 0);
            else if (keystate.IsKeyDown(Keys.NumPad6)||keystate.IsKeyDown(Keys.Right))
                keyShots = new Vector2(1, 0);
            else if (keystate.IsKeyDown(Keys.NumPad8)||keystate.IsKeyDown(Keys.Up))
                keyShots = new Vector2(0, -1);
            return keyShots;
        }

        private static Vector2 handleGamePadShots(GamePadState gamepadState)
        {
            return new Vector2(gamepadState.ThumbSticks.Right.X, -gamepadState.ThumbSticks.Right.Y);
        }

        public static void handleInput(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Vector2 fireAngle = Vector2.Zero;
            Vector2 moveAngle = Vector2.Zero;
            double fireDegrees;

            moveAngle += handleKeyboardMovement(Keyboard.GetState());
            moveAngle += handleGamePadMovement(GamePad.GetState(PlayerIndex.One));
            fireAngle += handleKeyboardShots(Keyboard.GetState());
            fireAngle += handleGamePadShots(GamePad.GetState(PlayerIndex.One));
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                MainMenu.menuSelectInstance.Play();
                GameManager.SetState(3);
            }
            if (GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.Start))
            {

                MainMenu.menuSelectInstance.Play();
                GameManager.SetState(3);
            }
            if (moveAngle != Vector2.Zero)
            {

                moveAngle.Normalize();
                if(fireAngle != Vector2.Zero) CharacterAngle = fireAngle;
                moveAngle = checkTileObstacles(elapsed, moveAngle);

            }

            if (moveAngle == Vector2.Zero)
            {
                CharacterAngle = fireAngle;
            }

            if (fireAngle != Vector2.Zero || GamePad.GetState(PlayerIndex.One).Buttons.X == ButtonState.Pressed)
            {
                if (WeaponManager.CanFireWeapon)
                {

                    fireDegrees = Math.Atan2(CharacterAngle.Y, CharacterAngle.X);
                    CharacterAngle.X = (float)Math.Cos(fireDegrees);
                    CharacterAngle.Y = (float)Math.Sin(fireDegrees);

                    WeaponManager.FireWeapon
                        (new Vector2(CharacterSprite.WorldLocation.X + 35,
                            CharacterSprite.WorldLocation.Y + 35),
                            CharacterAngle * WeaponManager.WeaponSpeed);
                    shootInstance.Volume = 0.75f;
                    shootInstance.Play();
                    

                }
            }

            if (fireAngle == Vector2.Zero)
            {
                CharacterAngle = moveAngle;
            }


            if (CharacterAngle != Vector2.Zero)
            {
               // CharacterSprite.RotateTo(CharacterAngle);
            }



            CharacterSprite.Velocity = moveAngle * playerSpeed;

          

            if (CharacterSprite.Velocity != Vector2.Zero)
            {
                
                if (Delay == 0)
                {
                    stepInstance.Volume = 0.75f;
                    stepInstance.Play();
                    Delay = 10;
                }
                else
                {

                    Delay--;
                }
            }
            else
            {       
                stepInstance.Stop();
            }


            CharacterAngleDegrees = Math.Atan2(CharacterAngle.Y, CharacterAngle.X);
            CharacterAngleDegrees *= 180 / Math.PI;

            repositionCamera(gameTime, moveAngle);


        }

        #endregion

        #region Movement Limitation

        private static void clampToWorld()
        {
            float currentX = CharacterSprite.WorldLocation.X;
            float currentY = CharacterSprite.WorldLocation.Y;

            currentX = MathHelper.Clamp(currentX, 0, Camera.WorldRectangle.Right - CharacterSprite.frameWidth);
            currentY = MathHelper.Clamp(currentY, 0, Camera.WorldRectangle.Bottom - CharacterSprite.FrameHeight);

            CharacterSprite.WorldLocation = new Vector2(currentX, currentY);
        }

        private static void repositionCamera(GameTime gameTime, Vector2 moveAngle)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            float moveScale = playerSpeed * elapsed;
            

            Camera.Move(new Vector2(moveAngle.X, 0) * moveScale);

            Camera.Move(new Vector2(moveAngle.X, 0) * moveScale);


            Camera.Move(new Vector2(0, moveAngle.Y) * moveScale);

            Camera.Move(new Vector2(0, moveAngle.Y) * moveScale);

            if (fireAngle != Vector2.Zero)
            {
                    Random rand = new Random();
                    Camera.Move(new Vector2(rand.Next(-4,4),rand.Next(-4,4)));
            }
        }

        private static Vector2 checkTileObstacles(//Collision detection for wall tiles
            float elapsedTime,
            Vector2 moveAngle)
        {
            Vector2 newHorizontalLocation = CharacterSprite.WorldLocation +
                (new Vector2(moveAngle.X, 0) * (playerSpeed * elapsedTime));

            Vector2 newVerticleLocation = CharacterSprite.WorldLocation +
                (new Vector2(0, moveAngle.Y) * (playerSpeed * elapsedTime));

            Rectangle newHorizontalRect = new Rectangle(
                (int)newHorizontalLocation.X,
                (int)CharacterSprite.WorldLocation.Y,
                CharacterSprite.frameWidth,
                CharacterSprite.FrameHeight);

            Rectangle newVerticleRect = new Rectangle(
                (int)CharacterSprite.WorldLocation.X,
                (int)newVerticleLocation.Y,
                CharacterSprite.frameWidth,
                CharacterSprite.FrameHeight);

            int horizleftPixel = 0;
            int horizRightPixel = 0;

            int vertTopPixel = 0;
            int vertBottomPixel = 0;

            if (moveAngle.X < 0)
            {
                horizleftPixel = (int)newHorizontalRect.Left;
                horizRightPixel = (int)CharacterSprite.WorldRectangle.Left;
            }

            if (moveAngle.X > 0)
            {
                horizleftPixel = (int)CharacterSprite.WorldRectangle.Right;
                horizRightPixel = (int)newHorizontalRect.Right;
            }

            if (moveAngle.Y < 0)
            {
                vertTopPixel = (int)newVerticleRect.Top;
                vertBottomPixel = (int)CharacterSprite.WorldRectangle.Top;
            }

            if (moveAngle.Y > 0)
            {
                vertTopPixel = (int)CharacterSprite.WorldRectangle.Bottom;
                vertBottomPixel = (int)newVerticleRect.Bottom;
            }

            if (moveAngle.X != 0)
            {
                for (int x = horizleftPixel; x < horizRightPixel; x++)
                {
                    for (int y = 0; y < CharacterSprite.FrameHeight; y++)
                    {
                        if (TileMap.IsWallTileByPixel(
                            new Vector2(x, newHorizontalLocation.Y + y)))
                        {
                            moveAngle.X = 0;
                            break;
                        }
                    }
                    if (moveAngle.X == 0)
                    {
                        break;
                    }
                }
            }

            if (moveAngle.Y != 0)
            {
                for (int y = vertTopPixel; y < vertBottomPixel; y++)
                {
                    for (int x = 0; x < CharacterSprite.frameWidth; x++)
                    {
                        if (TileMap.IsWallTileByPixel(
                            new Vector2(newVerticleLocation.X + x, y)))
                        {
                            moveAngle.Y = 0;
                            break;
                        }
                    }
                    if (moveAngle.Y == 0)
                    {
                        break;
                    }
                }
            }




            //Player damage



            //Enemy collision END





            return moveAngle;
        }

        #endregion



        #region Update and Draw

        public static void Update(GameTime gameTime)
        {
            healthTimer += 1;
            handleInput(gameTime);
            CharacterSprite.Update(gameTime);
            if (GodMode == false)
            {
                if (health < 1)
                {
                    GameManager.SetState(7);
                }
            }
            if (GodMode == true)//Adds invulnarability when the user is in tutorial level
            {
                health = 3;
            }

            if (healthTimer > 50) //Adds delay to melee enemies to prevent instant death
            {
                foreach (Enemy enemy in EnemyManager.Enemies)
                {
                    if (CharacterSprite.IsCircleColliding(
                         enemy.EnemyBase.WorldCenter,
                         enemy.EnemyBase.CollisionRadius))
                    {
                        health -= 1;
                        Console.WriteLine(health);
                        healthTimer = 0;
                    }

                }
            }

        }

        public static void Draw(SpriteBatch spriteBatch)
        {

            for (int i = 1; i < 18; i++)
            {
                
                //Draws the character and choses the frame based on the angle to character is facing
                double degreeMeasure = (360 / 16) * (i - 1);
                if (CharacterAngleDegrees < 0) CharacterAngleDegrees += 360;
                if (CharacterAngleDegrees > degreeMeasure - 11.25 && CharacterAngleDegrees < degreeMeasure + 11.25)
                {
                    CharacterSprite.Frame = i;
                }
            }
            CharacterSprite.Draw(spriteBatch);

        }

        #endregion

    }
}

