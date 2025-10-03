using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpEnd : Istate
{
    readonly Player player;
    readonly Animator animator;
    float duration;
    public JumpEnd(Player player)
    {
        this.player = player;
        animator= player.animator;
    }   

    public void Enter()
    {
        duration = 0.2f;
        animator.CrossFade(Player.PlayerState.PlayerJumpEnd.ToString(), 0.1f);
    }
    public void Execute()
    {
        duration -= Time.deltaTime;
        float moveInput = GameInput.Ins.GetMovementVectorNormalized();
        if(duration > 0) return;
        if (moveInput != 0)
        {
            player.stateMachine.ChangeState(player.run);
        }
        
        else
        {
            player.stateMachine.ChangeState(player.idle);
        }
    }

    public void Exit()
    {

    }

    // Start is called before the first frame update
    
}
