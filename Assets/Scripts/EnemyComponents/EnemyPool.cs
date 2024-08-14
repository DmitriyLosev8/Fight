using UnityEngine;

namespace Assets.Scripts.EnemyComponents
{
    internal class EnemyPool 
    {
        private Enemy _melee;
        private Enemy _range;

        public EnemyPool(Enemy melee, Enemy range)
        {
            CreateMelee(melee);
            CreateRange(range);   
        }

        public Enemy GetEnemy(int �hanceOfSpawn)
        {
            if (�hanceOfSpawn >= _melee.MinBorderOfChanceOfCreate && �hanceOfSpawn < _melee.MaxBorderOfChanceOfCreate)
            {
                if (_melee.gameObject.activeSelf == false)
                {
                    _melee.gameObject.SetActive(true);
                    
                    return _melee;
                }    
            }

            if (�hanceOfSpawn >= _range.MinBorderOfChanceOfCreate && �hanceOfSpawn! < _range.MaxBorderOfChanceOfCreate)
            {
                if (_range.gameObject.activeSelf == false)
                {
                    _range.gameObject.SetActive(true);

                    return _range;
                }  
            }

            return null;
        }

        private void CreateMelee(Enemy melee) 
        {
            _melee = GameObject.Instantiate(melee);
            _melee.gameObject.SetActive(false);
        }

        private void CreateRange(Enemy range)
        {
            _range = GameObject.Instantiate(range);
            _range.gameObject.SetActive(false);
        }
    }
}