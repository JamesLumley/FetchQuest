using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace FetchQuest
{
    //CLASS CREATED BY: CHRIS
    //ART BY: DAN
    class BossScreen
    {
        public static string[] menuItems = { "Health:          +", 
                                             "Bullet Speed:   +",
                                             "Bullet Spread:    +"};
        protected static Texture2D background;
        public static Color unselected = Color.Black;

        public static KeyboardState keyboardState;
        public static KeyboardState oldKeyboardState;
        public static GamePadState gamepadState;
        public static GamePadState oldGamepadState;

        public static SpriteFont spriteFont;

        public static Vector2 position;
        public static float width = 0f;
        public static float height = 0f;

        public static void MeasureMenu()
        {
            height = 0;
            width = 0;
            foreach (string item in menuItems)
            {
                Vector2 size = spriteFont.MeasureString(item);
                if (size.X > width)
                    width = size.X;
                height += spriteFont.LineSpacing + 5;
            }

            position = new Vector2((Camera.ViewPortWidth - width) / 2, (Camera.ViewPortHeight - height) / 2 + 150);
        }

        public static bool CheckKey(Keys theKey)
        {
            return keyboardState.IsKeyUp(theKey) &&
                oldKeyboardState.IsKeyDown(theKey);
        }

        public static bool CheckButton(Buttons theButton)
        {
            return gamepadState.IsButtonUp(theButton) &&
                oldGamepadState.IsButtonDown(theButton);
        }

        public static void Initialize(Texture2D bg, SpriteFont font)
        {
            background = bg;
            spriteFont = font;
            MeasureMenu();
        }

        public static void SetString(int a, int b, int c)
        {
            menuItems[0] = "Health:          +" + a;
            menuItems[1] = "Bullet Speed:   +" + b;
            menuItems[2] = "Bullet Spread:    +" + c;
        }

        public static void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();
            
            if (CheckKey(Keys.Enter))
            {
                if (Player.GodMode == true)
                {
                    GameManager.SetState(2);
                }
                else if (Player.GodMode == false)
                {
                GameManager.NewLevel();
                }
            }
            oldKeyboardState = keyboardState;
            gamepadState = GamePad.GetState(PlayerIndex.One, GamePadDeadZone.None);
            if (CheckButton(Buttons.A))
            {
                if (Player.GodMode == true)
                {
                    GameManager.SetState(2);
                }
                else if (Player.GodMode == false)
                {
                    GameManager.NewLevel();
                }
            }
            oldGamepadState = gamepadState;
        }

        new public static void Draw(SpriteBatch spriteBatch)
        {
            MeasureMenu();
            Vector2 location = position;
            Color tint;
            spriteBatch.Draw(background, Vector2.Zero, Color.White);
            for (int i = 0; i < menuItems.Length; i++)
            {
                tint = unselected;
                spriteBatch.DrawString(
                    spriteFont,
                    menuItems[i],
                    location,
                    Color.White);
                location.Y += spriteFont.LineSpacing + 5;
            }
        }
    }
}
