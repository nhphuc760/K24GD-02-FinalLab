using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour, IDamageAble
{

    [SerializeField] protected EnemySO enemyObjectSO;
    [SerializeField] protected float rangeActive;
    public Rigidbody2D rb;
    public Animator animator;
    protected Vector2 startPos;
    public StateMachine stateMachine;
    public float timeChangeState;
    protected int currentHealth;
    public bool lockAtPlayer;
    public Transform Player;

    public event EventHandler<IDamageAble.OnHealthChangeEventArgs> HealthChange;

    public EnemySO GetEnemyObjectSO => enemyObjectSO;
    protected virtual void Awake()
    {
        if(rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
        startPos = transform.position;
        lockAtPlayer = false;
    }   
   
  

    protected virtual void OnEnable()
    {
        currentHealth = enemyObjectSO.maxHealth;
    }
    protected virtual void Start()
    {
        
    }
    protected virtual void OnDisable()
    {

    }
   
    protected virtual void Update()
    {
        stateMachine.Update();
    }

    
    public virtual void TakeDamage(int damage)
    {
       
      
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, enemyObjectSO.maxHealth);
        HealthChange?.Invoke(this, new IDamageAble.OnHealthChangeEventArgs { fillAmount = (float)currentHealth/enemyObjectSO.maxHealth});


    }
    public virtual void Die()
    {
       
        Destroy(gameObject);
    }

    public int GetDamage() => GetEnemyObjectSO.damage;
    
}
