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
    class BossManager
    {
        #region Declarations
        public static List<Boss> Bosses = new List<Boss>();
        public static Texture2D BossTexture,BossBar;
        public static Rectangle BossInitialFrame;

        #endregion

        #region Initialization

        public static void Initialize(
            Texture2D texture,
            Texture2D healthbar,
            Rectangle initalFrame)
        {
            BossTexture = texture;
            BossBar = healthbar;
            BossInitialFrame = initalFrame;

        }



        #endregion

        #region Boss Management
        public static void AddBoss(Vector2 squareLocation)//Spawns a boss in set location
        {
            int startX = (int)squareLocation.X;
            int startY = (int)squareLocation.Y;

            Rectangle squareRect = TileMap.SquareWorldRectangle(startX, startY);
            Boss newBoss = new Boss(
                new Vector2(squareRect.X, squareRect.Y),
                BossTexture,
                BossBar,
                BossInitialFrame);

            newBoss.currentTargetSquare = squareLocation;
            Bosses.Add(newBoss);
        }
        #endregion

        static public void Update(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            for (int x = Bosses.Count - 1; x >= 0; x--)
            {
                if (Bosses[x].Destroyed)
                {
                    Bosses.RemoveAt(x);
                }
                else
                {
                    Bosses[x].Update(gameTime);
                }
            }
        }

        public static void Draw(SpriteBatch spritebatch)
        {
            foreach (Boss Boss in Bosses)
            {
                Boss.Draw(spritebatch);
            }
        }
    }
}
