using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boar : EnemyBase
{

    
    public BoarIdle boarIdle;
    public BoarRun boarRun;
    public BoarWalk boarWalk;
    public BoarHit boarHit;
     Vector2 posLeft;
    Vector2 posRight;
    public Vector2 StartPos => startPos;    
    public int CurrentHealth => currentHealth;  
    public Vector2 PosLeft => posLeft;
    public Vector2 PosRight => posRight;
    public enum BoarState
    {
        BoarIdle,
        BoarRun,
        BoarWalk,
        BoarHit
    }
    protected override void Awake()
    {
        base.Awake();
        boarIdle = new BoarIdle(this);
        boarRun = new BoarRun(this);
        boarWalk = new BoarWalk(this);
        boarHit = new BoarHit(this);
        stateMachine = new StateMachine(boarIdle);
    }
    protected override void Start()
    {
        base.Start();
        posLeft = (Vector2)transform.position + Vector2.left * rangeActive/2f;
        posRight = (Vector2)transform.position + Vector2.right * rangeActive/2f;
    }
   
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + Vector2.right * rangeActive / 2f);
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + Vector2.left * rangeActive / 2f);
    }
    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        stateMachine.ChangeState(boarHit);
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().TakeDamage(GetEnemyObjectSO.damage);
        }
    }
}
