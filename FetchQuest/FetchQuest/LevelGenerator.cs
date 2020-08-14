using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace FetchQuest
{
    //CLASS CREATED BY CHRIS
    class LevelGenerator
    {
        private static int minDistanceFromPlayer = 250;
        public static int enemymax = 0;
        public const int MAXENEMYMAX = 150;
        public static int bossSpawn = 3;
        public static void Generate()
        {
            GoalManager.Objectives.Clear();
            EnemyManager.Enemies.Clear();
            BarrelManager.Barrels.Clear();
            BossManager.Bosses.Clear();
            WeaponManager.EnemyShots.Clear();
            WeaponManager.Shots.Clear();
            BarrelManager.Hearts.Clear();
            Random rand = new Random();
            int numberOfEnemies = 0;
            int numberOfBarrels = 0;
            int numberOfObjectives = 0;
            int numberOfBosses = 0;
            int X = 0;
            int Y = 0;
            if (GameManager.CurrentLevel % bossSpawn != 0)//Places enemies randomly if not a boss level
            {
                while (numberOfEnemies < enemymax)
                {
                    X = rand.Next(2, TileMap.MapWidth - 2);
                    Y = rand.Next(0, TileMap.MapHeight - 2);
                    Vector2 tileLocation = new Vector2(X, Y);
                    if (TileMap.PlayerSpawnSolitary((int)tileLocation.X, (int)tileLocation.Y))
                    {
                        if (rand.Next(1, 10) == 1)
                        {
                            EnemyManager.AddEnemy(tileLocation);
                            numberOfEnemies++;
                        }
                    }
                }
            }
            while (numberOfBarrels < 10)//Places barrels randomly onto the map
            {
                X = rand.Next(2, TileMap.MapWidth - 2);
                Y = rand.Next(0, TileMap.MapHeight - 2);
                Vector2 tileLocation = new Vector2(X, Y);
                if (TileMap.IsFloorTile(X, Y))
                {
                    if (TileMap.IsInCorner(X, Y))
                    {
                        if (rand.Next(1, 10) == 1)
                        {
                            BarrelManager.AddBarrel(tileLocation);
                            numberOfBarrels++;
                        }
                    }
                }
            }
            if (GameManager.CurrentLevel % bossSpawn != 0)//Places objectives randomly if it is not a boss level
            {

                while (numberOfObjectives < 3)
                {
                    X = rand.Next(2, TileMap.MapWidth - 2);
                    Y = rand.Next(0, TileMap.MapHeight - 2);
                    Vector2 tileLocation = new Vector2(X, Y);
                    if (TileMap.IsFloorTile(X, Y))
                    {
                        if (rand.Next(1, 10) == 1)
                        {
                            if (Vector2.Distance(TileMap.GetSquareCenter(X, Y),
                            Player.CharacterSprite.WorldCenter) < minDistanceFromPlayer)
                            {
                                continue;
                            }
                            else
                            {
                                GoalManager.PlaceObjective(tileLocation);
                                numberOfObjectives++;
                            }
                        }

                    }
                }
            }

            if (GameManager.CurrentLevel % bossSpawn == 0)//Places the boss randomly onto the level if it is a boss level
            {

                while (numberOfBosses < 1)
                {

                    X = rand.Next(2, TileMap.MapWidth - 2);
                    Y = rand.Next(0, TileMap.MapHeight - 2);
                    Vector2 tileLocation = new Vector2(X, Y);
                    if (TileMap.IsFloorTile(X, Y))
                    {
                        if (rand.Next(1, 10) == 1)
                        {
                            if (!(Vector2.Distance(TileMap.GetSquareCenter(X, Y),
                            Player.CharacterSprite.WorldCenter) < minDistanceFromPlayer))
                            {
                                BossManager.AddBoss(tileLocation);
                                numberOfBosses++;
                            }
                        }
                    }
                }

            }
        }
    }
}
