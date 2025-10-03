using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeAttack : Istate
{
    readonly Bee bee;
    readonly Animator animator;
    AnimatorStateInfo animStateInfo;
    public BeeAttack(Bee bee)
    {
        this.bee = bee;
        animator = bee.animator;
    }
    public void Enter()
    {
        bee.IsAttacking = true;
        animator.CrossFade(Bee.BeeState.Fly.ToString(), .1f);
    }

    public void Execute()
    {
        animStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        Collider2D player = bee.CheckPlayerInRange();
        if (player != null)
        {
            bee.rb.velocity = (Vector2)(player.transform.position - bee.transform.position).normalized * bee.GetEnemyObjectSO.speed_run;
           
        }
        else
        {
            bee.stateMachine.ChangeState(bee.beeFly);

        }
        if (animStateInfo.IsName(Bee.BeeState.Attack.ToString()))
        {
            bee.attackingArea.SetActive(true);
            if(animStateInfo.normalizedTime >= 1f)
            {
                bee.attackingArea.SetActive(false);
                animator.CrossFade(Bee.BeeState.Fly.ToString(), 0.1f);
                //bee.stateMachine.ChangeState(bee.beeAttack);
            }
        }
        
    }

    public void Exit()
    {
       bee.IsAttacking = false;
    }
}
