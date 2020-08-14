using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace FetchQuest
{
    //CLASS CREATED BY JAMES
    //CLASS EDITED BY DAN
    //ART CREATED BY DAN
    class WeaponManager
    {

        #region Declarations

        static public List<Particle> Shots = new List<Particle>();
        static public List<Particle> EnemyShots = new List<Particle>();
        static public Texture2D Texture;
        static public Rectangle shotRectangle = new Rectangle(0, 0, 32, 16);
        static public float WeaponSpeed = 800f;
        static private float shotTimer = 0f;
        static public int collisionRadius = 2;
        static private double velocityDegrees;
        static public float shotMinTimer = 0.15f;
        static public SoundEffectInstance soundExplosionInstance;
        static public SoundEffectInstance soundPotBreakInstance;
        static public SoundEffectInstance soundWallHitInstance;
        //UPGRADES
        //Save these 3 variables to keep data on upgrades when reloaded
        public static float weaponFireRateUpgrade = 0.0f; //When game is loaded, the fire delay must be minused by this number
        public static int bulletSizeUpgrade = 0;
        public static int bulletSpreadUpgrade = 0;
        //End UPGRADES

        public static int bulletSpreadAngle = 2;
        static private Random rand = new Random();
    
      
        static bool redShot = true;

        #endregion

        #region Properties

        static public float WeaponFireDelay
        {
            get
            {
                return shotMinTimer;
            }
        }

        static public bool CanFireWeapon
        {
            get
            {
                return (shotTimer >= WeaponFireDelay);
            }
        }

        #endregion

        #region Effects Management Methods

        private static void AddShot(Vector2 location, Vector2 velocity, int frame)//Creates a new bullet
        {
            Particle shot = new Particle(
                 location,
                 Texture,
                 shotRectangle,
                 velocity,
                 Vector2.Zero,
                 WeaponManager.WeaponSpeed,
                 120,
                 Color.White,
                 Color.White);

            shot.AddFrame(new Rectangle(
                shotRectangle.X + shotRectangle.Width, shotRectangle.Y,
                shotRectangle.Width, shotRectangle.Height));

            shot.CollisionRadius = collisionRadius + (bulletSizeUpgrade / 2);

            shot.Animate = false;
            shot.RotateTo(velocity);
            Shots.Add(shot);
        }

        //EDITED BY DAN{
        public static void AddEnemyShot(Vector2 location, Vector2 velocity, int frame)//Creates a new bullet from an enemy
        {
            Particle enemyShot = new Particle(
                 location,
                 Texture,
                 shotRectangle,
                 velocity,
                 Vector2.Zero,
                 WeaponManager.WeaponSpeed,
                 120,
                 Color.White,
                 Color.White);

            enemyShot.AddFrame(new Rectangle(0, 16, 32, 16));
            enemyShot.AddFrame(new Rectangle(0, 32, 32, 16));

            enemyShot.CollisionRadius = 30;

            enemyShot.Animate = false;
            enemyShot.RotateTo(velocity);
            EnemyShots.Add(enemyShot);
        }//}EDITED BY DAN

        private static void createLargeExplosions(Vector2 location)
        {
            EffectsManager.AddLargeExplosion(location + new Vector2(-10, -10));
            EffectsManager.AddLargeExplosion(location + new Vector2(-10, 10));
            EffectsManager.AddLargeExplosion(location + new Vector2(10, 10));
            EffectsManager.AddLargeExplosion(location + new Vector2(10, -10));
            EffectsManager.AddLargeExplosion(location);
        }



        #endregion

        #region Weapons Management Methods

        public static void FireWeapon(Vector2 location, Vector2 velocity)
        {
            
                AddShot(location, velocity, 0);
                shotTimer = 0.0f;
                Random viewShake = new Random();
                Camera.Move(new Vector2(viewShake.Next(-20, 20),viewShake.Next(-20, 20)));
            
            if (bulletSpreadUpgrade > 0)//Adds extra bullets if the player has a bullet spread upgrade
            {
                for (int x = 1; x <= bulletSpreadUpgrade; x++ )
                {
                    float baseAngle = (float)Math.Atan2(velocity.Y, velocity.X);
                    float offset = MathHelper.ToRadians(bulletSpreadAngle);
                    
                    AddShot(location,
                    new Vector2((float)Math.Cos(baseAngle - offset * x),                      
                    (float)Math.Sin(baseAngle - (offset * x))) * velocity.Length(), 0);
                         
                    AddShot(location,
                    new Vector2((float)Math.Cos(baseAngle + offset * x),                        
                    (float)Math.Sin(baseAngle + (offset * x))) * velocity.Length(), 0);

                }
            }
        }

        #endregion

        #region Collision Detection

        private static void checkShotPlayerImpacts(Sprite enemyShot)//checks if the shot hits a player
        {
            if (enemyShot.Expired)
            {
                return;
            }
            if (enemyShot.IsCircleColliding(Player.CharacterSprite.WorldCenter, Player.CharacterSprite.CollisionRadius))
            {
                enemyShot.Expired = true;
                Player.health -= 1;
                soundExplosionInstance.Volume = 0.75f;
                soundExplosionInstance.Play();
            }
        }

        private static void checkShotEnemyImpacts(Sprite shot)//Checks if the shots hit enemies/bullets/barrels/bosses
        {
            if (shot.Expired)
            {
                return;
            }
            foreach (Enemy enemy in EnemyManager.Enemies)
            {
                if (!enemy.Destroyed)
                {
                    if (shot.IsCircleColliding(
                        enemy.EnemyBase.WorldCenter,
                        enemy.EnemyBase.CollisionRadius))
                    {
                        shot.Expired = true;
                        enemy.health -= 1;

                        if (shot.Frame == 0)
                        {
                          

                            soundExplosionInstance.Volume = 0.75f;
                            soundExplosionInstance.Play();
                            
                        }
                        else
                        {
                            if (shot.Frame == 1)
                            {
                                soundExplosionInstance.Volume = 0.75f;
                                soundExplosionInstance.Play();

                               
                                checkRocketSplashDamage(shot.WorldCenter);
                            }
                        }
                    }
                }
            }
            foreach (Barrel barrel in BarrelManager.Barrels)
            {
                if (!barrel.Destroyed)
                {
                    if (shot.IsCircleColliding(
                       barrel.BarrelBase.WorldCenter,
                       barrel.BarrelBase.CollisionRadius))
                    {
                        shot.Expired = true;
                        barrel.Destroyed = true;
                        
                        if (shot.Frame == 0)
                        {
                            EffectsManager.AddDust(barrel.BarrelBase.WorldCenter);
                            soundPotBreakInstance.Volume = 0.75f;
                            soundPotBreakInstance.Play();
                        }
                        else
                        {
                            if (shot.Frame == 1)
                            {
                                EffectsManager.AddDust(barrel.BarrelBase.WorldCenter);
                               
                                checkRocketSplashDamage(shot.WorldCenter);
                                soundPotBreakInstance.Volume = 0.75f;
                                soundPotBreakInstance.Play();
                            }
                        }
                    }
                }
            }

            foreach (Boss boss in BossManager.Bosses)
            {
                if (!boss.Destroyed)
                {
                    if (shot.IsCircleColliding(
                       boss.BossBase.WorldCenter,
                       boss.BossBase.LargeCollisionRadius))
                    {
                        shot.Expired = true;
                        boss.health -= 1;

                        int upgrade = rand.Next(1, 3);




                        if (shot.Frame == 0)
                        {
                            EffectsManager.AddExplosion(
                                boss.BossBase.WorldCenter,
                                boss.BossBase.Velocity / 30);
                        }
                        else
                        {
                            if (shot.Frame == 1)
                            {
                                
                                checkRocketSplashDamage(shot.WorldCenter);
                            }
                        }
                    }
                }
            }
        }
        private static void checkRocketSplashDamage(Vector2 location)
        {
            int rocketSplashRadius = 40;
            foreach (Enemy enemy in EnemyManager.Enemies)
            {
                if (!enemy.Destroyed)
                {
                    if (enemy.EnemyBase.IsCircleColliding(
                        location, rocketSplashRadius))
                    {
                        //enemy.Destroyed = true;

                        EffectsManager.AddExplosion(
                            enemy.EnemyBase.WorldCenter,
                            Vector2.Zero);
                    }
                }
            }
        }
        //*********************************************************************
        private static void checkShotWallImpacts(Sprite shot) //checks if the shots hit a wall and gets rid of them
        {
            if (shot.Expired)
            {
                return;
            }
            if (TileMap.IsWallTile(TileMap.GetSquareAtPixel(shot.WorldCenter)))
            {
                shot.Expired = true;
                if (shot.Frame == 0)
                {
                    EffectsManager.AddSparksEffect(shot.WorldCenter, shot.Velocity);
                    soundWallHitInstance.Volume = 0.75f;
                    soundWallHitInstance.Play();
                }
                else
                {
                   
                    checkRocketSplashDamage(shot.WorldCenter);
                    soundWallHitInstance.Volume = 0.75f;
                    soundWallHitInstance.Play();
                }
            }
        }



        #endregion

        #region Update and Draw

        static public void Update(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;;
            shotTimer += elapsed;
            for (int x = Shots.Count - 1; x >= 0; x--)
            {
                Shots[x].Update(gameTime);
                checkShotWallImpacts(Shots[x]);
                checkShotEnemyImpacts(Shots[x]);
                if (Shots[x].Expired)
                {
                    Shots.RemoveAt(x);
                }
            }
            for (int x = EnemyShots.Count - 1; x >= 0; x--)
            {
                EnemyShots[x].Update(gameTime);
                Random shotRandom = new Random();
                EnemyShots[x].Frame = shotRandom.Next(2) + 1;
                checkShotWallImpacts(EnemyShots[x]);
                checkShotPlayerImpacts(EnemyShots[x]);
                if (EnemyShots[x].Expired)
                {
                    EnemyShots.RemoveAt(x);
                }
            }
            
        }

        static public void Draw(SpriteBatch spriteBatch)
        {

            foreach (Particle sprite in Shots)
            {
                sprite.Draw(spriteBatch);
            }
            foreach (Particle sprite in EnemyShots)
            {
                sprite.Draw(spriteBatch);
            }
        }


        #endregion

    }
}
