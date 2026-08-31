using UnityEngine;

public class Player_BasicAttackState : PlayerState
{
    private float attackVelocityTimer;
    private float lastTimeAttacked;

    private const int FirstComboIndex = 1; // We start combo index with number 1, this parameter is used in the animator.
    private int comboIndex = 1;
    private int comboLimit = 3;
    private int attackDirection;

    private bool comboAttackQueued;

    public Player_BasicAttackState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {

        if (comboLimit != player.attackVelocity.Length)
        {
            Debug.LogWarning("I've adjusted combo limit according to the amount in the attackVelocity array.");
            comboLimit = player.attackVelocity.Length;
        }

    }


    public override void Enter()
    {
        base.Enter();
        ResetComboIndexIfNeeded();
        comboAttackQueued = false;

        // Define attack direction according to input, if no input is given, use the direction the player is facing.
        attackDirection = player.moveInput.x != 0 ? (int)(player.moveInput.x) : player.facingDirection;

        anim.SetInteger("basicAttackIndex", comboIndex);
        ApplyAttackVelocity();
    }


    public override void Update()
    {
        base.Update();
        HandleAttackVelocity();

        if (input.Player.Attack.WasPressedThisFrame())
        {
            QueueNextAttack();
        }

        if (triggerCalled)
        {
            HandleStateExit();
        }
    }

    public override void Exit()
    {
        base.Exit();
        comboIndex++;
        lastTimeAttacked = Time.time; // remember time when we attacked
    }

    private void HandleStateExit()
    {
        if (comboAttackQueued)
        {
            anim.SetBool(animBoolName, false);
            player.EnterAttackStateWithDelay();
        }
        else
        {
            stateMachine.ChangeState(player.idleState);
        }
    }

    private void QueueNextAttack()
    {
        if (comboIndex < comboLimit)
        {
            comboAttackQueued = true;
        }
    }

    private void HandleAttackVelocity()
    {
        attackVelocityTimer -= Time.deltaTime;

        if (attackVelocityTimer < 0)
        {
            player.SetVelocity(0, rb.linearVelocity.y);
        }
    }

    private void ApplyAttackVelocity()
    {
        Vector2 attackVelocity = player.attackVelocity[comboIndex - 1];

        attackVelocityTimer = player.attackVelocityDuration;
        player.SetVelocity(attackVelocity.x * attackDirection, attackVelocity.y);
    }

    private void ResetComboIndexIfNeeded()
    {
        if (Time.time > lastTimeAttacked + player.comboResetTime)
        {
            comboIndex = FirstComboIndex;
        }

        if (comboIndex > comboLimit)
        {
            comboIndex = FirstComboIndex;
        }
    }
}
