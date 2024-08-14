using UnityEngine;

namespace Assets.Scripts.FiniteStateMachineComponents
{
    internal class FSMPreparing : FSMState
    {
        private FiniteStateMachine _fsm;
        private float _delayToAttack;
        private float _currentDelayToAttack;
        private Animator _animator;
        private CharacterStateSlider _slider;

        public FSMPreparing(FiniteStateMachine fsm, float delay, Animator animator, CharacterStateSlider slider)
        {
            _fsm = fsm;
            _delayToAttack = delay;
            _currentDelayToAttack = _delayToAttack;
            _animator = animator;
            _slider = slider;
            _slider.SetMaxValue(_delayToAttack);
        }

        public override void Enter()
        {
            _animator.SetBool(AnimatorHash.IsIdle, true);   
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

           if(_currentDelayToAttack <= 0)
            {
                _fsm.SetState<FSMAttack>();
                _currentDelayToAttack = _delayToAttack;
            }

             _currentDelayToAttack -= Time.deltaTime;
            _slider.Show(_currentDelayToAttack);
        }
    }
}