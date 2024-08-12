using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.EnemyComponents
{
    internal class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private Enemy _meleePrafab;
        [SerializeField] private Enemy _rangePrafab;
        [SerializeField] private Transform _spotOfSpawn;

        private int _minBorderOfrandom = 0;
        private int _maxBorderOfrandom = 100;
        private int currentChanceOfSpawn;

        private EnemyPool _pool;

        private void Start()
        {
            _pool = new EnemyPool(_meleePrafab, _rangePrafab);  
        }

        public Enemy CurrentEnemy;

        public void Spawn()
        {
            currentChanceOfSpawn = Random.Range(_minBorderOfrandom, _maxBorderOfrandom);

            if(currentChanceOfSpawn >= _meleePrafab.MinBorderOfChanceOfCreate && currentChanceOfSpawn !> _meleePrafab.MaxBorderOfChanceOfCreate)
            {
                CurrentEnemy = _pool.GetEnemy(EnemiesHash.MeleeEnemyId);
                CurrentEnemy.transform.position = _spotOfSpawn.position;
            }

            if (currentChanceOfSpawn >= _rangePrafab.MinBorderOfChanceOfCreate && currentChanceOfSpawn! > _rangePrafab.MaxBorderOfChanceOfCreate)
            {
                CurrentEnemy =  _pool.GetEnemy(EnemiesHash.RangeEnemyId);
                CurrentEnemy.transform.position = _spotOfSpawn.position;
            }
        }
    }
}