using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeState : State
{
    Coroutine reloading;
    BasicEnemyFSM fsm;
    Flee flee;
    AIVision vision;
    bool reloaded = false;
    public FleeState(StateMachine stateMachine, GameObject gameObject)
     : base("Flee", stateMachine, gameObject)
    {
        fsm = (BasicEnemyFSM)stateMachine;
    }

    public override void Enter()
    {
        reloading = null;
        reloaded = false;
        flee = gameObject.GetComponent<Flee>();
        vision = gameObject.GetComponent<AIVision>();
    }

    public override void Exit()
    {
    }

    public override void UpdateLogic()
    {
        if (reloading == null)
        {
            reloading = fsm.StartCoroutineFSM(Reload());
        }
        else
        {
            //Huir
            flee.target = vision.target.transform;
            flee.UpdateFlee();
        }
        if (reloaded)
        {
            fsm.ChangeState(fsm.follow);
        }
    }
    IEnumerator Reload()
    {
        yield return new WaitForSeconds(fsm.timeToReload);
        fsm.ammo = fsm.maxAmmo;
        reloaded = true;
    }
}
