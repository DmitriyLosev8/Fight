using System;
using UnityEngine;
using Assets.Scripts.Interfaces;

namespace Assets.Scripts.PlayerComponents
{
    internal class PlayersIndicators : IDamageable
    {
        private float _health;
        private float _startHealth;
        private float _armor;
        private bool _isEnemy = false;

        public float Health => _health;

        public float Armor => _armor;

        public bool IsEnemy => _isEnemy;

        public event Action<float> ValueChanged;
        public event Action Lost;

        public PlayersIndicators(float health, float armor)
        {
            _health = health;
            _startHealth = _health;
            _armor = armor;
            ValueChanged?.Invoke(_health);
        }

        public void TakeDamage(float damage)
        {
            if (_health > 0)
            {
                if (_armor != 0)
                {
                    _health -= damage - _armor;
                    ValueChanged?.Invoke(_health);
                }
                else
                {
                    _health -= damage;
                    ValueChanged?.Invoke(_health);
                }
            }
            
            if (_health <= 0)
            {
                Lost?.Invoke();
            }
        }

        public void FullHeal()
        {
            if(_health < _startHealth)
            {
                _health = _startHealth;
                ValueChanged?.Invoke(_health);
            }  
        }
    }
}