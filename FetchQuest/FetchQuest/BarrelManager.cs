using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FetchQuest
{
    //CLASS CREATED BY: JAMES
    class BarrelManager
    {
        #region Declarations
        public static List<Barrel> Barrels = new List<Barrel>();//Creates a list of barrels to spawn
        public static List<Heart> Hearts = new List<Heart>();
        public static Texture2D barrelTexture;
        public static Texture2D itemTexture;
        public static Rectangle barrelInitialFrame;
        private static int heartChance = 20;

        #endregion

        #region Initialization

        public static void Initialize(
            Texture2D texture, Texture2D texture2,
            Rectangle initalFrame)
        {
            barrelTexture = texture;
            barrelInitialFrame = initalFrame;
            itemTexture = texture2;
        }

        #endregion

        #region Barrel Management
        public static void AddBarrel(Vector2 squareLocation)//Spawns a barrel on the map in a set location
        {
            int startX = (int)squareLocation.X;
            int startY = (int)squareLocation.Y;

            Rectangle squareRect = TileMap.SquareWorldRectangle(startX, startY);
            Barrel newBarrel = new Barrel(
                new Vector2(squareRect.X, squareRect.Y),
                barrelTexture,
                barrelInitialFrame);

            newBarrel.currentTargetSquare = squareLocation;
            Barrels.Add(newBarrel);
        }
        #endregion

        public static void AddHeart(Vector2 squareLocation)//Spawns a barrel on the map in a set location
        {
            Random rand = new Random();
            int canHeart = rand.Next(0, 100);
            if (canHeart < heartChance)
            {
                Heart newHeart = new Heart(squareLocation, new Rectangle(96, 384, 96, 96), itemTexture);
                Console.WriteLine(squareLocation);
                Hearts.Add(newHeart);
            }
        }

        static public void Update(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            foreach (Heart heart in Hearts)
            {
                heart.Update(gameTime);
            }

        }

        public static void Draw(SpriteBatch spritebatch)
        {
            foreach (Barrel barrel in Barrels)
            {
                barrel.Draw(spritebatch);
            }
            foreach (Heart heart in Hearts)
            {
                heart.Draw(spritebatch);
            }
        }
    }
}
