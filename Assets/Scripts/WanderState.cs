using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderState : State
{
    public Wander wander;
    public AIVision vision;

    BasicEnemyFSM fsm;
    public WanderState(StateMachine stateMachine, GameObject gameObject)
        : base("Wander", stateMachine, gameObject)
    {
        fsm = (BasicEnemyFSM)stateMachine;
    }

    public override void Enter()
    {
        wander = gameObject.GetComponent<Wander>();
        vision = gameObject.GetComponent<AIVision>();
    }

    public override void Exit()
    {

    }

    public override void UpdateLogic()
    {
        wander.UpdateWander();
        if (vision.UpdateVision())
        {
            stateMachine.ChangeState(fsm.follow);
        }
    }
}
