using System;
using System.Collections;
using UnityEngine;
using Assets.Scripts.EnemyComponents;
using Assets.Scripts.FiniteStateMachineComponents;
using Assets.Scripts.Interfaces;

namespace Assets.Scripts.PlayerComponents
{
    [RequireComponent(typeof(Animator))]
    internal class Player : MonoBehaviour
    {
        [SerializeField] private PlayerData _data;
        [SerializeField] private Attacker _attacker;
        
        private IDamageable _enemy;       
        private FiniteStateMachine _fsm;
        private float _preAttackDelay;   
        private float _secondsToChangeWeapon = 2;
        private WaitForSeconds _timeToChangeWeapon;
        private float _secondsToSpawn = 4;
        private WaitForSeconds _timeToSpawn;
        private GlobalUI _globalUi;
        private Animator _animator;

        private bool _isAlive = true;
        private bool _isChangingWeapon;
        private bool _isEnemy = false;
        private bool _isSpawning;
        private bool _needToExitFight;

        public event Action<PlayersIndicators> ReadyToSpawn;

        public bool IsNotInFight { get; private set; }

        public bool IsInFight { get; private set; }

        public PlayersIndicators PlayersIndicators { get; private set; }

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnDisable()
        {
            PlayersIndicators.Lost -= ExitFight;
            PlayersIndicators.ValueChanged -= _globalUi.ShowPlayerHealth;
            _globalUi.StartButtonClicked -= StartFight;
            _globalUi.CancelButtonClicked -= ExitFight;
            _globalUi.ChangeWeaponButtonClicked -= ChangeWeapon;
            _globalUi.TakeFullHPButtonClicked -= PlayersIndicators.FullHeal; 
        }

        private void Start()
        { 
            InitFsm();

            _timeToChangeWeapon = new WaitForSeconds(_secondsToChangeWeapon);
            _timeToSpawn = new WaitForSeconds(_secondsToSpawn);

            StartCoroutine(UpdateState());
        }

        public void TakeGUI(GlobalUI gui)
        {
            _globalUi = gui;
            _globalUi.StartButtonClicked += StartFight;
            _globalUi.CancelButtonClicked += ExitFight;
            _globalUi.ChangeWeaponButtonClicked += ChangeWeapon;
           
            if (_data != null)
            {
                PlayersIndicators = new PlayersIndicators(_data.Health, _data.Armor);
                _globalUi.TakeFullHPButtonClicked += PlayersIndicators.FullHeal;
                PlayersIndicators.Lost += ExitFight;
                _attacker.Init(_data.StrengthOfAttack);
                _preAttackDelay = _data.PreAttackDelay;
                _globalUi.SetMaxPlayerHealthSlider(PlayersIndicators.Health);
                PlayersIndicators.ValueChanged += _globalUi.ShowPlayerHealth;
                _globalUi.SetPlayerArmorValueSlider(PlayersIndicators.Armor);
            }
        }

        public void TakeEnemy(Enemy enemy)
        {
            _enemy = enemy;
            _attacker.SetEnemy(_enemy);
        }

        public void OnSpawning()
        {
            _attacker.SetEnemy(null);
            _fsm.CurrentState.Exit();
            _fsm.SetState<FSMIdle>();
            _isSpawning = true;
        }

        public void ExitFight()
        {
            _needToExitFight = true;
            _isSpawning = false;
            _globalUi.TurnCancelButton(_isSpawning);
            _globalUi.TurnSpawnDelayImage(_isSpawning);
            _fsm.CurrentState.Exit();
            _fsm.SetState<FSMIdle>();
            _globalUi.EnableStarBatton();
        }

        private void ChangeWeapon()
        {
            _isChangingWeapon = true;
        }

        private IEnumerator UpdateState()
        {
            while (_isAlive)
            {      
                if (_isSpawning == true && _fsm.CurrentState is FSMIdle)
                {
                    _globalUi.TurnSpawnDelayImage(_isSpawning);

                    yield return _timeToSpawn;  

                    if (!_needToExitFight)
                    {
                        ReadyToSpawn?.Invoke(PlayersIndicators);
                        _fsm.SetState<FSMPreparing>();
                        _isSpawning = false;
                        _globalUi.TurnSpawnDelayImage(_isSpawning);
                    }
                }

                if (_isChangingWeapon && _fsm.CurrentState is FSMPreparing || _isChangingWeapon && _fsm.CurrentState is FSMIdle) 
                {
                    _globalUi.TurnChangeWeaponDelayImage(_isChangingWeapon);
                   
                    yield return _timeToChangeWeapon;  
                    
                    _attacker.ChangeWeapon();
                    _isChangingWeapon = false;
                    _globalUi.TurnChangeWeaponDelayImage(_isChangingWeapon);
                }

                _fsm.Update();
                yield return null;

                ChangeAvailableToTakeFullHP();
                ChangeAvailableToExitFight();
            }
        }

        private void StartFight()
        {
            if (_needToExitFight)
                _needToExitFight = false;

            OnSpawning();
        }

        private void ChangeAvailableToTakeFullHP()
        {
            if(_fsm.CurrentState is FSMIdle && PlayersIndicators.Health < _data.Health && !_isSpawning)
            {
                IsNotInFight = true;
                _globalUi.TurnTakeFullHPButton(IsNotInFight);
            }
            else
            {
                IsNotInFight = false;
                _globalUi.TurnTakeFullHPButton(IsNotInFight);
            }
        }

        private void ChangeAvailableToExitFight()
        {
            if (_fsm.CurrentState is FSMPreparing || _fsm.CurrentState is FSMAttack || _fsm.CurrentState is FSMIdle && _isSpawning)
            {
                IsInFight = true;
                _globalUi.TurnCancelButton(IsInFight);
            }
            else
            {
                IsInFight = false;
                _globalUi.TurnCancelButton(IsInFight);
            }
        }

        private void InitFsm()
        {
            _fsm = new FiniteStateMachine();
            _fsm.SetTypeOfCharacter(_isEnemy);
            _fsm.AddState(new FSMIdle(_fsm, _animator));
            _fsm.AddState(new FSMPreparing(_fsm, _preAttackDelay, _animator, _globalUi.PlayerPrepareSlider));
            _fsm.AddState(new FSMAttack(_fsm, _attacker.CurrentWeapon.SpeedOfAttack, _animator, _attacker, _globalUi.PlayerAttackSlider));
            _fsm.SetState<FSMIdle>();
        }
    }
}