using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpStart : Istate
{
    readonly Player player;
     readonly Animator animator;
    float duration;
    public JumpStart(Player player)
    {
        this.player = player;
        animator= player.animator;
    }
    public void Enter()
    {
        duration = 0.1f;
       
        animator.CrossFade(Player.PlayerState.PlayerJumpStart.ToString(), 0.1f);
    }

    public void Execute()
    {
       float moveInput = GameInput.Ins.GetMovementVectorNormalized();
      if(moveInput != 0)
        {
            player.transform.position += new Vector3(moveInput * Time.deltaTime * player.Speed, 0f, 0f);
            player.MoveDirection = moveInput < 0 ? -1 : 1;
        }
        duration -= Time.deltaTime;
        if(duration <= 0 && player.IsOnGround())
        {
            player.stateMachine.ChangeState(player.jumpEnd);
        }
    }

    public void Exit()
    {
        
    }

    
}
