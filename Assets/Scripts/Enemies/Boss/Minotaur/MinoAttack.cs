using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinoAttack : Istate
{
    readonly Minotaur mino;
    readonly Animator animator;
    AnimatorStateInfo animatorStateInfo;
    float attackDurationInterval;
    bool waitingInterval;
    public MinoAttack(Minotaur mino)
    {
        this.mino = mino;
        animator = mino.animator;
    }

    public void Enter()
    {
        
        
        mino.rb.velocity = Vector2.zero;
        animator.CrossFade(Minotaur.MinoState.Attack.ToString(), .1f);
        
    }

    public void Execute()
    {

        animatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if(animatorStateInfo.normalizedTime >= 1f && animatorStateInfo.IsName(Minotaur.MinoState.Attack.ToString()) && !waitingInterval)
        {
            waitingInterval = true;
            animator.CrossFade(Minotaur.MinoState.Idle.ToString(), .1f);
        }
       
        if (mino.CheckPlayerInAttackArea(out Collider2D player)){
            mino.lockAtPlayer = true;
            attackDurationInterval -= Time.deltaTime;
           
            if(attackDurationInterval <= 0)
            {
                attackDurationInterval = mino.AttackDurationInterval;
                animator.CrossFade(Minotaur.MinoState.Attack.ToString(), .1f);
                waitingInterval = false;
            }
          
        }
        else
        {
            mino.lockAtPlayer = false;
            
                mino.stateMachine.ChangeState(mino.minoWalk);
        }
       
    }

    public void Exit()
    {

    }

    
}
