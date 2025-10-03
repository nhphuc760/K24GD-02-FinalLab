using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : Istate
{
    readonly Player player;
    readonly Animator animator;
    AnimatorStateInfo currentBaseState;

    public Attack(Player player)
    {
        this.player = player;
        animator= player.animator;
        
    }
    public void Enter()
    {
        player.Attacking = true;
        player.AttackArea.SetActive(true);
        AudioController.Ins.PlaySound(AudioController.Ins.sword);
        animator.CrossFade(Player.PlayerState.PlayerAttack.ToString(), 0.1f);
        
    }

    public void Execute()
    {
        currentBaseState = animator.GetCurrentAnimatorStateInfo(0);
        float moveInput = GameInput.Ins.GetMovementVectorNormalized();
        if(moveInput != 0)
            player.transform.position += new Vector3(moveInput * Time.deltaTime * player.Speed, 0f, 0f);
        if (currentBaseState.normalizedTime >= 1f && currentBaseState.IsName(Player.PlayerState.PlayerAttack.ToString()))
        {
           
            player.stateMachine.ChangeState(player.stateMachine.oldState);
        }
    }

    public void Exit()
    {
        player.Attacking = false;
        player.AttackArea.SetActive(false);
    }
}
