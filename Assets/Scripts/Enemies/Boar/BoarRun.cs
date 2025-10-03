using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarRun : Istate
{
    readonly Boar boar;
    readonly Animator animator;
    Collider2D collider2D;
    float duration;
    Vector2 tarGetPos;
    bool isFacingRight;
    public BoarRun(Boar boar)
    {
        this.boar = boar;
        animator= boar.animator;
        collider2D= boar.GetComponent<Collider2D>();
    }
    public void Enter()
    {
        animator.CrossFade(Boar.BoarState.BoarRun.ToString(), 0.1f);
        collider2D.isTrigger = false;
        duration = Random.Range(.5f, boar.timeChangeState);
        if (Random.Range(0, 2) == 0)
        {
            boar.rb.velocity = new Vector2(boar.GetEnemyObjectSO.speed_run, 0);
            isFacingRight = true;
        }
        else
        {
            boar.rb.velocity = new Vector2(-boar.GetEnemyObjectSO.speed_run, 0);
            isFacingRight = false;
        }
    }

    public void Execute()
    {
        duration -= Time.deltaTime;
        if (isFacingRight)
        {
            tarGetPos = (boar.PosRight - (Vector2)boar.transform.position).normalized;
         
            if (Vector2.Dot(boar.rb.velocity.normalized, tarGetPos) <=0 )
            {
                Flip();
            }
        }
        else
        {

            tarGetPos =( boar.PosLeft - (Vector2)boar.transform.position).normalized;
            
            if (Vector2.Dot(boar.rb.velocity.normalized, tarGetPos) <= 0)
            {

                Flip();
            }
        }

        if (duration <= 0)
        {
            boar.stateMachine.ChangeState(boar.stateMachine.oldState);
        }
    }

    public void Exit()
    {
       collider2D.isTrigger = true;
    }
    void Flip()
    {
        boar.rb.velocity = new Vector2(-boar.rb.velocity.x, 0);
        isFacingRight = !isFacingRight;
    }
    
}
