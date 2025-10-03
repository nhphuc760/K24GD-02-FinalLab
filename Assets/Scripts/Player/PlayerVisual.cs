using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
 
 
    private void Update()
    {
        if(GameInput.Ins.GetMovementVectorNormalized() != 0)
        {
            if(GameInput.Ins.GetMovementVectorNormalized() < 0)
            {
             
               transform.localScale = new Vector3(-1, 1, 1);
            }
            else 
            {
              
                transform.localScale = Vector3.one;
            }
        }
    }
}
