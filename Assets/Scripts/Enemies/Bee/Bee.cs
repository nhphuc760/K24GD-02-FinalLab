using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Bee : EnemyBase
{

    public BeeFly beeFly;
    public BeeHit beeHit;
    public BeeAttack beeAttack;
    public BeeIdle beeIdle;
   
   
    [SerializeField] float stingInterval;
    public GameObject attackingArea;
    bool isAttacking = false;
    bool inCamera;
    public int CurrentHealth => currentHealth;

   
    public bool IsAttacking { get => isAttacking; set => isAttacking = value; }
    public bool InCamera { get => inCamera; set => inCamera = value; }
    public float CountingTimeInterval { get => countingTime; set => countingTime = value; }

    public enum BeeState
    {
        Fly,
        Hit,
        Attack

    }
    protected override void Awake()
    {
        base.Awake();
        beeFly = new BeeFly(this);
        beeHit = new BeeHit(this);
        beeAttack = new BeeAttack(this);
        beeIdle = new BeeIdle(this);
        stateMachine = new StateMachine(beeFly);
    }
    protected override void Start()
    {
        base.Start();
        countingTime = 0;
    }
    protected override void Update()
    {
        base.Update();
        Vector3 viewpos = Camera.main.WorldToViewportPoint(transform.position);
         inCamera = viewpos.x >= 0 && viewpos.x <= 1 && viewpos.y >= 0 && viewpos.y <= 1;
       
    }
   
   
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.TryGetComponent<Player>(out Player player))
    //    {
              
    //        animator.CrossFade(BeeState.Attack.ToString(), .1f);
    //    }
    //}
    float countingTime;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            countingTime -= Time.deltaTime;
            if (countingTime <= 0)
            {
                animator.CrossFade(BeeState.Attack.ToString(), 0.1f);
                countingTime = stingInterval;

            }
        }
    }
    public Vector2 GetNextTargetPos()
    {
        float range = Random.Range(0f, rangeActive);
        float angle = Random.Range(0, 361);
        Vector2 targetPos = startPos + new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)) * range;
        return targetPos;
    }
    
    public Collider2D CheckPlayerInRange()
    {
        Collider2D collider = Physics2D.OverlapCircle(startPos, rangeActive, 1 << 6);
        return collider;
    }
    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, Vector3.forward, rangeActive);
    }
    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        
        stateMachine.ChangeState(beeHit);
    }
}
