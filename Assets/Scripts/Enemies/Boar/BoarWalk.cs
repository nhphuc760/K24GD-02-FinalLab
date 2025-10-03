using UnityEngine;

public class BoarWalk : Istate
{
    readonly Boar boar;
    readonly Animator animator;
    float duration;
    bool isFacingRight;
    public BoarWalk(Boar boar)
    {
        this.boar = boar;
        animator= boar.animator;
        
    }
    public void Enter()
    {
        animator.CrossFade(Boar.BoarState.BoarWalk.ToString(), 0.1f);
       
        if(Random.Range(0, 2) == 0)
        {
            boar.rb.velocity = new Vector2(boar.GetEnemyObjectSO.speed_walk, 0);
            isFacingRight = true;
        }
        else
        {
            boar.rb.velocity = new Vector2(-boar.GetEnemyObjectSO.speed_walk, 0);
            isFacingRight = false;
        }
            duration = boar.timeChangeState;
    }

    public void Execute()
    {
        duration -= Time.deltaTime;

        if (isFacingRight)
        {
            if (Vector2.Distance((Vector2)boar.transform.position, boar.PosRight) <= 0.1f)
            {
                boar.rb.velocity = new Vector2(-boar.rb.velocity.x, 0);
                isFacingRight = !isFacingRight;
            }
        }
        else
        {
            if (Vector2.Distance((Vector2)boar.transform.position, boar.PosLeft) <= 0.1f)
            {
                isFacingRight = !isFacingRight;
                boar.rb.velocity = new Vector2(-boar.rb.velocity.x, 0);
            }
        }
        
        if(duration <= 0)
        {
            boar.stateMachine.ChangeState(boar.boarIdle);
        }
    }

    public void Exit()
    {
        
    }
}
