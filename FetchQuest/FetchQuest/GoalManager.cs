using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace FetchQuest
{
    //CLASS CREATED BY: CHRIS
    //OBJECTIVE ART CREATED BY: DAN
    class GoalManager
    {
        public static List<Objective> Objectives = new List<Objective>();
        public static int activeCount = 0;
        public static int pickedup = 0;
        Random rand = new Random();
        private static Texture2D texture;
        private static Rectangle Frame;
        private static SoundEffectInstance pickUpInstance;

        public static void Initialize(
            Texture2D textureSheet,
            Rectangle initialRectangle,
            SoundEffect pickUp)
        {
            texture = textureSheet;
            Frame = initialRectangle;
            pickUpInstance = pickUp.CreateInstance();
        }

        public static void DetectPickup()//Checks to see if a objective is being picked up
        {
            foreach (Objective objective in Objectives)
            {
                if (objective.IsCircleColliding(
                        Player.CharacterSprite.WorldCenter,
                        Player.CharacterSprite.CollisionRadius))
                    {
                        objective.Deactivate();
                        activeCount--;
                        pickedup++;
                        pickUpInstance.Volume = 0.75f;
                        pickUpInstance.Play();
                    }
                }
        }

        public static void PlaceObjective(Vector2 squareLocation)//Places objectives in their random location
        {
            int startX = (int)squareLocation.X;
            int startY = (int)squareLocation.Y;
            Rectangle squareRect = TileMap.SquareWorldRectangle(startX, startY);
            Objective newObjective = new Objective(new Vector2(squareRect.X, squareRect.Y), Frame, texture);
            newObjective.MapLocation = squareLocation;
            Objectives.Add(newObjective);
        }

        public static void Update(GameTime gameTime)
        {
            DetectPickup();
            foreach (Objective objective in Objectives)
            {
                objective.Update(gameTime);
            }

            for (int x = Objectives.Count - 1; x >= 0; x--)
            {
                if (Objectives[x].Destroyed)
                {
                    Objectives.RemoveAt(x);
                }
            }
            if (pickedup == 3)
            {
                pickedup = 0;
                GameManager.NewLevel();
            }
        }
       
        public static void Draw(SpriteBatch spriteBatch)
        {
            foreach (Objective objective in Objectives)
            {
                objective.Draw(spriteBatch);
            }
        }
    }
}

