using System;
using UnityEngine;
using Assets.Scripts.Interfaces;

namespace Assets.Scripts.EnemyComponents
{
    internal class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private Enemy _meleePrafab;
        [SerializeField] private Enemy _rangePrafab;
        [SerializeField] private Transform _spotOfSpawn;

        private int _minBorderOfrandom = 0;
        private int _maxBorderOfrandom = 100;
        private int _currentChanceOfSpawn;
        private Enemy _currentEnemy; 
        private GlobalUI _gui;
        private EnemyPool _pool;

        public event Action<Enemy> EnemyCreated;
        public event Action EnemyDied;

        private void Start()
        {
            _pool = new EnemyPool(_meleePrafab, _rangePrafab);  
        }

        private void OnDisable()
        {
            if (_currentEnemy != null)
                _currentEnemy.Died -= OnDied;
        }

        public void TakeGUI(GlobalUI gui)
        {
            _gui = gui;
        }

        public void StopFight()
        {
            if(_currentEnemy!= null && _currentEnemy.gameObject.activeSelf)
            {
                _currentEnemy.ExitFight();
                _gui.StopShowEnemyHealth();
            }        
        }

        public void Spawn(IDamageable player)
        {      
            _currentChanceOfSpawn = UnityEngine.Random.Range(_minBorderOfrandom, _maxBorderOfrandom);  
            _currentEnemy = _pool.GetEnemy(_currentChanceOfSpawn);
            _currentEnemy.transform.position = _spotOfSpawn.position;
            _gui.SetMaxEnemyHealthSlider(_currentEnemy.Health);
            _currentEnemy.HealthChanged += _gui.ShowEnemyHealth;
            _currentEnemy.InitFsm(_gui.EnemyPrepareSlider, _gui.EnemyAttackSlider);  
            _currentEnemy.TakePlayer(player);
            EnemyCreated?.Invoke(_currentEnemy);
            _currentEnemy.Died += OnDied;
        }

        private void OnDied()
        {
            EnemyDied?.Invoke();
            _gui.StopShowEnemyHealth();
        }
    }
}