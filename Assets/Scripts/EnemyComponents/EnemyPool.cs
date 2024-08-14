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

        public Enemy GetEnemy(int ñhanceOfSpawn)
        {
            if (ñhanceOfSpawn >= _melee.MinBorderOfChanceOfCreate && ñhanceOfSpawn < _melee.MaxBorderOfChanceOfCreate)
            {
                if (_melee.gameObject.activeSelf == false)
                {
                    _melee.gameObject.SetActive(true);
                    
                    return _melee;
                }    
            }

            if (ñhanceOfSpawn >= _range.MinBorderOfChanceOfCreate && ñhanceOfSpawn! < _range.MaxBorderOfChanceOfCreate)
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