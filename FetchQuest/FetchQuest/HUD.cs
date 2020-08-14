using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace FetchQuest
{
    //CLASS CREATED BY: DAN
    //ART CREATED BY: DAN
    class HUD
    {
        public static Sprite heartSprite;
        public static Sprite percentSprite;
        public static Sprite objectiveSprite;
        public static Sprite controlSprite;
        //public static Sprite bossBar;
        //public static Sprite bossframe;
        private static Vector2 heartPosition;
        private static Vector2 objectivePosition;
        private static Vector2 bossbarposition;
        private static int completion = GameManager.CurrentLevel+2;
        private static Vector2 percentPosition;
        private static int spriteSize = 48;
        private static double animCounter;
        private static bool reverseAnim = false;
        public static float controlsAlphaCounter = 0;
        public static void Initialize(Texture2D texture, Texture2D items)
        {

            heartSprite = new Sprite(Vector2.Zero, texture, new Rectangle(0, 0, 48, 48), Vector2.Zero);
            heartSprite.AddFrame(new Rectangle(spriteSize, 0, spriteSize, spriteSize));
            heartSprite.AddFrame(new Rectangle(0, spriteSize, spriteSize, spriteSize));
            heartSprite.AddFrame(new Rectangle(spriteSize, spriteSize, spriteSize, spriteSize));
            percentSprite = new Sprite(Vector2.Zero, texture, new Rectangle(spriteSize*2, 0, spriteSize, spriteSize), Vector2.Zero);
            percentSprite.AddFrame(new Rectangle(spriteSize * 3, 0, spriteSize, spriteSize));
            percentSprite.AddFrame(new Rectangle(spriteSize * 2, spriteSize, spriteSize, spriteSize));
            //bossBar = new Sprite(Vector2.Zero, hpBar, new Rectangle(0, 45, (int)(hpBar.Width * ((double)(BossManager/Boss.maxHealth))), 44), Vector2.Zero);
           // bossframe = new Sprite(Vector2.Zero,hpBar,new Rectangle(0, 0, hpBar.Width, 44),Vector2.Zero);
            objectiveSprite = new Sprite(Vector2.Zero, items, new Rectangle(0, 0, 96, 96), Vector2.Zero);
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    objectiveSprite.AddFrame(new Rectangle(j * 96, i * 96, 96, 96));
                }
            }      
            controlSprite = new Sprite(Vector2.Zero, texture, new Rectangle(0, 96, 512, 512), Vector2.Zero);
        }

        static public void Draw(SpriteBatch spriteBatch)
        {
            if (reverseAnim == false)
            {
                if (animCounter < 4) animCounter += 0.2;
                else reverseAnim = true;
            }
            if (reverseAnim == true)
            {
                if (animCounter > -1) animCounter -= 0.2;
                else reverseAnim = false;
            }

            for (int i = 0; i < Player.maxHealth; i++)//Shows health display
            {
                heartPosition.X = Camera.Position.X + 10 + (i * 48);
                heartPosition.Y = Camera.Position.Y + 10;
                heartSprite.WorldLocation = heartPosition;
                heartSprite.Frame = (int)animCounter;
                if (Player.health <= i) heartSprite.TintColor = Color.Black;
                else heartSprite.TintColor = Color.White;
                heartSprite.Draw(spriteBatch);
            }


            for (int i = 0; i < 3; i++)
            {
                if (GameManager.CurrentLevel % LevelGenerator.bossSpawn != 0)
                {
                    objectivePosition.X = Camera.Position.X + Camera.ViewPortWidth - 96 - (48 * i);
                    objectivePosition.Y = Camera.Position.Y + 10;
                    objectiveSprite.WorldLocation = objectivePosition;
                    objectiveSprite.Frame = QuestGenerator.lostObjectSelection + 1;
                    if (i >= GoalManager.pickedup) objectiveSprite.TintColor = Color.Black;
                    else objectiveSprite.TintColor = Color.White;
                    objectiveSprite.Draw(spriteBatch);
                }
            }
            //if(GameManager.CurrentLevel % LevelGenerator.bossSpawn == 0)
               // {
              //      bossbarposition.X = Camera.Position.X + (Camera.ViewPortWidth/ 4);
              //      bossbarposition.Y = Camera.Position.Y + 100;
             //       bossBar.WorldLocation = bossbarposition;
              //      bossBar.Draw(spriteBatch);
              //      bossframe.WorldLocation = bossbarposition;
              //      bossframe.Draw(spriteBatch);
            //    }

            completion = GameManager.CurrentLevel + 2;

            for(int i = 0; i < completion + 1; i++)//Shows completion percent
            {
                percentPosition.X = Camera.Position.X + Camera.ViewPortWidth - 58 - (((completion + 1)- i) * 38);
                percentPosition.Y = Camera.Position.Y + Camera.ViewPortHeight - 58;
                percentSprite.WorldLocation = percentPosition;

                if (i == completion) percentSprite.Frame = 1;

                percentSprite.Draw(spriteBatch);

                percentSprite.Frame = 0;

                if (i == 1)
                {
                    percentSprite.Frame = 2;
                    percentPosition.X += 10;
                    percentSprite.WorldLocation = percentPosition;
                    percentSprite.Draw(spriteBatch);
                    percentSprite.Frame = 0;
                }

                if (GameManager.InTutorial)//Shows controls in the tutorial level
                {
                    controlsAlphaCounter += 0.2f;
                    controlSprite.WorldLocation = new Vector2(Camera.Position.X + 300, Camera.Position.Y + 100);
                    controlSprite.TintColor = Color.FromNonPremultiplied(255, 255, 255, 255 - (int)controlsAlphaCounter);
                    controlSprite.Draw(spriteBatch);
                }

                if(Player.shootInstance.IsDisposed)
                {
                    Camera.Move(new Vector2(4, 4));
                }
            }
        }
    }
}
