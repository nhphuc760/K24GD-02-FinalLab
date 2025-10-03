using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeIdle : Istate
{
    readonly Bee bee;
    readonly Animator animator;
    float duration;
    public BeeIdle(Bee bee)
    {
        this.bee = bee;
        animator = bee.animator;
    }
    public void Enter()
    {
        duration = Random.Range(1.5f, bee.timeChangeState);
        bee.rb.velocity = Vector2.zero;
        animator.CrossFade(Bee.BeeState.Fly.ToString(), 0.1f);
    }

    public void Execute()
    {
        duration -= Time.deltaTime;
        if (duration <= 0)
        {
          bee.stateMachine.ChangeState(bee.beeFly);
        }
        if (bee.InCamera)
        {
            if (bee.CheckPlayerInRange() != null)
            {
                bee.stateMachine.ChangeState(bee.beeAttack);
            }
        }
    }

    public void Exit()
    {
       
    }
}
