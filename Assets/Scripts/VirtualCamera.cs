using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class VirtualCamera : MonoBehaviour
{
    [SerializeField]
    CinemachineVirtualCamera cinemachine;
    private void Start()
    {     
        cinemachine.Follow = Player.Ins.transform;
    }
}
