using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    InputActions inputActions;
    
    public static GameInput Ins { get; private set; }
    private void Awake()
    {
        
        if(Ins != null && Ins != this)
        {
            Destroy(gameObject);
            return;
        }
        Ins = this;
        DontDestroyOnLoad(this);
        inputActions = new InputActions();
        inputActions.Player.Enable();

    }

    public float GetMovementVectorNormalized()
    {
        return inputActions.Player.Move.ReadValue<float>();
    }
    public bool IsPressAttack()
    {
        return Keyboard.current.fKey.wasPressedThisFrame || Keyboard.current.enterKey.wasPressedThisFrame;
    }
    public bool IsPressJump()
    {
        return Keyboard.current.spaceKey.wasPressedThisFrame || Keyboard.current.wKey.wasPressedThisFrame;
    }
}
