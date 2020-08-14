using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FetchQuest
{
    class Heart
    {
        //CLASS CREATED BY CHRIS
        //ART CREATED BY DAN
        private Sprite HeartBase;
        private Sprite shadowSprite;
        public Vector2 MapLocation;
        private double bobAmount = 0;
        private bool reverseAnim;
        public bool Destroyed = false;
        private int collisionRadius = 60;


        public Heart(
            Vector2 mapLocation,
            Rectangle initialFrame,
            Texture2D texture)
        {

            HeartBase = new Sprite(
                mapLocation,
                texture,
                initialFrame,
                Vector2.Zero);

            HeartBase.CollisionRadius = collisionRadius;
            MapLocation = HeartBase.WorldLocation;
            HeartBase.AddFrame(new Rectangle(96,382,96,96));
            HeartBase.Frame = 2;
        }

        public bool IsCircleColliding(Vector2 otherCenter, float radius)//checks collision of objective
        {
            return HeartBase.IsCircleColliding(otherCenter, radius);
        }

        public void Deactivate()
        {
            Destroyed = true;
            Player.health += 1;
            if (Player.health > Player.maxHealth) Player.health = Player.maxHealth;
            Console.WriteLine("HEALTH PICKUP!");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!Destroyed)
            {
                if (reverseAnim == false)
                {
                    if (bobAmount < 7) bobAmount += 0.1;
                    else reverseAnim = true;
                }
                if (reverseAnim == true)
                {
                    if (bobAmount > 0) bobAmount -= 0.1;
                    else reverseAnim = false;
                }

                HeartBase.WorldLocation += new Vector2(-5 - ((float)bobAmount * 1.5f), 8 - (float)bobAmount);
                HeartBase.TintColor = Color.Black;
                HeartBase.Draw(spriteBatch);

                HeartBase.WorldLocation = MapLocation;
                HeartBase.WorldLocation -= new Vector2(0, (float)bobAmount);
                HeartBase.TintColor = Color.White;
                HeartBase.Draw(spriteBatch);

                if (IsCircleColliding(Player.CharacterSprite.WorldCenter, collisionRadius)) Deactivate();
            }
        }

        public void Update(GameTime gameTime)
        {
            if (!Destroyed)
            {
                HeartBase.Update(gameTime);
            }
        }


    }
}
