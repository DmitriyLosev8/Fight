using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.FiniteStateMachine
{
    internal class FSMAttack : FSMState
    {
        private FiniteStateMachine _fsm;
        private float _delayToPreparing;
        private float _currentDelayToPreparing;


        public FSMAttack(FiniteStateMachine fsm, float delay)
        {
            _fsm = fsm;
            _delayToPreparing = delay;
            _currentDelayToPreparing = _delayToPreparing;
        }

        public override void Enter()
        {
            Debug.Log("Началась атака");
        }


        public override void Exit()
        {
            Debug.Log("Атака закончилась");
        }


        public override void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                _fsm.SetState<FSMIdle>();
            }

            if (_currentDelayToPreparing <= 0)
            {
                _fsm.SetState<FSMPreparing>();

                _currentDelayToPreparing = _delayToPreparing;
            }

            _currentDelayToPreparing -= Time.deltaTime;
        }
    }
}