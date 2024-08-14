using UnityEngine;

namespace Assets.Scripts.FiniteStateMachineComponents
{
    internal class FSMAttack : FSMState
    {
        private FiniteStateMachine _fsm;
        private float _speedOfAttack;
        private float _currentSpeedOfAttack;
        private Animator _animator;
        private IAttackable _attacker;
        private CharacterStateSlider _slider;

        public FSMAttack(FiniteStateMachine fsm, float speedOfAttack, Animator animator, IAttackable attaker, CharacterStateSlider slider)
        {
            _fsm = fsm; 
            _animator = animator;
            _attacker = attaker;
            _speedOfAttack = speedOfAttack;
            _currentSpeedOfAttack = _speedOfAttack;
            _slider = slider;
            _slider.SetMaxValue(_speedOfAttack);
        }

        public override void Enter()
        {          
            switch (_attacker.AttackType)
            {
                case AttackTypeHash.Sword:
                    _animator.SetFloat(AnimatorHash.SwordSpeed, _currentSpeedOfAttack);
                    _animator.Play(AnimatorHash.SwordAttack);
                   
                    break;
                case AttackTypeHash.Bow:
                    _animator.SetFloat(AnimatorHash.BowSpeed, _currentSpeedOfAttack);
                    _animator.Play(AnimatorHash.Shoot);
                    
                    break;
                case AttackTypeHash.EnemyMelee:
                    _animator.SetFloat(AnimatorHash.EnemyMeleeSpeed, _currentSpeedOfAttack);
                     _animator.Play(AnimatorHash.MeleeAttack);
                    break;
                case AttackTypeHash.EnemyRange:
                    _animator.SetFloat(AnimatorHash.EnemyRangeSpeed, _currentSpeedOfAttack);
                    _animator.Play(AnimatorHash.RangeAttack);
                   
                    break;
            }

            _slider.StartShow();
        }

        public override void Exit()
        {
            _slider.StopShow();
        }

        public override void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                _fsm.SetState<FSMIdle>();
            }

            if (_currentSpeedOfAttack <= 0)
            {
                _fsm.SetState<FSMPreparing>();
                _currentSpeedOfAttack = _speedOfAttack;
                _attacker.Attack();
            }

            _currentSpeedOfAttack -= Time.deltaTime;
            _slider.Show(_currentSpeedOfAttack);
        }
    }
}