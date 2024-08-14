using UnityEngine;

namespace Assets.Scripts.FiniteStateMachineComponents
{
    internal class FSMIdle : FSMState
    {
        private FiniteStateMachine _fsm;
        private Animator _animator;
      
        public FSMIdle(FiniteStateMachine fsm, Animator animator) 
        {
            _fsm = fsm;
            _animator = animator;
        }

        public override void Enter()
        {
            _animator.SetBool(AnimatorHash.IsIdle, true);
            //_animator.Play("Idle");
        }


        public override void Exit()
        {
        }
    }
}