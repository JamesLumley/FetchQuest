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
    //CLASS CREATED BY CHRIS
    //ART CREATED BY DAN
    public class MainMenu
    {
        public static Texture2D background;
        public static Texture2D background2,background3,background4,background5,background6;
        public static Texture2D townSheet;
        private static string[] menuItems = {"Start","Tutorial","Credits","Exit"};
        public static int selectedIndex;
        public static Color unselected = Color.Black;
        public static Color selected = Color.White;
        static private Random rand = new Random();
        public static KeyboardState keyboardState;
        public static KeyboardState oldKeyboardState;
        public static GamePadState gamepadState;
        public static GamePadState oldGamepadState;
        public static SoundEffectInstance menuBlipInstance;
        public static SoundEffectInstance menuSelectInstance;

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

        public static void MeasureMenu()//Measures the menu for placement purposes
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

        public static void Update(GameTime gameTime)//Changes the game state and seleceted index depending on the keys the user presses
        {
            keyboardState = Keyboard.GetState();

            if (CheckKey(Keys.Down))
            {
                selectedIndex++;
                if (selectedIndex == menuItems.Length)
                    selectedIndex = 0;
                menuBlipInstance.Volume = 0.75f;
                menuBlipInstance.Play();
            }
            if (CheckKey(Keys.Up))
            {
                selectedIndex--;
                if (selectedIndex < 0)
                    selectedIndex = menuItems.Length - 1;
                menuBlipInstance.Volume = 0.75f;
                menuBlipInstance.Play();
            }
            if (CheckKey(Keys.Enter))
            {
                menuSelectInstance.Volume = 0.75f;
                menuSelectInstance.Play();
                switch (selectedIndex)
                {
                    case 0: GameManager.SetState(11);
                        break;
                    case 1: TileMap.TutorialGenerateMap();
                        GameManager.SetState(13);
                        break;
                    case 2: GameManager.SetState(8);
                        break;
                    case 3: GameManager.SetState(5);
                        break;
                    default: break;
                }
            }
            oldKeyboardState = keyboardState;
            gamepadState = GamePad.GetState(PlayerIndex.One, GamePadDeadZone.None);
            if (CheckButton(Buttons.DPadDown)||CheckButton(Buttons.LeftThumbstickDown))
            {
                menuBlipInstance.Volume = 0.75f;
                menuBlipInstance.Play();
                selectedIndex++;
                if (selectedIndex == menuItems.Length)
                    selectedIndex = 0;
            }
            if (CheckButton(Buttons.DPadUp) || CheckButton(Buttons.LeftThumbstickUp))
            {
                menuBlipInstance.Volume = 0.75f;
                menuBlipInstance.Play();
                selectedIndex--;
                if (selectedIndex < 0)
                    selectedIndex = menuItems.Length - 1;
            }
            if (CheckButton(Buttons.A))
            {
                menuSelectInstance.Volume = 0.75f;
                menuSelectInstance.Play();
                switch (selectedIndex)
                {
                    case 0: GameManager.SetState(11);
                        break;
                    case 1: TileMap.TutorialGenerateMap();
                        GameManager.SetState(10);
                        break;
                    case 2: GameManager.SetState(8);
                        break;
                    case 3: GameManager.SetState(5);
                        break;
                    default: break;
                }
            }
            oldGamepadState = gamepadState;
        }

        public static void Initialize(Texture2D bg, Texture2D bg2, Texture2D townsFolk, SpriteFont font)
        {
            background = bg;
            background2 = bg2;
            townSheet = townsFolk;
            spriteFont = font;
            MeasureMenu();
            
        }
        public static void Draw(SpriteBatch spriteBatch)
        {
            MeasureMenu();
            Vector2 location = position;
            Color tint;
            spriteBatch.Draw(background, Vector2.Zero, Color.White);
            for (int i = 0; i < menuItems.Length; i++)
            {
                if (i == selectedIndex)
                    tint = Color.White;
                else
                    tint = Color.Black;
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
