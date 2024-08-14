using UnityEngine;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Weapons;

namespace Assets.Scripts.PlayerComponents
{
    internal class Attacker : MonoBehaviour, IAttackable
    {
        [SerializeField] private Weapon _Sword;
        [SerializeField] private Weapon _Bow;

        private IDamageable _enemy;
        private float _strengthOfAttack;

        public Weapon CurrentWeapon { get; private set; }

        public float SpeedOfAttack { get; private set; }

        public int _currentTypeOfAttack { get; private set; }

        public int AttackType => _currentTypeOfAttack;

        private void Awake()
        {
            SetStartWeapon();
        }

        public void SetEnemy(IDamageable enemy)
        {
            _enemy = enemy;
        }

        public void Init(float strengthOfAttack)
        {
            _strengthOfAttack = strengthOfAttack;
        }

        public void Attack() 
        {
            if (CurrentWeapon != null && _enemy != null)
            {
                CurrentWeapon.Attack(_strengthOfAttack, _enemy);
            }
        }

        public void ChangeWeapon()
        {
            if(CurrentWeapon == _Sword)
            {
                _Sword.gameObject.SetActive(false);
                _Bow.gameObject.SetActive(true);
                CurrentWeapon = _Bow;
                SpeedOfAttack = CurrentWeapon.SpeedOfAttack;
                _currentTypeOfAttack = CurrentWeapon.Id;
               
                return;
            }

            if (CurrentWeapon == _Bow)
            {
                _Bow.gameObject.SetActive(false);
                _Sword.gameObject.SetActive(true);
                CurrentWeapon = _Sword;
                SpeedOfAttack = CurrentWeapon.SpeedOfAttack;
                _currentTypeOfAttack = CurrentWeapon.Id;
            }
        }

        private void SetStartWeapon()
        {
            _Sword.gameObject.SetActive(true);
            CurrentWeapon = _Sword;
            _Bow.gameObject.SetActive(false);
            SpeedOfAttack = CurrentWeapon.SpeedOfAttack;
            _currentTypeOfAttack = CurrentWeapon.Id;
        }     
    }
}