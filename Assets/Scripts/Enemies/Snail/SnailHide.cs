using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailHide : Istate
{
    readonly Snail snail;
    readonly Animator animator;
    float duration;
     public SnailHide(Snail snail)
    {
        this.snail = snail;
        animator = snail.animator;
    }
    public void Enter()
    {
       
        snail.rb.velocity = Vector2.zero;
        duration = snail.timeChangeState;
       animator.CrossFade(Snail.SnailState.Hide.ToString(), 0.1f);
    }

    public void Execute()
    {
       duration -= Time.deltaTime;
        if(duration <= 0f)
         {
              snail.stateMachine.ChangeState(snail.snailWalk);
        }
    }

    public void Exit()
    {
       
    }
}
