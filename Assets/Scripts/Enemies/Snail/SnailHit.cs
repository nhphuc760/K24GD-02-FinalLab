using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailHit : Istate
{
    readonly Snail snail;
    readonly Animator animator;
    AnimatorStateInfo currentBaseState;
    public SnailHit(Snail snail)
    {
        this.snail = snail;
        animator= snail.animator;
    }
    public void Enter()
    {

        animator.CrossFade(Snail.SnailState.Hit.ToString(), 0.1f);
    }

    public void Execute()
    {
        currentBaseState = animator.GetCurrentAnimatorStateInfo(0);
        if(currentBaseState.IsName(Snail.SnailState.Hit.ToString()) && currentBaseState.normalizedTime >= 1f)
        {
            
            if(snail.CurrentHealth <= 0)
            {
                snail.stateMachine.ChangeState(snail.snailDead);
                return;
            }

            if (Random.Range(0f, 1f) <= 0.2f)
                snail.stateMachine.ChangeState(snail.snailHide);
            else
                snail.stateMachine.ChangeState(snail.snailWalk);

        }
        
    }

    public void Exit()
    {
       
    }
  
   
}
