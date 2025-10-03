using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dead : Istate
{
    readonly Player player;
    readonly Animator animator;
    public Dead(Player player)
    {
        this.player = player;
        animator= player.animator;
    }
    public void Enter()
    {
      
    }

    public void Execute()
    {
       
    }

    public void Exit()
    {
        
    }

   
}
