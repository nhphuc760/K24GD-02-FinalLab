using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPosition : MonoBehaviour
{
    [SerializeField] Transform startPosition;
    private void Awake()
    {
       
       Player.Ins.transform.position = startPosition.position;
    }



}
