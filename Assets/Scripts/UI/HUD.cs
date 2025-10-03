using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] Image fill;
    private void Start()
    {
        player.HealthChange += Player_HealthChange;
    }

    private void Player_HealthChange(object sender, IDamageAble.OnHealthChangeEventArgs e)
    {
        fill.fillAmount = e.fillAmount;
    }
}
