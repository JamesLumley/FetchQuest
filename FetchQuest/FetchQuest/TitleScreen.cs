using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace FetchQuest
{
    //CLASS CREATED BY CHRIS
    //ART CREATED BY DAN
    class TitleScreen
    {
        public static Texture2D background;
        public static KeyboardState keyboardState;
        public static KeyboardState oldKeyboardState;
        public static GamePadState gamepadState;
        public static GamePadState oldGamepadState;
        public static void Initialize(Texture2D bg)
        {
            background = bg;
        }
        private static bool CheckKey(Keys theKey)
        {
            return keyboardState.IsKeyUp(theKey) &&
                oldKeyboardState.IsKeyDown(theKey);
        }

        private static bool CheckButton(Buttons theButton)
        {
            return gamepadState.IsButtonUp(theButton) &&
                oldGamepadState.IsButtonDown(theButton);
        }

        public static void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();

            if (CheckKey(Keys.Enter))
            {
                GameManager.SetState(2);
            }
            
            oldKeyboardState = keyboardState;

            gamepadState = GamePad.GetState(PlayerIndex.One, GamePadDeadZone.None);

            if (CheckButton(Buttons.Start))
            {
                GameManager.SetState(2); 
            }
            
            oldGamepadState = gamepadState;
        }
        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, Vector2.Zero, Color.White);
        }
    }
}
