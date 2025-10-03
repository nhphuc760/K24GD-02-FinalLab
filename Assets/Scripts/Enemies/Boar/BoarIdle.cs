using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarIdle : Istate
{
    readonly Boar boar;
    readonly Animator animator;
    float duration;
    public BoarIdle(Boar boar)
    {
        this.boar = boar;
        animator= boar.animator;
    }
    public void Enter()
    {
        boar.rb.velocity = Vector2.zero;
        duration = Random.Range( 1f, boar.timeChangeState);
        animator.CrossFade(Boar.BoarState.BoarIdle.ToString(), 0.1f);
        
    }

    public void Execute()
    {
        duration -= Time.deltaTime;
        if(duration <= 0)
        {
            
                switch (Random.Range(0f, 1f))
            {
                 case  <= 0.1f:
                    boar.stateMachine.ChangeState(boar.boarRun);
                    break;
                case <= 1f:
                    boar.stateMachine.ChangeState(boar.boarWalk);
                    break;
            }
           
        }
    }

    public void Exit()
    {
       
    }
}
