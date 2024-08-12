using Assets.Scripts.FiniteStateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMExample : MonoBehaviour
{
    private FiniteStateMachine _fsm;
    private float _attackDelay = 5;
    private float _preparingDelay = 3;

    private void Start()
    {
        _fsm = new FiniteStateMachine();

        _fsm.AddState(new FSMIdle(_fsm));
        _fsm.AddState(new FSMPreparing(_fsm, _attackDelay));
        _fsm.AddState(new FSMAttack(_fsm, _preparingDelay));

        _fsm.SetState<FSMIdle>();
    }

    private void Update()
    {
        _fsm.Update();
    }
}
