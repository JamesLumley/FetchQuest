using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace FetchQuest
{
    //EDITED BY: JAMES, DAN, CHRIS
    //ART BY: DAN
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont spriteFont;
        //ART IMPLEMENTED BY: DAN
        Texture2D
            forestSheet,
            dungeonSheet,
            caveSheet,
            objectSheet,
            playerSheet,
            barrelSheet,
            titleSheet,
            menuSheet,
            HUDSheet,
            enemySheet,
            itemSheet,
            goalSheet,
            townSheet,
            gameOverSheet,
            creditsSheet,
            bossSheet,
            bossScreenSheet,
            bossquestsheet,
            controlsheet,
            enemysheet,
            introsheet,
            bossBar;
        //SOUND IMPLEMENTED BY: JAMES
        SoundEffect
            soundStep,
            soundShot,
            soundPickUpKeyItem,
            soundExplosion,
            soundBGSound,
            soundPotBreak,
            soundWallHit,
            soundMenuBlip,
            bossTrack,
            creditsTrack,
            mainTheme,
            soundMenuSelect;
      

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            this.graphics.PreferredBackBufferWidth = 1098;
            this.graphics.PreferredBackBufferHeight = 636;
            this.graphics.ApplyChanges();
            base.Initialize();
            GameManager.SetState(1);

        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            forestSheet = Content.Load<Texture2D>(@"Textures\Forest\Tileset_Scaled");
            dungeonSheet = Content.Load<Texture2D>(@"Textures\Dungeon\Tileset_Scaled");
            caveSheet = Content.Load<Texture2D>(@"Textures\Cave\Tileset_Scaled");
            HUDSheet = Content.Load<Texture2D>(@"Textures\HUDsheet");
            objectSheet = Content.Load<Texture2D>(@"Textures\objectSheet");
            itemSheet = Content.Load<Texture2D>(@"Textures\Items_scaled");
            enemySheet = Content.Load<Texture2D>(@"Textures\enemy_tileset_scaled");
            playerSheet = Content.Load<Texture2D>(@"Textures\Player_Scaled");
            barrelSheet = Content.Load<Texture2D>(@"Textures\Barrels_Scaled");
            titleSheet = Content.Load<Texture2D>(@"Textures\TitleScreen");
            menuSheet = Content.Load<Texture2D>(@"Textures\MenuScreen");
            goalSheet = Content.Load<Texture2D>(@"Textures\QuestBacking");
            townSheet = Content.Load<Texture2D>(@"Textures\Townsfolk_Full");
            spriteFont = Content.Load<SpriteFont>(@"MenuFont");
            soundStep = Content.Load<SoundEffect>(@"Audio\step_hard_terrain");
            soundShot = Content.Load<SoundEffect>(@"Audio\shoot_1");
            soundExplosion = Content.Load<SoundEffect>(@"Audio\explosion_4");
            soundPickUpKeyItem = Content.Load<SoundEffect>(@"Audio\pickup_keyitem");
            soundBGSound = Content.Load<SoundEffect>(@"Audio\Main_2_loops");
            soundPotBreak = Content.Load<SoundEffect>(@"Audio\Explosion_2");
            soundWallHit = Content.Load<SoundEffect>(@"Audio\Explosion_1");
            soundMenuBlip = Content.Load<SoundEffect>(@"Audio\menu_blip");
            soundMenuSelect = Content.Load<SoundEffect>(@"Audio\menu_select");
            bossTrack = Content.Load<SoundEffect>(@"Audio\bosstrack");
            creditsTrack = Content.Load<SoundEffect>(@"Audio\creditstrack");
            mainTheme = Content.Load<SoundEffect>(@"Audio\maintheme");
            gameOverSheet = Content.Load<Texture2D>(@"Textures\GameOver");
            creditsSheet = Content.Load<Texture2D>(@"Textures\CreditsScreen");
            bossSheet = Content.Load<Texture2D>(@"Textures\boss_tileset_scaled");
            bossScreenSheet = Content.Load<Texture2D>(@"Textures\BossDefeated");
            bossquestsheet = Content.Load<Texture2D>(@"Textures\BossScreen");
            introsheet = Content.Load<Texture2D>(@"Textures\Intro");
            controlsheet = Content.Load<Texture2D>(@"Textures\ControlScreen");
            enemysheet = Content.Load<Texture2D>(@"Textures\EnemyDetail");
            bossBar = Content.Load<Texture2D>(@"Textures\HealthBar");

            Camera.WorldRectangle = new Rectangle(0, 0, 3200, 4400);
            Camera.ViewPortWidth = 1098;
            Camera.ViewPortHeight = 636;

            TileMap.Initialize(forestSheet,dungeonSheet,caveSheet);
            Player.Initialize(playerSheet,
                new Rectangle(384, 0, 48, 48),
                1,
                new Rectangle(34, 0, 10, 5),
                1,
                new Vector2((Camera.WorldRectangle.Right / 2), (Camera.WorldRectangle.Bottom - 300)),
                soundStep, soundShot);

            EffectsManager.Initialize(objectSheet,barrelSheet,
                new Rectangle(0, 288, 2, 2),
                new Rectangle(0, 256, 32, 32),
                3);

            WeaponManager.Texture = objectSheet;
            WeaponManager.soundExplosionInstance = soundExplosion.CreateInstance();
            WeaponManager.soundPotBreakInstance = soundPotBreak.CreateInstance();
            WeaponManager.soundWallHitInstance = soundWallHit.CreateInstance();

            GameManager.bgSound = mainTheme.CreateInstance();
            GameManager.creditSound = creditsTrack.CreateInstance();
            GameManager.bossSound = bossTrack.CreateInstance();


            EnemyManager.Initialize(
                enemySheet,
                new Rectangle(0, 0, 96, 96));

            BarrelManager.Initialize(barrelSheet,itemSheet,
                new Rectangle(0, 0, 48, 48));
            
            BossManager.Initialize(bossSheet,bossBar,
                new Rectangle(0, 0, 192, 192));

            HUD.Initialize(HUDSheet, itemSheet);

            GoalManager.Initialize(
                itemSheet,
                new Rectangle(96, 0, 96, 96),
                soundPickUpKeyItem
                );
            TitleScreen.Initialize(titleSheet);
            MainMenu.Initialize(menuSheet,goalSheet,townSheet,spriteFont);
            GameOver.Initialize(gameOverSheet, spriteFont);
            Credits.Initialize(creditsSheet);
            BossScreen.Initialize(bossScreenSheet,spriteFont);
            BossQuestScreen.Initialize(bossquestsheet);
            MainMenu.menuBlipInstance = soundMenuBlip.CreateInstance();
            MainMenu.menuSelectInstance = soundMenuSelect.CreateInstance();
            QuestGenerator.menuSelectInstance = soundMenuSelect.CreateInstance();
            IntroScreen.Initialize(introsheet);
            ControlScreen.Initialize(controlsheet);
            EnemyDetailScreen.Initialize(enemysheet);
           
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GameManager.Exit)
            {
                this.Exit();
            }
            // TODO: Add your update logic here
            //GAME STATE SYSTEM IMPLEMENTED BY CHRIS
            if (GameManager.TitleScreenShown)
            {
                TitleScreen.Update(gameTime);
            }
            else if (GameManager.MenuScreenShown)
            {
                MainMenu.Update(gameTime);
            }
            else if (GameManager.PauseScreenShown)
            {
                PauseMenu.Update(gameTime);
            }
            else if (GameManager.QuestScreenShown)
            {
                QuestGenerator.Update(gameTime);
            }
            else if (GameManager.InGame)
            {
                Player.Update(gameTime);
                WeaponManager.Update(gameTime);
                EffectsManager.Update(gameTime);
                EnemyManager.Update(gameTime);
                BossManager.Update(gameTime);
                BarrelManager.Update(gameTime);
                GoalManager.Update(gameTime);
                base.Update(gameTime);
            }
            else if (GameManager.GameOverShown)
            {
                GameOver.Update(gameTime);
            }
            else if (GameManager.CreditsShown)
            {
                Credits.Update(gameTime);
            }
            else if (GameManager.BossScreenShown)
            {
                BossScreen.Update(gameTime);
            }
            else if (GameManager.InTutorial)
            {
                Player.Update(gameTime);
                WeaponManager.Update(gameTime);
                EffectsManager.Update(gameTime);
                EnemyManager.Update(gameTime);
                BossManager.Update(gameTime);
                BarrelManager.Update(gameTime);
                GoalManager.Update(gameTime);
                base.Update(gameTime);
            }
            else if (GameManager.DifficultyShown)
            {
                Difficulty.Update(gameTime);
            }
            else if (GameManager.IntroScreenShown)
            {
                IntroScreen.Update(gameTime);
            }
            else if (GameManager.ControlScreenShown)
            {
                ControlScreen.Update(gameTime);
            }
            else if (GameManager.BossQuestScreenShown)
            {
                BossQuestScreen.Update(gameTime);
            }
            else if (GameManager.EnemyDetailScreenShown)
            {
                EnemyDetailScreen.Update(gameTime);
            }
            
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            //GAME STATE SYSTEM IMPLEMENTED BY CHRIS
            spriteBatch.Begin();
            if (GameManager.TitleScreenShown)
            {
                TitleScreen.Draw(spriteBatch);
            }
            else if (GameManager.MenuScreenShown)
            {
                MainMenu.Draw(spriteBatch);
            }
            else if (GameManager.PauseScreenShown)
            {
                PauseMenu.Draw(spriteBatch);
            }
            else if (GameManager.InGame)
            {
                TileMap.Draw(spriteBatch);
                GoalManager.Draw(spriteBatch);
                WeaponManager.Draw(spriteBatch);
                EnemyManager.Draw(spriteBatch);
                BarrelManager.Draw(spriteBatch);
                BossManager.Draw(spriteBatch);
                Player.Draw(spriteBatch);
                EffectsManager.Draw(spriteBatch);
                HUD.Draw(spriteBatch);
            }
            else if (GameManager.QuestScreenShown)
            {
                QuestGenerator.Draw(spriteBatch);
            }
            else if (GameManager.GameOverShown)
            {
                GameOver.Draw(spriteBatch);
            }
            else if (GameManager.CreditsShown)
            {
                Credits.Draw(spriteBatch);
            }
            else if (GameManager.BossScreenShown)
            {
                BossScreen.Draw(spriteBatch);
            }
            else if (GameManager.InTutorial)
            {
                TileMap.Draw(spriteBatch);
                GoalManager.Draw(spriteBatch);
                WeaponManager.Draw(spriteBatch);
                EnemyManager.Draw(spriteBatch);
                BarrelManager.Draw(spriteBatch);
                
                BossManager.Draw(spriteBatch);
                Player.Draw(spriteBatch);
                EffectsManager.Draw(spriteBatch);
                HUD.Draw(spriteBatch);
                Player.GodMode = true;
            }
            else if (GameManager.DifficultyShown)
            {
                Difficulty.Draw(spriteBatch);
            }
            else if (GameManager.IntroScreenShown)
            {
                IntroScreen.Draw(spriteBatch);
            }
            else if (GameManager.ControlScreenShown)
            {
                ControlScreen.Draw(spriteBatch);
            }
            else if (GameManager.BossQuestScreenShown)
            {
                BossQuestScreen.Draw(spriteBatch);
            }
            else if (GameManager.EnemyDetailScreenShown)
            {
                EnemyDetailScreen.Draw(spriteBatch);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
