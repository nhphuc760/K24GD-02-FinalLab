using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyVisual : MonoBehaviour
{
    [SerializeField] EnemyBase enemyBase;
    [SerializeField] Image fill;
    [SerializeField] GameObject Canvas;
    Rigidbody2D rb;
    bool isShow;
    private void Start()
    {
        rb = enemyBase.rb;
        enemyBase.HealthChange += EnemyBase_HealthChange;
        Hide();
    }

    private void EnemyBase_HealthChange(object sender, IDamageAble.OnHealthChangeEventArgs e)
    {
        if (!isShow) Show();
        fill.color = Color.Lerp(Color.red, Color.green,  e.fillAmount);
        fill.fillAmount = e.fillAmount;
    }

    void Show()
    {
        Canvas.SetActive(true);
        isShow = true;
    }
    void Hide()
    {
        Canvas?.SetActive(false);
        isShow = false;
    }



    private void Update()
    {
        if (enemyBase.lockAtPlayer) { 
            if(transform.position.x < enemyBase.Player.position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                transform.localScale = Vector3.one;
            }
        }
        if(rb.velocity.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if(rb.velocity.x < 0)
        {
            transform.localScale = Vector3.one;
        }
    }
    
}
