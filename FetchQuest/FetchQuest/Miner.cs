using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace FetchQuest
{
    class Miner
    {
        #region Declarations
        private Vector2 worldLocation = Vector2.Zero;
        private List<Rectangle> frames = new List<Rectangle>();
        public bool Expired = false;
        #endregion

        public Vector2 WorldLocation
        {
            get { return worldLocation; }
            set { worldLocation = value; }
        }

        public Rectangle WorldRectangle
        {
            get
            {
                return new Rectangle(
                    (int)worldLocation.X,
                    (int)worldLocation.Y,
                    FrameWidth,
                    FrameHeight);
            }
        }

        public Vector2 RelativeCenter
        {
            get { return new Vector2(FrameWidth / 2, FrameHeight / 2); }
        }

        public Vector2 WorldCenter
        {
            get { return worldLocation + RelativeCenter; }
        }

        public int FrameWidth
        {
            get { return frames[0].Width; }
        }

        public int FrameHeight
        {
            get { return frames[0].Height; }
        }

        public static Carve()
        {
        }

        public virtual void Update(GameTime gameTime)
        {
            if (!Expired)
            {
                float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
        }
    }
}

