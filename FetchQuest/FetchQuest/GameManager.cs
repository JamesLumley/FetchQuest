using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace FetchQuest
{
    //CLASS CREATED BY: CHRIS
    //EDITED BY: JAMES
    class GameManager
    {
        public static bool TitleScreenShown = false;
        public static bool MenuScreenShown = false;
        public static bool PauseScreenShown = false;
        public static bool InGame = false;
        public static bool Exit = false;
        public static bool QuestScreenShown = false;
        public static bool GameOverShown = false;
        public static bool CreditsShown = false;
        public static bool BossScreenShown = false;
        public static bool InTutorial = false;
        public static bool DifficultyShown = false;
        public static bool BossQuestScreenShown = false;
        public static bool IntroScreenShown = false;
        public static bool ControlScreenShown = false;
        public static bool EnemyDetailScreenShown = false;
        public static int CurrentLevel = 5;
        public static int CurrentPowerups = 0;
        public static int enemyincrease = 0;
        public static Random rand = new Random();
        public static SoundEffectInstance bgSound;
        public static SoundEffectInstance creditSound;
        public static SoundEffectInstance bossSound;

        public static void SetState(int state)//Sets the different game states
        {
            switch (state)
            {
                case 1: TitleScreenShown = true;
                    MenuScreenShown = false;
                    PauseScreenShown = false;
                    InGame = false;
                    QuestScreenShown = false;
                    CreditsShown = false;
                    BossScreenShown = false;
                    InTutorial = false;
                    DifficultyShown = false;
                    IntroScreenShown = false;
                    ControlScreenShown = false;
                    EnemyDetailScreenShown = false;
                    BossQuestScreenShown = false;
                    creditSound.Stop();
                    bossSound.Stop();
                    bgSound.Play();
                    break;
                case 2: MenuScreenShown = true;
                    TitleScreenShown = false;
                    PauseScreenShown = false;
                    InGame = false;
                    QuestScreenShown = false;
                    CreditsShown = false;
                    BossScreenShown = false;
                    InTutorial = false;
                    DifficultyShown = false;
                    IntroScreenShown = false;
                    ControlScreenShown = false;
                    EnemyDetailScreenShown = false;
                    BossQuestScreenShown = false;
                    creditSound.Stop();
                    bossSound.Stop();
                    bgSound.Stop();
                    break;
                case 3: PauseScreenShown = true;
                    MenuScreenShown = false;
                    TitleScreenShown = false;
                    InGame = false;
                    QuestScreenShown = false;
                    CreditsShown = false;
                    BossScreenShown = false;
                    InTutorial = false;
                    DifficultyShown = false;
                    IntroScreenShown = false;
                    ControlScreenShown = false;
                    EnemyDetailScreenShown = false;
                    BossQuestScreenShown = false;
                    creditSound.Stop();
                    bossSound.Stop();
                    bgSound.Stop();
                    break;
                case 4: Player.GodMode = false; 
                    InGame = true;
                    MenuScreenShown = false;
                    PauseScreenShown = false;
                    TitleScreenShown = false;
                    QuestScreenShown = false;
                    CreditsShown = false;
                    InTutorial = false;
                    BossScreenShown = false;
                    DifficultyShown = false;
                    IntroScreenShown = false;
                    ControlScreenShown = false;
                    EnemyDetailScreenShown = false;
                    BossQuestScreenShown = false;
                    bgSound.Volume = 1.0f;
                    creditSound.Stop();
                    if (CurrentLevel % LevelGenerator.bossSpawn == 0)
                    {
                        bossSound.Play();
                        bgSound.Stop();
                    }
                    else
                    {
                        bossSound.Stop();
                        bgSound.Play();
                    }
                    break;
                case 5: Exit = true;
                    InGame = false;
                    MenuScreenShown = false;
                    PauseScreenShown = false;
                    TitleScreenShown = false;
                    QuestScreenShown = false;
                    CreditsShown = false;
                    BossScreenShown = false;
                    InTutorial = false;
                    DifficultyShown = false;
                    IntroScreenShown = false;
                    ControlScreenShown = false;
                    EnemyDetailScreenShown = false;
                    BossQuestScreenShown = false;
                    bossSound.Stop();
                    creditSound.Stop();
                    bgSound.Stop();
                    break;
                case 6: QuestScreenShown = true;
                    Camera.Position = Vector2.Zero;
                    InGame = false;
                    MenuScreenShown = false;
                    PauseScreenShown = false;
                    TitleScreenShown = false;
                    CreditsShown = false;
                    BossScreenShown = false;
                    InTutorial = false;
                    DifficultyShown = false;
                    IntroScreenShown = false;
                    ControlScreenShown = false;
                    EnemyDetailScreenShown = false;
                    BossQuestScreenShown = false;
                    bgSound.Stop();
                    creditSound.Stop();
                    bossSound.Stop();
                    QuestGenerator.Quest = QuestGenerator.questCreator();
                    QuestGenerator.TownFolkSet();
                    break;
                case 7: GameOverShown = true;
                    QuestScreenShown = false;
                    InGame = false;
                    MenuScreenShown = false;
                    PauseScreenShown = false;
                    TitleScreenShown = false;
                    CreditsShown = false;
                    BossScreenShown = false;
                    InTutorial = false;
                    DifficultyShown = false;
                    IntroScreenShown = false;
                    ControlScreenShown = false;
                    EnemyDetailScreenShown = false;
                    BossQuestScreenShown = false;
                    bgSound.Stop();
                    creditSound.Stop();
                    bossSound.Stop();
                    break;
                case 8: CreditsShown = true;
                    GameOverShown = false;
                    QuestScreenShown = false;
                    InGame = false;
                    MenuScreenShown = false;
                    PauseScreenShown = false;
                    TitleScreenShown = false;
                    BossScreenShown = false;
                    InTutorial = false;
                    DifficultyShown = false;
                    IntroScreenShown = false;
                    ControlScreenShown = false;
                    EnemyDetailScreenShown = false;
                    BossQuestScreenShown = false;
                    creditSound.Play();
                    bgSound.Stop();
                    bossSound.Stop();
                    break;
                case 9: BossScreenShown = true;
                    CreditsShown = false;
                    GameOverShown = false;
                    QuestScreenShown = false;
                    InGame = false;
                    MenuScreenShown = false;
                    PauseScreenShown = false;
                    TitleScreenShown = false;
                    InTutorial = false;
                    DifficultyShown = false;
                    IntroScreenShown = false;
                    ControlScreenShown = false;
                    EnemyDetailScreenShown = false;
                    BossQuestScreenShown = false;
                    bgSound.Stop();
                    creditSound.Stop();
                    break;
                    //EDITED BY JAMES{    
                case 10:
                    Player.GodMode = true;
                    InTutorial = true;
                    GameOverShown = false;
                    QuestScreenShown = false;
                    InGame = false;
                    MenuScreenShown = false;
                    PauseScreenShown = false;
                    TitleScreenShown = false;
                    CreditsShown = false;
                    DifficultyShown = false;
                    IntroScreenShown = false;
                    ControlScreenShown = false;
                    EnemyDetailScreenShown = false;
                    BossQuestScreenShown = false;
                    creditSound.Stop();
                    bossSound.Stop();
                    bgSound.Volume = 1.0f;
                    bgSound.Play();//}EDITED BY JAMES
                    HUD.controlsAlphaCounter = 1;
                    break;
                case 11: 
                    IntroScreenShown = false;
                    ControlScreenShown = false;
                    EnemyDetailScreenShown = false;
                    BossQuestScreenShown = false;
                    DifficultyShown = true;
                    InTutorial = false;
                    GameOverShown = false;
                    QuestScreenShown = false;
                    InGame = false;
                    MenuScreenShown = false;
                    PauseScreenShown = false;
                    TitleScreenShown = false;
                    CreditsShown = false;
                    bossSound.Stop();
                    creditSound.Stop();
                    break;
                case 12:
                    IntroScreenShown = true;
                    ControlScreenShown = false;
                    EnemyDetailScreenShown = false;
                    BossQuestScreenShown = false;
                    DifficultyShown = false;
                    InTutorial = false;
                    GameOverShown = false;
                    QuestScreenShown = false;
                    InGame = false;
                    MenuScreenShown = false;
                    PauseScreenShown = false;
                    TitleScreenShown = false;
                    CreditsShown = false;
                    bossSound.Stop();
                    creditSound.Stop();
                    break;
                case 13:
                    IntroScreenShown = false;
                    ControlScreenShown = true;
                    EnemyDetailScreenShown = false;
                    BossQuestScreenShown = false;
                    DifficultyShown = false;
                    InTutorial = false;
                    GameOverShown = false;
                    QuestScreenShown = false;
                    InGame = false;
                    MenuScreenShown = false;
                    PauseScreenShown = false;
                    TitleScreenShown = false;
                    CreditsShown = false;
                    bossSound.Stop();
                    creditSound.Stop();
                    break;
                case 14:
                    IntroScreenShown = false;
                    ControlScreenShown = false;
                    EnemyDetailScreenShown = true;
                    BossQuestScreenShown = false;
                    DifficultyShown = false;
                    InTutorial = false;
                    GameOverShown = false;
                    QuestScreenShown = false;
                    InGame = false;
                    MenuScreenShown = false;
                    PauseScreenShown = false;
                    TitleScreenShown = false;
                    CreditsShown = false;
                    bossSound.Stop();
                    creditSound.Stop();
                    break;
                case 15:
                    IntroScreenShown = false;
                    ControlScreenShown = false;
                    EnemyDetailScreenShown = false;
                    BossQuestScreenShown = true;
                    DifficultyShown = false;
                    InTutorial = false;
                    GameOverShown = false;
                    QuestScreenShown = false;
                    InGame = false;
                    MenuScreenShown = false;
                    PauseScreenShown = false;
                    TitleScreenShown = false;
                    CreditsShown = false;
                    bossSound.Stop();
                    creditSound.Stop();
                    break;
                default: break;
            }
        }
        public static void NewLevel()//Sets the game up for a new level
        {
            if (LevelGenerator.enemymax < LevelGenerator.MAXENEMYMAX)
            {
                LevelGenerator.enemymax = LevelGenerator.enemymax + enemyincrease;
            }
            CurrentLevel++;
            
            
            Player.health = Player.maxHealth;
            if (CurrentLevel % LevelGenerator.bossSpawn != 0)
            {
                GameManager.SetState(6);
            }
            else
            {
                GameManager.SetState(15);
            }
        }



    }
}
