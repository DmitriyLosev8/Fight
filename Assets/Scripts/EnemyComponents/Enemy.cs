using System;
using UnityEngine;
using Assets.Scripts.FiniteStateMachineComponents;
using Assets.Scripts.Interfaces;


namespace Assets.Scripts.EnemyComponents
{
    [RequireComponent(typeof(Animator))]
    internal class Enemy : MonoBehaviour, IDamageable, IAttackable
    {
        [SerializeField] private EnemyData _enemyData;

        private FiniteStateMachine _fsm;
        private Animator _animator;
        private float _health;
        private float _strengthOfAttack;
        private float _speedOfAttack;
        private bool _isEnemy = true;
        private IDamageable _player;  
        
        public event Action<float> HealthChanged;
        public event Action Died;
       
        public int IdOfType { get; private set; }
       
        public float MinBorderOfChanceOfCreate { get; private set; }
       
        public float MaxBorderOfChanceOfCreate { get; private set; }
       
        public float DelayOfAttack { get; private set; }

        public float Health => _health;

        public bool IsEnemy => _isEnemy;

        public int AttackType => _enemyData.IdOfType;

        private void Awake()
        {              
            _animator = GetComponent<Animator>();       
        }

        private void Update()
        {
            _fsm.Update();
        }

        private void OnEnable()
        {
            Init();
            HealthChanged?.Invoke(_health);
        }
     
        public void TakePlayer(IDamageable player)
        {
            _player = player;
        }

        public void Init()
        {
            IdOfType = _enemyData.IdOfType;
            _health = _enemyData.Health;
            _strengthOfAttack = _enemyData.StrengthOfAttack;
            MinBorderOfChanceOfCreate = _enemyData.MinBorderOfChanceOfCreate;
            MaxBorderOfChanceOfCreate = _enemyData.MaxBorderOfChanceOfCreate;
            DelayOfAttack = _enemyData.DelayOfAttack;
            _speedOfAttack = _enemyData.SpeedOfAttack;
        }

        public void ExitFight()
        {
            gameObject.SetActive(false);
            _fsm.CurrentState.Exit();
            _fsm = null;
        }

        public void TakeDamage(float damage)
        { 
            if(_health > 0)
            {
                _health -= damage;
                HealthChanged?.Invoke(_health);
            }
             
            if (_health <= 0)
            {
                Died?.Invoke();
                ExitFight();
            }
        }

        public void Attack()
        {      
            if(_player != null)
                _player.TakeDamage(_strengthOfAttack);
        }

        public void InitFsm(CharacterStateSlider enemyPrepareSlider, CharacterStateSlider enemyAttackSlider) 
        {
            _fsm = new FiniteStateMachine();
            _fsm.SetTypeOfCharacter(_isEnemy);
            _fsm.AddState(new FSMPreparing(_fsm, DelayOfAttack, _animator, enemyPrepareSlider));
            _fsm.AddState(new FSMAttack(_fsm, _speedOfAttack, _animator, this, enemyAttackSlider));
            _fsm.SetState<FSMPreparing>(); 
        }
    }
}