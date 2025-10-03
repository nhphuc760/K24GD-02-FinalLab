using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingArea : MonoBehaviour
{
    [SerializeField] GameObject presenter;
    [SerializeField] LayerMask targetLayer;
    int damage;
    private void Start()
    {
        if (presenter.TryGetComponent<IDamageAble>(out IDamageAble damageDealer))
        {
            damage = damageDealer.GetDamage();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((1 << collision.gameObject.layer & targetLayer) != 0)
        {
            if(collision.TryGetComponent<IDamageAble>(out IDamageAble damageReceiver))
            {
                
                    damageReceiver.TakeDamage(damage);
                
                
            }
           
        }
    }
}
