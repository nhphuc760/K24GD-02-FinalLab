using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageAble
{
    event EventHandler<OnHealthChangeEventArgs> HealthChange;
    public class OnHealthChangeEventArgs : EventArgs
    {
        public float fillAmount;
    }
    void TakeDamage(int damage);
    int GetDamage();
}
