using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Run : Istate
{
    readonly Player player;
    readonly Animator animator;
    public Run(Player player)
    {
        this.player = player;
        animator= player.animator;
    }

    public void Enter()
    {
       animator.CrossFade(Player.PlayerState.PlayerRun.ToString(), 0.1f);
    }

    public void Execute()
    {
        float moveInput = GameInput.Ins.GetMovementVectorNormalized();
        if (moveInput != 0f)
        {
            
            player.transform.position += new Vector3(moveInput * Time.deltaTime * player.Speed, 0, 0);
           player.MoveDirection = moveInput < 0 ? -1 : 1;

        }
         else 
        {
            player.stateMachine.ChangeState(player.idle);
        }
    }

    public void Exit()
    {
    
  }
}
