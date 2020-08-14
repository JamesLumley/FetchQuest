using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace FetchQuest
{
    //CLASS CREATED BY JAMES
    //CLASS EDITED BY DAN
    //ART CREATED BY DAN
    class QuestGenerator : MainMenu
    {
        public static SoundEffectInstance menuSelectInstance; 
        static String[] lostObject = new String[] { 
            "baby",
            "watermelon",
            "doll",     
            "lollypop",
            "bucket",
            "alcoholic beverage",
            "mother",
            "toaster",
            "GameBoy(TM)",
            "brush",
            "fridge",
            "marbles",
            "fried egg",
            "bible",
            "underwear",
            "screwdriver"
        };
        static String[] lostWhileDoing = new String[]{
            "dancing",
            "hunting",
            "washing my horse",
            "alone",
            "with my girlfriend",
            "with my boyfriend",
            "with my dad",
            "eating chocolate",
            "fighting rats",
            "writing poetry",
            "having a party",
            "crying"


        };

        static String[] lostIn = new String[]{
            "the forest",
            "the castle",
            "the dark cave",
        };

        static private Random rand = new Random();
        public static String Quest = questCreator();
        public static int lostObjectSelection = rand.Next(0, lostObject.Length);
        public static int lostObjectLocation = rand.Next(0, lostIn.Length);



        public static string questCreator() //Randomizes the quest text
        {
            lostObjectSelection = rand.Next(0, lostObject.Length);
            lostObjectLocation = rand.Next(0, lostIn.Length);
            String questText = "Help! I lost my " + lostObject[lostObjectSelection] + " whilst I was " +
                lostWhileDoing[rand.Next(0, lostWhileDoing.Length)] + " in " + lostIn[lostObjectLocation];

            return questText;

        }

        
        //EDITED BY DAN{
        public static Sprite townFolkHead;
        public static Sprite townFolkFace;
        public static Sprite townFolkBody;
        //Used to randomize what the townfolk on the quest screen look like
        public static void TownFolkSet()
        {
            int spriteSize = 256;
            townFolkHead = new Sprite(new Vector2(450, 300), townSheet, new Rectangle(0, 0, spriteSize, spriteSize), Vector2.Zero);
            for (int i = 0; i < 4; i++) townFolkHead.AddFrame(new Rectangle(i * spriteSize, 0, spriteSize, spriteSize));

            townFolkFace = new Sprite(new Vector2(450, 300), townSheet, new Rectangle(0, spriteSize, spriteSize, spriteSize), Vector2.Zero);
            for (int i = 1; i < 3; i++)
            {
                for (int j = 0; j < 4; j++) townFolkFace.AddFrame(new Rectangle(j * spriteSize, i * spriteSize, spriteSize, spriteSize));
            }

            townFolkBody = new Sprite(new Vector2(450, 300), townSheet, new Rectangle(spriteSize * 3, 0, spriteSize, spriteSize), Vector2.Zero);
            for (int i = 0; i < 4; i++) townFolkBody.AddFrame(new Rectangle(i * spriteSize, spriteSize * 3, spriteSize, spriteSize));


            townFolkFace.Frame = rand.Next(0, 9);
            townFolkBody.Frame = rand.Next(1, 5);
            townFolkHead.Frame = rand.Next(0, 5);
        }

        new public static void Update(GameTime gameTime)
        {
            
            if ((Keyboard.GetState().IsKeyDown(Keys.Enter)) || (GamePad.GetState(PlayerIndex.One,GamePadDeadZone.None).IsButtonDown(Buttons.A)))
            {
                menuSelectInstance.Volume = 0.75f;
                menuSelectInstance.Play();
                GoalManager.pickedup = 0;
                TileMap.Generate();
                Random coordinatePick = new Random();
                Vector2 playerSpawnPosition = Vector2.Zero;
                Vector2 tempSpawnPosition = Vector2.Zero;

                while(playerSpawnPosition == Vector2.Zero)
                {
                    tempSpawnPosition = new Vector2(coordinatePick.Next(TileMap.MapWidth), coordinatePick.Next(TileMap.MapHeight));
                    if (TileMap.PlayerSpawnSolitary((int)tempSpawnPosition.X, (int)tempSpawnPosition.Y) == true) playerSpawnPosition = tempSpawnPosition * TileMap.TileWidth;
                }

                Player.CharacterSprite.WorldLocation = playerSpawnPosition; //new Vector2(((Camera.WorldRectangle.Right / 2) + 30), Camera.WorldRectangle.Bottom - 270);

                LevelGenerator.Generate();

                //Player.CharacterSprite.WorldLocation = new Vector2(Camera.WorldRectangle.Right / 2, Camera.WorldRectangle.Bottom - 300);
                Player.health = Player.maxHealth;
                GameManager.SetState(4);
            }
        }//}EDITED BY DAN

        new public static void Draw(SpriteBatch spriteBatch)
        {
            position = new Vector2(200, 580);
            Vector2 location = position;

            spriteBatch.Draw(background2, Vector2.Zero, Color.White);

            townFolkBody.Draw(spriteBatch);
            townFolkHead.Draw(spriteBatch);
            townFolkFace.Draw(spriteBatch);

            spriteBatch.DrawString(
                      spriteFont,
                      Quest,
                      location,
                      Color.Black);


        }
    }
}

