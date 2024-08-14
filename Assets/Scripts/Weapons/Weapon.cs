using UnityEngine;
using Assets.Scripts.Interfaces;

namespace Assets.Scripts.Weapons
{
    internal abstract class Weapon : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private float _speedOfAttack;
        [SerializeField] private int _id;

        public float SpeedOfAttack => _speedOfAttack;

        public int Id => _id;

        public virtual void Attack(float damage, IDamageable enemy) 
        {
            if(enemy.IsEnemy)
            {
                enemy.TakeDamage(damage);
            }  
        }
    }
}