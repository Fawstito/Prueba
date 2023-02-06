using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    public string name;
    protected StateMachine stateMachine;
    protected GameObject gameObject;

    public State(string name, StateMachine stateMachine, GameObject gameObject)
    {
        this.name = name;
        this.stateMachine = stateMachine;
        this.gameObject = gameObject;
    }
    public abstract void Enter();
    public abstract void UpdateLogic();
    public abstract void Exit();
}
