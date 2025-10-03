using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Minotaur : EnemyBase
{

    public  MinoIdle minoIdle;
    public  MinoAttack minoAttack;
    public  MinoWalk minoWalk;
    [SerializeField] Vector3 areaAttacking;
    [SerializeField] float attackDurationInterval;
    Vector2 posLeft;
    Vector2 posRight;
    Vector3 boxSize;
    bool inCamera;

    public Vector2 PosLeft { get => posLeft;  }
    public Vector2 PosRight { get => posRight;  }
    public bool InCamera { get => inCamera; set => inCamera = value; }
    public float AttackDurationInterval { get => attackDurationInterval; set => attackDurationInterval = value; }

    protected override void Awake()
    {
        base.Awake();
        minoAttack = new MinoAttack(this);
        minoIdle = new MinoIdle(this);
        minoWalk = new MinoWalk(this);
        stateMachine = new StateMachine(minoIdle);

    }
    protected override void Start()
    {
        base.Start();
        posLeft = startPos + Vector2.left * rangeActive / 2f;
        posRight = startPos + Vector2.right * rangeActive / 2f;
        boxSize = new Vector3(rangeActive, 5f, 0);
    }
    protected override void Update()
    {
        base.Update();
        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
        inCamera = viewPos.x >=0 && viewPos.x <=1 && viewPos.y >=0 && viewPos.y <=1;
    }
    public enum MinoState
    {
        Idle,
        Walk,
        Attack
    }


    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.yellow;
        Vector3 boxSize = new Vector3(rangeActive, 5f, 0);
        Handles.DrawWireCube(transform.position, boxSize);
        Handles.color = Color.red;
        Handles.DrawWireCube(transform.position, areaAttacking);
    }
    public bool CheckPlayerInRange(out Collider2D col)
    {
        col = Physics2D.OverlapBox(startPos, boxSize, 0, 1 << 6);
        return col != null;
    }

    public bool CheckPlayerInAttackArea(out Collider2D col)
    {
        col = Physics2D.OverlapBox(transform.position, areaAttacking, 0f, 1 << 6);
        return col != null;
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        if(currentHealth <= 0)
        {
            Die();
            UIManager.Ins.TriggerGameWin();
        }
    }
}
