using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailWalk : Istate
{
    readonly Snail snail;
    readonly Animator animator;
    bool isFacingRight;
   
    public SnailWalk(Snail snail)
    {
        this.snail = snail;
        animator= snail.animator;
    }
    public void Enter()
    {
       animator.CrossFade(Snail.SnailState.Walk.ToString(), 0.1f);
        if (Random.Range(0, 2) == 0)
        {
            snail.rb.velocity = new Vector2(snail.GetEnemyObjectSO.speed_walk, 0);
            isFacingRight = true;
        }
        else
        {
            snail.rb.velocity = new Vector2(-snail.GetEnemyObjectSO.speed_walk, 0);
            isFacingRight = false;
        }
    }

    public void Execute()
    {
        if (isFacingRight)
        {
            if (Vector2.Distance((Vector2)snail.transform.position, snail.PosRight) <= 0.01f)
            {
                FlipDirect();
            }
        }
        else
        {
            if (Vector2.Distance((Vector2)snail.transform.position, snail.PosLeft) <= 0.01f)
            {
                FlipDirect();
            }
        }
    }

    public void Exit()
    {
        
    }
   
    void FlipDirect()
    {
        snail.rb.velocity = new Vector2(-snail.rb.velocity.x, 0);
        isFacingRight = !isFacingRight;
    }
}
