using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace FetchQuest
{
    class Enemy
    {
        //CLASS CREATED BY: JAMES
        //EDITED BY: DAN & CHRIS
        //ART BY: DAN
        #region Declarations
        #region Declarations
        public Sprite EnemyBase;

        public float EnemySpeed = 200f;
        public Vector2 currentTargetSquare;
        public bool Destroyed = false;
        private double animCounter;
        private bool reverseAnim = false;
        private int collisionRadius = 40;
        private int bulletSpeed = 200;
        private static Random rand = new Random();
        private int enemyType = rand.Next(7);
        private int enemyShotCounter = 0;
        public int health;
        private int maxHealth;
        private int enemyTint;
        #endregion

        #region Constructor
        public Enemy(
            Vector2 worldLocation,
            Texture2D texture,
            Rectangle initialFrame)
        {
            EnemyBase = new Sprite(
                worldLocation,
                texture,
                initialFrame,
                Vector2.Zero);

            //EDITED BY DAN {
            EnemyBase.AddFrame(new Rectangle(96, 0, 96, 96));
            EnemyBase.AddFrame(new Rectangle(0, 96, 96, 96));
            EnemyBase.AddFrame(new Rectangle(96, 96, 96, 96));

            EnemyBase.AddFrame(new Rectangle(182, 0, 96, 96));
            EnemyBase.AddFrame(new Rectangle(96 + 182, 0, 96, 96));
            EnemyBase.AddFrame(new Rectangle(182, 96, 96, 96));
            EnemyBase.AddFrame(new Rectangle(96 + 182, 96, 96, 96));

            EnemyBase.AddFrame(new Rectangle(374, 0, 96, 96));
            EnemyBase.AddFrame(new Rectangle(96 + 374, 0, 96, 96));
            EnemyBase.AddFrame(new Rectangle(374, 96, 96, 96));
            EnemyBase.AddFrame(new Rectangle(96 + 374, 96, 96, 96));

            EnemyBase.CollisionRadius = collisionRadius;
            Rectangle enemyFrame = initialFrame;
            enemyFrame.Offset(0, initialFrame.Height);
            
            //Sets different attributes for different enemies
            if (enemyType > 2 && enemyType < 6) enemyType = 0;
            if (enemyType >= 6 && enemyType < 7) enemyType = 2;

            if (enemyType == 0)
            {
                maxHealth = 2;
                health = maxHealth;
            }
            if (enemyType == 1)
            {
                maxHealth = 10;
                health = maxHealth;
                EnemySpeed = 220f;
            }
            if (enemyType == 2)
            {
                maxHealth = 5;
                health = maxHealth;
                EnemySpeed = 50f;
            }
            //} EDITED BY DAN
        }
        #endregion


        #region AI Methods
        //EDITED BY CHRIS{
        private Vector2 determineMoveDirection()//Choose a location to move to
        {
            if (reachedTargetSquare())
            {
                currentTargetSquare = getNewTargetSquare();
            }
            Vector2 squareCenter = TileMap.GetSquareCenter(currentTargetSquare);
            return squareCenter - EnemyBase.WorldCenter;
        }

        private bool reachedTargetSquare()//Check if enemy is at target square
        {
            return (Vector2.Distance(
                EnemyBase.WorldCenter,
                TileMap.GetSquareCenter(currentTargetSquare)) <= 2);
        }

        private Vector2 getNewTargetSquare()//Find the best path to the character
        {
            List<Vector2> path = PathFinder.FindPath(
                TileMap.GetSquareAtPixel(EnemyBase.WorldCenter),
                TileMap.GetSquareAtPixel(Player.CharacterSprite.WorldCenter));

            if (path != null)
            {
                if (path.Count > 1)
                {
                    return new Vector2(path[1].X, path[1].Y);
                }
                else
                {
                    return TileMap.GetSquareAtPixel(Player.CharacterSprite.WorldCenter);
                }
            }
            else return new Vector2(0, 0);
        }
        #endregion

        public void Update(GameTime gameTime)
        {
            if (!Destroyed)
            {
                Vector2 direction = determineMoveDirection();
                direction.Normalize();

                EnemyBase.Velocity = direction * EnemySpeed;
                EnemyBase.Update(gameTime);

                Vector2 directionToPlayer = Player.CharacterSprite.WorldCenter - EnemyBase.WorldCenter;
                directionToPlayer.Normalize();
                //} EDITED BY CHRIS
                if (health <= 0) Destroyed = true;
                //EDITED BY DAN {
                if (enemyType == 2)
                {
                    enemyShotCounter += 1;
                    if (enemyShotCounter == 40)//Makes the enemy shoot at the player
                    {
                        WeaponManager.AddEnemyShot(EnemyBase.WorldCenter, directionToPlayer * bulletSpeed, 0);
                        enemyShotCounter = 0;
                    }
                }
                //} EDITED BY DAN

                EnemyBase.WorldLocation = EnemyBase.WorldLocation;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!Destroyed)
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
                EnemyBase.Frame = (int)animCounter + (4 * (enemyType));
                enemyTint = ((255 / maxHealth) * health);
                EnemyBase.TintColor = Color.FromNonPremultiplied(enemyTint+150, enemyTint, enemyTint, 255);
                EnemyBase.Draw(spriteBatch);
            }
        }
    }
}

        #endregion
