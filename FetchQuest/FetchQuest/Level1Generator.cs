using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FetchQuest
{
    //CLASS CREATED BY: JAMES
    class Level1Generator
    {
        public static void EnemyGeneration()//Places the player and enemies in set positions for the purpose of the tutorial level
        {
            GoalManager.Objectives.Clear();
            EnemyManager.Enemies.Clear();
            BarrelManager.Barrels.Clear();
            BossManager.Bosses.Clear();
            GameManager.InTutorial = true;

            Player.CharacterSprite.WorldLocation = new Vector2(1800, 4200);

            //ENEMIES
            //bottom row of enemies
            EnemyManager.AddEnemy(new Vector2(10, 35));
            
      
            EnemyManager.AddEnemy(new Vector2(20, 35));
           
            EnemyManager.AddEnemy(new Vector2(30, 35));

            //left middle enemies
            EnemyManager.AddEnemy(new Vector2(10, 30));
            
            
            EnemyManager.AddEnemy(new Vector2(10, 15));

            //enemies in left room
            EnemyManager.AddEnemy(new Vector2(4, 15));
            EnemyManager.AddEnemy(new Vector2(4, 16));
            EnemyManager.AddEnemy(new Vector2(4, 17));
         

            //enemies on top middle
           
            EnemyManager.AddEnemy(new Vector2(22, 12));
   
           
          
            EnemyManager.AddEnemy(new Vector2(29, 12));
           
            EnemyManager.AddEnemy(new Vector2(32, 12));

            //enemies on right middle

            EnemyManager.AddEnemy(new Vector2(31, 18));
            EnemyManager.AddEnemy(new Vector2(32, 18));
 
  
 
            EnemyManager.AddEnemy(new Vector2(22, 27));
            EnemyManager.AddEnemy(new Vector2(22, 25));
            EnemyManager.AddEnemy(new Vector2(22, 21));
            EnemyManager.AddEnemy(new Vector2(22, 19));

            
            BossManager.AddBoss(new Vector2(22, 2));
            
            
           

        }
    }
}
