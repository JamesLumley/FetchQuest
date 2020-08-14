using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace FetchQuest
{
    //CLASS CREATED BY CHRIS
    //CLASS EDITED BY JAMES
    //ART CREATED BY DAN
    public class PauseMenu : MainMenu
    {
        private static string[] menuItems = { "Resume", "Quit" };
        public static void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();

            if (CheckKey(Keys.Down))//Checks the keyboard state to navigate the menu
            {
                menuBlipInstance.Volume = 0.75f;
                menuBlipInstance.Play();
                selectedIndex++;
                if (selectedIndex == menuItems.Length)
                    selectedIndex = 0;
            }
            if (CheckKey(Keys.Up))
            {
                menuBlipInstance.Volume = 0.75f;
                menuBlipInstance.Play();
                selectedIndex--;
                if (selectedIndex < 0)
                    selectedIndex = menuItems.Length - 1;
            }
            if (CheckKey(Keys.Enter))
            {
                menuSelectInstance.Volume = 0.75f;
                menuSelectInstance.Play();
                switch (selectedIndex)
                {
                //EDITED BY JAMES{
                    case 0:
                        if (Player.GodMode == true)//Checks to see whether the game is in the tutorial or not
                        {
                            GameManager.SetState(10);//if in tutorial 
                        }
                        else if (Player.GodMode == false)
                        {
                            GameManager.SetState(4);
                        }
                        break;
                    case 1: GameManager.SetState(2);
                        break;
                    default: break;
                        //}EDITED BY JAMES
                }
            }
            oldKeyboardState = keyboardState;
            gamepadState = GamePad.GetState(PlayerIndex.One, GamePadDeadZone.None);
            if (CheckButton(Buttons.DPadDown) || CheckButton(Buttons.LeftThumbstickDown))
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
                    //EDITED BY JAMES{
                    case 0:
                        if (Player.GodMode == true)
                        {
                            GameManager.SetState(10);
                        }
                        else if (Player.GodMode == false)
                        {
                            GameManager.SetState(4);
                        }
                        break;
                    case 1: GameManager.SetState(2);
                        break;
                    default: break;
                    //}EDITED BY JAMES
                }
            }
            oldGamepadState = gamepadState;
        }
        new public static void Draw(SpriteBatch spriteBatch)//Draws the pause menu
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
