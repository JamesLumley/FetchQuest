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
    //ART BY: DAN
    class Barrel
    {
        #region Declarations
        public Sprite BarrelBase;

        public float BarrelSpeed = 30f;
        public Vector2 currentTargetSquare;
        public bool Destroyed = false;
        private bool heartSpawn = false;
        private int collisionRadius = 40;
        #endregion

        #region Constructor

        public Barrel(
            Vector2 worldLocation,
            Texture2D texture,
            Rectangle initialFrame)
        {
            BarrelBase = new Sprite(
                worldLocation,
                texture,
                initialFrame,
                Vector2.Zero);
            
            BarrelBase.CollisionRadius = collisionRadius;
            Rectangle BarrelFrame = initialFrame;
            BarrelFrame.Offset(0, initialFrame.Height);
            BarrelBase.AddFrame(new Rectangle(0, 0, 64, 64));
            BarrelBase.AddFrame(new Rectangle(64, 0, 64, 64));
        }
        #endregion

        public void Draw(SpriteBatch spriteBatch)
        {

            if (!Destroyed)
            {
                BarrelBase.Frame = 1;//Unbroken barrel
            }
            else
            {
                BarrelBase.Frame = 2;//Smashed barrel
                if (heartSpawn == false)
                {
                    heartSpawn = true;
                    BarrelManager.AddHeart(new Vector2 (BarrelBase.WorldLocation.X-10,BarrelBase.WorldLocation.Y-30));
                }
            }

            BarrelBase.Draw(spriteBatch);
        }
    }
}
