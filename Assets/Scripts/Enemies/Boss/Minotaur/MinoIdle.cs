using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinoIdle : Istate
{
    readonly Minotaur mino;
    readonly Animator animator;
    float duration;
    public MinoIdle(Minotaur mino)
    {
        this.mino = mino;
        animator = mino.animator;
    }

    public void Enter()
    {
      
        duration = Random.Range(0.7f * mino.timeChangeState , mino.timeChangeState); //70% - 100%
        animator.CrossFade(Minotaur.MinoState.Idle.ToString(), .1f);
        mino.rb.velocity = Vector2.zero;
    }

    public void Execute()
    {
       
        duration -= Time.deltaTime; 
        if(duration <= 0f)
        {
            mino.stateMachine.ChangeState(mino.minoWalk);
        }
        if (!mino.InCamera) return;
        if(mino.CheckPlayerInRange(out Collider2D col))
        {
            mino.stateMachine.ChangeState(mino.minoWalk);
        }
    }

    public void Exit()
    {
        
    }
}
