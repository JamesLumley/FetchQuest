using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace FetchQuest
{
    //CLASS CREATED BY: CHRIS
    //ART BY: CHRIS
    class Credits
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
        private static bool CheckKey(Keys theKey)//Returns if the pressed key is up and down
        {
            return keyboardState.IsKeyUp(theKey) &&
                oldKeyboardState.IsKeyDown(theKey);
        }

        private static bool CheckButton(Buttons theButton)//Returns if the pressed button is up and down
        {
            return gamepadState.IsButtonUp(theButton) &&
                oldGamepadState.IsButtonDown(theButton);
        }

        public static void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();

            if (CheckKey(Keys.Back))
            {
                GameManager.SetState(2);//Go to main menu
            }

            oldKeyboardState = keyboardState;

            gamepadState = GamePad.GetState(PlayerIndex.One, GamePadDeadZone.None);

            if (CheckButton(Buttons.Back))
            {
                GameManager.SetState(2);//Go to main menu
            }

            oldGamepadState = gamepadState;
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, Vector2.Zero, Color.White);
        }

    }
}
