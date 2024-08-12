using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.EnemyComponents
{
    internal class EnemyPool 
    {
        private List<Enemy> _enemies;
        
        public EnemyPool(Enemy melee, Enemy range)
        {
            AddEnemy(melee);
            AddEnemy(range);    
        }


        public Enemy GetEnemy(int idOfEnemy)
        {
            foreach (Enemy enemy in _enemies)
            {
                if (enemy.gameObject.activeSelf == false && enemy.IdOfType == idOfEnemy)
                {
                    enemy.gameObject.SetActive(true);

                    return enemy;
                }
            }

            return null;
        }
       
        private void AddEnemy(Enemy enemy)
        {
            Enemy currentEnemy = GameObject.Instantiate(enemy);
            currentEnemy.gameObject.SetActive(false);
            _enemies.Add(currentEnemy);
        }  
    }
}