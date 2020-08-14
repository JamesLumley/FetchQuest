using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FetchQuest
{
    //CLASS CREATED BY: JAMES
    //EDITED BY: CHRIS
    //ART BY: DAN
    class Boss
    {
        #region Declarations
        public Sprite BossBase;
        public Texture2D hpBar;
        public float BossSpeed = 40f;
        public Vector2 currentTargetSquare;
        public bool Destroyed = false;
        public int maxHealth = 30;
        public int health = 0;
        public static int enemyincrease = 0;
        public static int healthincrease = 0;
        private int collisionRadius = 80;
        private static Random rand = new Random();
        private int bossShotCounter = 0;
        private int enemyTint = 0;
        private int bulletSpeed = 200;
        private int enemySpawnCounter = 0;
        private int enemySpawnDelay = 100;
        private float barScale = 0;
        private double animCounter = 0;
        private bool reverseAnim = false;
        public static int maxenemies = 5;
        #endregion

        #region Constructor

        public Boss(
            Vector2 worldLocation,
            Texture2D texture,
            Texture2D healthbar,
            Rectangle initialFrame)
        {
            BossBase = new Sprite(
                worldLocation,
                texture,
                initialFrame,
                Vector2.Zero);

            BossBase.AddFrame(new Rectangle(192, 0, 192, 192));
            BossBase.AddFrame(new Rectangle(0, 192, 192, 192));
            BossBase.AddFrame(new Rectangle(192, 192, 192, 192));
            BossBase.LargeCollisionRadius = collisionRadius;
            Rectangle BossFrame = initialFrame;
            BossFrame.Offset(0, initialFrame.Height);
            maxHealth += (healthincrease + (GameManager.CurrentLevel * 10));
            health = maxHealth;
            hpBar = healthbar;
        }
        #endregion

        public void Update(GameTime gameTime)
        {
            if (!Destroyed)
            {
                Vector2 directionToPlayer = Player.CharacterSprite.WorldCenter - BossBase.WorldCenter;
                directionToPlayer.Normalize();

                bossShotCounter += 1;
                if (bossShotCounter == 60)//Shoots projectiles towards the player with a 360 degree cone
                {
                   
                    float offset = MathHelper.ToRadians(30);
                    float baseAngle = (float)Math.Atan2(directionToPlayer.Y, directionToPlayer.X);


                    WeaponManager.AddEnemyShot(BossBase.WorldCenter, directionToPlayer * bulletSpeed, 0);
                    bossShotCounter = 0;

                    for (int x = 1; x < 7; x++){

                        WeaponManager.AddEnemyShot(BossBase.WorldCenter,
                        new Vector2((float)Math.Cos(baseAngle - offset * x),
                        (float)Math.Sin(baseAngle - (offset * x))) * (directionToPlayer * bulletSpeed).Length(), 0);

                        WeaponManager.AddEnemyShot(BossBase.WorldCenter,
                        new Vector2((float)Math.Cos(baseAngle + offset * x),
                       (float)Math.Sin(baseAngle + (offset * x))) * (directionToPlayer * bulletSpeed).Length(), 0);
                     
                    }
                }

                //EDITED BY CHRIS {
                if (EnemyManager.Enemies.Count < maxenemies && enemySpawnCounter > enemySpawnDelay)//Spawns enemies at boss's location
                {
                    EnemyManager.AddEnemy(TileMap.GetSquareAtPixel(BossBase.WorldCenter));
                    enemySpawnCounter = 0;
                }
                enemySpawnCounter += 1;
                //} EDITED BY CHRIS

                //EnemyBase.RotateTo(direction);
                BossBase.Update(gameTime);

                

                if (health <= 0)
                {
                    Destroyed = true;
                    if (Player.GodMode == false)
                    {
                        //increases upgrades when a boss is killed (only if not in tutorial mode)
                        switch (rand.Next(5) + 1)
                        {//EDITED BY CHRIS {
                            case 1: WeaponManager.shotMinTimer -= 0.01f;
                                BossScreen.SetString(0, 1, 0);
                                break;
                            case 2: WeaponManager.shotMinTimer -= 0.01f;
                                BossScreen.SetString(0, 1, 0);
                                break;
                            case 3: Player.maxHealth++;
                                BossScreen.SetString(1, 0, 0);
                                break;
                            case 4: Player.maxHealth++;
                                BossScreen.SetString(1, 0, 0);
                                break;
                            case 5: WeaponManager.bulletSpreadUpgrade++;
                                BossScreen.SetString(0, 0, 1);
                                break;
                            default: break;
                            //} EDITED BY CHRIS
                        }
                        maxenemies += enemyincrease;
                        GameManager.SetState(9);
                        
                    }
                    else if (Player.GodMode == true)
                    {
                        BossScreen.SetString(0, 0, 0);
                        GameManager.SetState(9);
                    }
                    

                }
                BossBase.WorldLocation = BossBase.WorldLocation;
            }
        }



        public void Draw(SpriteBatch spriteBatch)
        {
            if (!Destroyed)//Animates the boss
            {
                if (reverseAnim == false)
                {
                    if (animCounter < 3.8) animCounter += 0.2;
                    else reverseAnim = true;
                }
                if (reverseAnim == true)
                {
                    if (animCounter > -0.2) animCounter -= 0.2;
                    else reverseAnim = false;
                }
                enemyTint = ((255 / maxHealth) * health);
                BossBase.TintColor = Color.FromNonPremultiplied(enemyTint + 150, enemyTint, enemyTint, 255);
                BossBase.Draw(spriteBatch);
                spriteBatch.Draw(hpBar, new Rectangle(0, 60, hpBar.Width, 44), new Rectangle(0, 45, hpBar.Width, 44), Color.Gray);

                //this part doesnt make sense why it doesnt work to me
                if (health == maxHealth)
                {
                    barScale = hpBar.Width;
                }
                else
                {
                    barScale = health * (hpBar.Width / maxHealth);
                }
                spriteBatch.Draw(hpBar, new Rectangle(0, 60, (int)barScale, 44),
                     new Rectangle(0, 45, hpBar.Width, 44), Color.Red);

                spriteBatch.Draw(hpBar, new Rectangle(0, 60, hpBar.Width, 44), new Rectangle(0, 0, hpBar.Width, 44), Color.White);
            }
        }
    }
}
