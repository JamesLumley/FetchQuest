using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FetchQuest
{
    //CLASS CREATED BY: JAMES
    //EDITED BY: DAN
    class EnemyManager
    {

        #region Declarations
        public static List<Enemy> Enemies = new List<Enemy>();
        public static Texture2D enemyTexture;
        public static Rectangle enemyInitialFrame;

        #endregion

        #region Initialization

        public static void Initialize(
            Texture2D texture,
            Rectangle initalFrame)
        {
            enemyTexture = texture;
            enemyInitialFrame = initalFrame;
        }

        #endregion

        #region Enemy Management
        public static void AddEnemy(Vector2 squareLocation)//Spawns an enemy at the specified location
        {
            int startX = (int)squareLocation.X;
            int startY = (int)squareLocation.Y;

            Rectangle squareRect = TileMap.SquareWorldRectangle(startX, startY);
            Enemy newEnemy = new Enemy(
                new Vector2(squareRect.X, squareRect.Y),
                enemyTexture,
                enemyInitialFrame);

            newEnemy.currentTargetSquare = squareLocation;
            Enemies.Add(newEnemy);
        }
        #endregion

        static public void Update(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            for (int x = Enemies.Count - 1; x >= 0; x--)//Checks to see if enemies are destroyed and need to be removed
            {
                if (Enemies[x].Destroyed)
                {
                    Enemies.RemoveAt(x);
                }
                else
                {
                    Enemies[x].Update(gameTime);
                }
            }
        }

        public static void Draw(SpriteBatch spritebatch)
        {
            foreach (Enemy enemy in Enemies)
            {
                enemy.Draw(spritebatch);
            }
        }
    }
}
