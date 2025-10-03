using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinoWalk : Istate
{
    readonly Minotaur mino;
    readonly Animator animator;
    bool isFacingRight;
    bool isFindPlayer;
    float duration;
    public MinoWalk(Minotaur mino)
    {
        this.mino = mino;
        animator = mino.animator;
    }

    public void Enter()
    {
       
        duration = Random.Range(0.8f * mino.timeChangeState, mino.timeChangeState);
        animator.CrossFade(Minotaur.MinoState.Walk.ToString(), .1f);
        
        if(Random.Range(0, 2) == 0)
        {
            isFacingRight = false;
            mino.rb.velocity = Vector2.left * mino.GetEnemyObjectSO.speed_walk;
        }
        else
        {
            isFacingRight= true;
            mino.rb.velocity = Vector2.right * mino.GetEnemyObjectSO.speed_walk;
        }
    }

    public void Execute()
    {
       if(!isFindPlayer)
       {
            duration -= Time.deltaTime;
            if (duration <= 0f)
            {
                mino.stateMachine.ChangeState(mino.minoIdle);
            }
        }
        if(isFacingRight)
        {
            if(Vector2.Distance(mino.PosRight, mino.transform.position) <= 0.2f)
            {
                Flip();
            }
        }
        else
        {
            if(Vector2.Distance(mino.PosLeft, mino.transform.position) <= 0.2f)
            {
                Flip();
            }
        }
        
        if (!mino.InCamera) return;
        if (mino.CheckPlayerInRange(out Collider2D player))
        {
            isFindPlayer = true;
            if(mino.Player == null)
            {
                mino.Player = player.transform;
            }
            Vector2 direc = player.transform.position.x < mino.transform.position.x ? Vector2.left : Vector2.right;
            mino.rb.velocity = direc * mino.GetEnemyObjectSO.speed_run;
            if(mino.CheckPlayerInAttackArea(out player))
            {
                mino.stateMachine.ChangeState(mino.minoAttack);
            }
           
        }
        else
        {
            isFindPlayer = false;
            if (isFacingRight)
                mino.rb.velocity = Vector2.right * mino.GetEnemyObjectSO.speed_walk;
            else
                mino.rb.velocity = Vector2.left * mino.GetEnemyObjectSO.speed_walk;
        }

    }

    public void Exit()
    {

    }

    void Flip()
    {
        mino.rb.velocity = new Vector2(-1f *mino.rb.velocity.x, 0f);
        isFacingRight = !isFacingRight;
    }
}
