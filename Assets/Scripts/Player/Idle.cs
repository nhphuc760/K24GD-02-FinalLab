using UnityEngine;

public class Idle : Istate
{
    readonly Player player;
      readonly  Animator animator;
    public Idle(Player player)
    {
        this.player = player;
        this.animator = player.animator;
    }
    public void Enter()
    {
       animator.CrossFade(Player.PlayerState.PlayerIdle.ToString(), 0.1f);
       
    }

    public void Execute()
    {
        float moveInput = GameInput.Ins.GetMovementVectorNormalized();
        if (moveInput != 0)
       {
           player.stateMachine.ChangeState(player.run);
        }


    }

    public void Exit()
    {
       
    }
}
