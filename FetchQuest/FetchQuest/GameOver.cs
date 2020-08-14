using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace FetchQuest
{
    class GameOver
    {
        //CLASS CREATED BY: CHRIS
        //ART BY: DAN
        private static string[] menuItems = { "Continue","Quit" };
        protected static Texture2D background;
        public static int selectedIndex;
        public static Color unselected = Color.Black;
        public static Color selected = Color.White;

        public static KeyboardState keyboardState;
        public static KeyboardState oldKeyboardState;
        public static GamePadState gamepadState;
        public static GamePadState oldGamepadState;

        public static SpriteFont spriteFont;

        public static Vector2 position;
        public static float width = 0f;
        public static float height = 0f;

        public int SelectedIndex
        {
            get { return selectedIndex; }
            set
            {
                selectedIndex = value;
                if (selectedIndex < 0)
                    selectedIndex = 0;
                if (selectedIndex >= menuItems.Length)
                    selectedIndex = menuItems.Length - 1;
            }
        }

        public static void MeasureMenu()//Used for placement of menu
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

            position = new Vector2((Camera.ViewPortWidth - width) / 2, (Camera.ViewPortHeight - height) / 2);
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

        new public static void Initialize(Texture2D bg, SpriteFont font)
        {
            GameOver.background = bg;
            GameOver.spriteFont = font;
            MeasureMenu();
        }

        new public static void Update(GameTime gameTime)//Checks what the user is doing
        {
            keyboardState = Keyboard.GetState();

            if (CheckKey(Keys.Down))
            {
                selectedIndex++;
                if (selectedIndex == menuItems.Length)
                    selectedIndex = 0;
            }
            if (CheckKey(Keys.Up))
            {
                selectedIndex--;
                if (selectedIndex < 0)
                    selectedIndex = menuItems.Length - 1;
            }
            if (CheckKey(Keys.Enter))
            {
                switch (selectedIndex)
                {
                    case 0: GameManager.SetState(6);
                        break;
                    case 1: GameManager.SetState(2);
                        break;
                    default: break;
                }
            }
            oldKeyboardState = keyboardState;
            gamepadState = GamePad.GetState(PlayerIndex.One, GamePadDeadZone.None);
            if (CheckButton(Buttons.DPadDown) || CheckButton(Buttons.LeftThumbstickDown))
            {
                selectedIndex++;
                if (selectedIndex == menuItems.Length)
                    selectedIndex = 0;
            }
            if (CheckButton(Buttons.DPadUp) || CheckButton(Buttons.LeftThumbstickUp))
            {
                selectedIndex--;
                if (selectedIndex < 0)
                    selectedIndex = menuItems.Length - 1;
            }
            if (CheckButton(Buttons.A))
            {
                switch (selectedIndex)
                {
                    case 0: GameManager.SetState(6);
                        break;
                    case 1: GameManager.SetState(2);
                        break;
                    default: break;
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
                if (i == selectedIndex)
                    tint = selected;
                else
                    tint = unselected;
                spriteBatch.DrawString(
                    spriteFont,
                    menuItems[i],
                    location,
                    tint);
                location.Y += spriteFont.LineSpacing + 5;
            }
        }
        

    }
}
