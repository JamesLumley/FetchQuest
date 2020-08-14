using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FetchQuest
{
    class Objective
    {
        //CLASS CREATED BY CHRIS
        //ART CREATED BY DAN
        private Sprite ObjectiveBase;
        private Sprite shadowSprite;
        public Vector2 MapLocation;
        private double bobAmount = 0;
        private bool reverseAnim;
        public bool Destroyed = false;
        private int collisionRadius = 60;
        

        public Objective(
            Vector2 mapLocation,
            Rectangle initialFrame,
            Texture2D texture)
        {
            
            ObjectiveBase = new Sprite(
                mapLocation,
                texture,
                initialFrame,
                Vector2.Zero);

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    ObjectiveBase.AddFrame(new Rectangle(j*96, i*96, 96, 96));
                }
            }

            ObjectiveBase.CollisionRadius = collisionRadius;

        }

        public bool IsCircleColliding(Vector2 otherCenter, float radius)//checks collision of objective
        {
            return ObjectiveBase.IsCircleColliding(otherCenter, radius);
        }

        public void Deactivate()
        {
            Destroyed = true;
           
           
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            ObjectiveBase.Frame = QuestGenerator.lostObjectSelection + 1;//Animates the objective items so they bob up and down
            if (QuestGenerator.lostObjectSelection != 6 && QuestGenerator.lostObjectSelection != 10)
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
                ObjectiveBase.WorldLocation += new Vector2(-5 - ((float)bobAmount * 1.5f), 8 - (float)bobAmount);
                ObjectiveBase.TintColor = Color.Black;
                ObjectiveBase.Draw(spriteBatch);
            }
            ObjectiveBase.WorldLocation = MapLocation * 96;
            ObjectiveBase.WorldLocation -= new Vector2(0,(float)bobAmount);
            ObjectiveBase.TintColor = Color.White;
            ObjectiveBase.Draw(spriteBatch);
        }

        public void Update(GameTime gameTime)
        {
            if (!Destroyed)
            {
                ObjectiveBase.Update(gameTime);
            }
        }


    }
}
