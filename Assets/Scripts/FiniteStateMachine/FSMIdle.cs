using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.FiniteStateMachine
{
    internal class FSMIdle : FSMState
    {
        private FiniteStateMachine _fsm;

        public FSMIdle(FiniteStateMachine fsm) 
        {
            _fsm = fsm;
        }

        public override void Enter()
        {
            Debug.Log("�������� �����������");
        }


        public override void Exit()
        {
            Debug.Log("����������� �����������");
        }


        public override void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _fsm.SetState<FSMPreparing>();
            }    
        }
    }
}