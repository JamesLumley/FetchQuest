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
    class BossQuestScreen : MainMenu
    {
        public static void Initialize(Texture2D bg)
        {
            background3 = bg;
        }

        new public static void Update(GameTime gameTime)
        {

            if ((Keyboard.GetState().IsKeyDown(Keys.Enter)) || (GamePad.GetState(PlayerIndex.One, GamePadDeadZone.None).IsButtonDown(Buttons.A)))
            {
                menuSelectInstance.Volume = 0.75f;
                menuSelectInstance.Play();
                GoalManager.pickedup = 0;
                TileMap.Generate();
                Random coordinatePick = new Random();
                Vector2 playerSpawnPosition = Vector2.Zero;
                Vector2 tempSpawnPosition = Vector2.Zero;

                while (playerSpawnPosition == Vector2.Zero)
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
            spriteBatch.Draw(background3, Vector2.Zero, Color.White);
        }
    }
}
