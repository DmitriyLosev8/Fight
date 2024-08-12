using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.FiniteStateMachine
{
    internal class FSMPreparing : FSMState
    {
        private FiniteStateMachine _fsm;
        private float _delayToAttack;
        private float _currentDelayToAttack;


        public FSMPreparing(FiniteStateMachine fsm, float delay)
        {
            _fsm = fsm;
            _delayToAttack = delay;
            _currentDelayToAttack = _delayToAttack;
        }


        public override void Enter()
        {
            Debug.Log("�������� ����������");
        }


        public override void Exit()
        {
            Debug.Log("���������� �����������");
        }

        public override void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                _fsm.SetState<FSMIdle>();
                Debug.Log("������ ����");
            }

           if(_currentDelayToAttack <= 0)
            {
                _fsm.SetState<FSMAttack>();

                _currentDelayToAttack = _delayToAttack;
            }

            _currentDelayToAttack -= Time.deltaTime;
        }
    }
}