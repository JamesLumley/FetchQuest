using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FetchQuest
{
    class Difficulty : MainMenu
    {
        private static string[] menuItems = { "Normal", "Hard", "Nightmare" };

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
                    case 0:
                        GameManager.CurrentLevel = 1;
                        WeaponManager.bulletSizeUpgrade = 0;
                        WeaponManager.bulletSpreadUpgrade = 0;
                        WeaponManager.weaponFireRateUpgrade = 0;
                        Player.maxHealth = 3;
                        Boss.maxenemies = 5;
                        LevelGenerator.enemymax = 5;
                        GameManager.enemyincrease = 2;
                        Boss.enemyincrease = 2;
                        Boss.healthincrease = 10;
                        GameManager.SetState(12);
                        break;
                    case 1:
                        GameManager.CurrentLevel = 1;
                        WeaponManager.bulletSizeUpgrade = 0;
                        WeaponManager.bulletSpreadUpgrade = 0;
                        WeaponManager.weaponFireRateUpgrade = 0;
                        Player.maxHealth = 3;
                        Boss.maxenemies = 10;
                        LevelGenerator.enemymax = 10;
                        GameManager.enemyincrease = 4;
                        Boss.enemyincrease = 4;
                        Boss.healthincrease = 20;
                        GameManager.SetState(12);
                        break;
                    case 2:
                        GameManager.CurrentLevel = 1;
                        WeaponManager.bulletSizeUpgrade = 0;
                        WeaponManager.bulletSpreadUpgrade = 0;
                        WeaponManager.weaponFireRateUpgrade = 0;
                        Player.maxHealth = 3;
                        Boss.maxenemies = 20;
                        LevelGenerator.enemymax = 20;
                        GameManager.enemyincrease = 5;
                        Boss.enemyincrease = 6;
                        Boss.healthincrease = 50;
                        GameManager.SetState(12);
                        break;
                    default: break;
                }
                selectedIndex = 0;
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
                    case 0:
                        GameManager.CurrentLevel = 1;
                        WeaponManager.bulletSizeUpgrade = 0;
                        WeaponManager.bulletSpreadUpgrade = 0;
                        WeaponManager.weaponFireRateUpgrade = 0;
                        Player.maxHealth = 3;
                        Boss.maxenemies = 5;
                        LevelGenerator.enemymax = 5;
                        GameManager.enemyincrease = 2;
                        GameManager.SetState(12);
                        break;
                    case 1:
                        GameManager.CurrentLevel = 1;
                        WeaponManager.bulletSizeUpgrade = 0;
                        WeaponManager.bulletSpreadUpgrade = 0;
                        WeaponManager.weaponFireRateUpgrade = 0;
                        Player.maxHealth = 3;
                        Boss.maxenemies = 10;
                        LevelGenerator.enemymax = 10;
                        GameManager.enemyincrease = 4;
                        GameManager.SetState(12);
                        break;
                    case 2:
                        GameManager.CurrentLevel = 1;
                        WeaponManager.bulletSizeUpgrade = 0;
                        WeaponManager.bulletSpreadUpgrade = 0;
                        WeaponManager.weaponFireRateUpgrade = 0;
                        Player.maxHealth = 3;
                        Boss.maxenemies = 20;
                        LevelGenerator.enemymax = 20;
                        GameManager.enemyincrease = 5;
                        GameManager.SetState(12);
                        break;
                    default: break;
                }
                selectedIndex = 0;
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
