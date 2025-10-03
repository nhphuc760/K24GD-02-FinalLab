using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarHit : Istate
{
    readonly Boar boar;
    readonly Animator animator;
    AnimatorStateInfo currentBaseState; 
    public BoarHit(Boar boar)
    {
        this.boar = boar;
        animator= boar.animator;
    }
    public void Enter()
    {
        
        animator.CrossFade(Boar.BoarState.BoarHit.ToString(), 0.1f);
    }

    public void Execute()
    {
    currentBaseState = animator.GetCurrentAnimatorStateInfo(0);
        
        if (currentBaseState.normalizedTime >= 1f && currentBaseState.IsName(Boar.BoarState.BoarHit.ToString()))
       {
            if(boar.CurrentHealth <= 0)
            {
                boar.Die();
                return;
            }
            if (Random.Range(0f, 1f) <= 0.35f)
                boar.stateMachine.ChangeState(boar.boarRun);
            else
                boar.stateMachine.ChangeState(boar.boarIdle);
        }
        
    }

    public void Exit()
    {
       
    }

    
}
