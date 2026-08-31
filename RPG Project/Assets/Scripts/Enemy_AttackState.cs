using UnityEngine;

public class Enemy_AttackState : EnemyState
{
    public Enemy_AttackState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }
    public override void Update()
    {
        base.Update();

        if (triggerCalled)
        {
            if (enemy.battleState.ShouldRetreat())
            {
                stateMachine.ChangeState(enemy.battleState); 
            }
            else if (enemy.battleState.WithinAttackRange())
            {
                stateMachine.ChangeState(enemy.attackState);
            }
            else
            {
                stateMachine.ChangeState(enemy.battleState);
            }
        }
    }
}
