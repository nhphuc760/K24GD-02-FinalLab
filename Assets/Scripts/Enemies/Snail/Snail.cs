using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snail : EnemyBase
{
    

    public SnailWalk snailWalk;
    public SnailHit snailHit;
    public SnailDead snailDead;
    public SnailHide snailHide;
    public Vector2 StartPos => startPos;
    public int CurrentHealth => currentHealth;
    Vector2 posLeft;
    Vector2 posRight;
    public Vector2 PosLeft => posLeft;
    public Vector2 PosRight => posRight;
    
    public enum SnailState
    {
        Hide,
        Hit,
        Dead,
        Walk
        
    }

    protected override void Awake()
    {
        base.Awake();
       
        snailWalk = new SnailWalk(this);
        snailHit = new SnailHit(this);
        snailHide = new SnailHide(this);
        snailDead = new SnailDead(this);    
        stateMachine = new StateMachine(snailWalk);
    }
    protected override void Start()
    {
        base.Start();
        posLeft = (Vector2)transform.position + Vector2.left * rangeActive / 2f;
        posRight = (Vector2)transform.position + Vector2.right * rangeActive / 2f;
    }
    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        if (stateMachine.currentState == snailDead) return;
        stateMachine.ChangeState(snailHit);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + Vector2.right * rangeActive / 2f);
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + Vector2.left * rangeActive / 2f);
    }
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
    
}
