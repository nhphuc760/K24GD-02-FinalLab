using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeFly : Istate
{
    readonly Bee bee;
    readonly Animator animator;
    Vector2 target;
    public BeeFly(Bee bee)
    {
        this.bee = bee;
        animator = bee.animator;
    }
    public void Enter()
    {
        target = bee.GetNextTargetPos();
        animator.CrossFade(Bee.BeeState.Fly.ToString(), 0.1f);
        bee.rb.velocity = (target - (Vector2)bee.transform.position).normalized * bee.GetEnemyObjectSO.speed_walk;
    }

    public void Execute()
    {
        if(Vector2.Distance(bee.transform.position, target) < 0.1f)
        {
            bee.stateMachine.ChangeState(bee.beeIdle);
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
