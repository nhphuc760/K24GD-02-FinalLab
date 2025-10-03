using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine 
{
    public Istate currentState;
    public Istate oldState;

    public StateMachine(Istate initialState)
    {
        currentState = initialState;
        oldState = currentState;
        currentState.Enter();
    }
    public void ChangeState(Istate newState)
    {
        if (currentState != null)
        {
          
            
         
          
           
                
           
            currentState.Exit();
        }
        currentState = newState;
        currentState.Enter();
       
    }
    public void Update()
    {
        if (currentState != null)
        {
            currentState.Execute();
        }
    }
}

