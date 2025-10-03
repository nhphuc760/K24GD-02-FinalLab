using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeHit : Istate
{
    readonly Bee bee;
    readonly Animator animator;
    AnimatorStateInfo currentBaseState;
    public BeeHit(Bee bee)
    {
        this.bee = bee;
        animator = bee.animator;
    }
    public void Enter()
    {
        bee.rb.velocity = Vector2.zero;
        animator.CrossFade(Bee.BeeState.Hit.ToString(), 0.1f);
    }

    public void Execute()
    {
        currentBaseState = animator.GetCurrentAnimatorStateInfo(0);
        if (currentBaseState.normalizedTime >= 1f && currentBaseState.IsName(Bee.BeeState.Hit.ToString()))
        {
            if (bee.CurrentHealth <= 0)
            {
                bee.Die();
                return;
            }
            bee.stateMachine.ChangeState(bee.stateMachine.oldState);
        }
    }

    public void Exit()
    {

    }
}
