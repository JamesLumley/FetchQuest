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
    class IntroScreen : MainMenu
    {
        public static void Initialize(Texture2D bg)
        {
            background4 = bg;
        }

        new public static void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();

            if (CheckKey(Keys.Enter))
            {
                GameManager.SetState(6);
            }

            oldKeyboardState = keyboardState;

            gamepadState = GamePad.GetState(PlayerIndex.One, GamePadDeadZone.None);

            if (CheckButton(Buttons.Start))
            {
                GameManager.SetState(6);
            }

            oldGamepadState = gamepadState;

        }

        new public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background4, Vector2.Zero, Color.White);
        }
    }
}
