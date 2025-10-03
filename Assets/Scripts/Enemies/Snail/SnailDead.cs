using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailDead : Istate
{
    readonly Snail snail;
    readonly Animator animator;
    AnimatorStateInfo currentBaseState;
    Collider2D collider;
    public SnailDead(Snail snail)
    {
        this.snail = snail;
        animator= snail.animator;
    }
    public void Enter()
    {
        collider = snail.GetComponent<Collider2D>();
        snail.rb.velocity = Vector2.zero;
        animator.CrossFade(Snail.SnailState.Dead.ToString(), 0.1f);
        
    }

    public void Execute()
    {
        currentBaseState = animator.GetCurrentAnimatorStateInfo(0);
        if (currentBaseState.IsName(Snail.SnailState.Dead.ToString()) && currentBaseState.normalizedTime >= 1f)
       {
            
            snail.DestroySelf();
        
        }
    }

    public void Exit()
    {
        
    }
  
}
