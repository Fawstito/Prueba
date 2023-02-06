using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowState : State
{
    public NavMeshAgent agent;
    public AIVision vision;
    BasicEnemyFSM fsm;
    public FollowState(StateMachine stateMachine, GameObject gameObject)
        : base("Follow", stateMachine, gameObject)
    {
        fsm = (BasicEnemyFSM)stateMachine;
    }

    public override void Enter()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        vision = gameObject.GetComponent<AIVision>();
    }

    public override void Exit()
    {

    }

    public override void UpdateLogic()
    {
        Vector3 target = vision.target.transform.position;
        Vector3 origin = gameObject.transform.position;
        float distance = Vector3.Distance(origin, target);
        if (fsm.ammo <= 0)
        {
            fsm.ChangeState(fsm.flee);
        }

        if (vision.target)
        {
            //Si no esta en rango
            if (distance > fsm.range)
            {
                agent.SetDestination(target);
            }
            //Si estamos en rango
            if (distance <= fsm.range)
            {
                agent.SetDestination(gameObject.transform.position);
                if (fsm.time > fsm.timeToShoot)
                {
                    var go = fsm.InstantiateObject(fsm.arrow, fsm.spawnPoint.position, fsm.spawnPoint.transform.rotation);
                    go.AddComponent<Rigidbody>().AddForce(fsm.spawnPoint.forward * fsm.throwForce);
                    fsm.time = 0;
                    fsm.ammo--;
                }
                fsm.time += Time.deltaTime;
            }
            target.y = 0;
            gameObject.transform.LookAt(target, Vector3.up);
        }

    }
}
