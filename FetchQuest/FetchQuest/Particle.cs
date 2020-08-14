using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FetchQuest
{
    //CLASS CREATED BY JAMES
    class Particle : Sprite
    {

        #region Declarations

        private Vector2 accelaration;
        private float maxSpeed;
        private int initialDuration;
        private int remainingDuration;
        private Color initialColor;
        private Color finalColor;

        #endregion

        #region Properties

        public int ElapsedDuration
        {
            get
            {
                return initialDuration - remainingDuration;
            }
        }

        public float DurationProgress
        {
            get
            {
                return (float)ElapsedDuration / (float)initialDuration;
            }
        }

        public bool IsActive
        {
            get
            {
                return (remainingDuration > 0);
            }
        }

        #endregion

        #region Constructor

        public Particle(Vector2 location, Texture2D texture, Rectangle initialFrame,
            Vector2 velocity, Vector2 accelaration, float maxSpeed, int duration,
            Color initialColor, Color finalColor)
            : base(location, texture, initialFrame, velocity)
        {
            initialDuration = duration;
            remainingDuration = duration;
            this.accelaration = accelaration;
            this.initialColor = initialColor;
            this.maxSpeed = maxSpeed;
            this.finalColor = finalColor;
        }

        #endregion

        #region Update and Draw

        public override void Update(GameTime gameTime)
        {
            if (remainingDuration <= 0)//Expires the particles are certain time
            {
                Expired = true;
            }

            if (!Expired)
            {
                Velocity += accelaration;
                if (Velocity.Length() > maxSpeed)
                {
                    Vector2 vel = Velocity;
                    vel.Normalize();
                    Velocity = vel * maxSpeed;
                }
                TintColor = Color.Lerp(initialColor, finalColor, DurationProgress);
                remainingDuration--;
            }
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (IsActive)//Draws it only if its active
            {
                base.Draw(spriteBatch);
            }
        }

        #endregion
    }
}
