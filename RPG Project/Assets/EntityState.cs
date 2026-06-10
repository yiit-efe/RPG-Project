using UnityEngine;

public abstract class EntityState
{
   protected Player player;
   protected StateMachine stateMachine;
   protected string stateName;

    public EntityState(Player player, StateMachine stateMachine, string stateName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.stateName = stateName;
    }   

    public virtual void Enter()
    {
        // Every time the state is changed, this will be called.

        Debug.Log("I entered the state: " + stateName);
    }

    public virtual void Update()
    {
        // The logic of the state runs here.

        Debug.Log("I am updating the state: " + stateName);
    }

    public virtual void Exit()
    {
        // This will be called every time the state is changed to a new one.

        Debug.Log("I exited the state: " + stateName);
    }
}
