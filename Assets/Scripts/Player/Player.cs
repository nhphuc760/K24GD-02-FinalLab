using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageAble
{
    public static Player Ins { get; private set; }
    [Header("Component")]
    public Animator animator;
    public Rigidbody2D m_rb;  
    public StateMachine stateMachine;
    [Header("Player Info")]
    [SerializeField] int maxHealth = 100;   
    [SerializeField] Vector2 boxSize;
    [SerializeField] float speed = 5f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] int damage = 20;
    [Header("reference")]   
    [SerializeField] LayerMask groundCheck;
    [SerializeField] Transform checkGround;
    public GameObject AttackArea;
    bool attacking = false;
    int moveDirection;
    int currentHealth;  
    public int Damage { get { return damage; } }
    public float Speed { get { return speed; } }

    public bool Attacking { get => attacking; set => attacking = value; }
    public int MoveDirection { get => moveDirection; set => moveDirection = value; }
    

    public enum PlayerState
    {
        PlayerIdle,
        PlayerRun,
        PlayerAttack,
        PlayerDead,
        PlayerJumpStart,
        PlayerJumpEnd
    }
  
    public  Idle idle;
    public  Run run;
    public  Attack attack;
    public  Dead dead;
    public  JumpStart jumpStart;
    public  JumpEnd jumpEnd;

    public event EventHandler<IDamageAble.OnHealthChangeEventArgs> HealthChange;

    private void Awake()
    {
        if(Ins != null && Ins != this)
        {
            Destroy(gameObject);
            return;
        }
        Ins = this;
        idle = new Idle(this);
        run = new Run(this);
        attack = new Attack(this);
        dead = new Dead(this);
        jumpStart = new JumpStart(this);
        jumpEnd = new JumpEnd(this);
        stateMachine = new StateMachine(idle);
        DontDestroyOnLoad(gameObject);
        
    }
    private void Start()
    {
        currentHealth = maxHealth;
        UIManager.Ins.OnReplay += UI_OnReplay;
    }

   

    void Update()
    {
       stateMachine.Update();
        if (GameInput.Ins.IsPressAttack() && !attacking)
        {      
            stateMachine.ChangeState(attack);
        }
        if(GameInput.Ins.IsPressJump())
        {
            
            if(!IsOnGround()) return;
           
            m_rb.velocity = new Vector2(m_rb.velocity.x, jumpForce);
            stateMachine.ChangeState(jumpStart);
        }
    }

    
   public bool IsOnGround()
    {

        Collider2D hit = Physics2D.OverlapBox(checkGround.position, boxSize, 0f, groundCheck);
        return hit != null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(checkGround.position, boxSize);
    }
    
     public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        HealthChange?.Invoke(this, new IDamageAble.OnHealthChangeEventArgs { fillAmount = (float)currentHealth/maxHealth});
        if(currentHealth <= 0)
        {
            UIManager.Ins.TriggerGameOver();
        }
    }

    public int GetDamage() => damage;
    private void UI_OnReplay(object sender, EventArgs e)
    {
        currentHealth = maxHealth;
        HealthChange?.Invoke(this, new IDamageAble.OnHealthChangeEventArgs { fillAmount = 1f});
    }
    private void OnDestroy()
    {
        UIManager.Ins.OnReplay -= UI_OnReplay;
    }

}
