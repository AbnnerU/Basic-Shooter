using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.InputSystem;

[Serializable] public class MoveInputEvent : UnityEvent<float, float> { }
[Serializable]public class JumpInputEvent : UnityEvent<float> { }
[Serializable] public class FlyInputEvent : UnityEvent<float> { }



public class InputControler : MonoBehaviour
{
    private PlayerInputControls controls;

    public MoveInputEvent moveInputEvent;
    public JumpInputEvent jumpInputEvent;
    public FlyInputEvent flyInputEvent;

    private Vector2 mousePosition;

    private void Awake()
    {
        controls = new PlayerInputControls();
    }

    private void OnEnable()
    {
        controls.Gameplay.Enable();
        controls.Gameplay.Move.performed += ctx => OnMovePerformed(ctx.ReadValue<Vector2>());
        controls.Gameplay.Move.canceled += ctx => OnMovePerformed(ctx.ReadValue<Vector2>());

        controls.Gameplay.Jump.performed += ctx => OnJumpPerformed(ctx.ReadValue<float>());
        controls.Gameplay.Jump.canceled += ctx => OnJumpPerformed(ctx.ReadValue<float>());

        controls.Gameplay.Fly.performed += ctx => OnFlyPerformed(ctx.ReadValue<float>());
        controls.Gameplay.Fly.canceled += ctx => OnFlyPerformed(ctx.ReadValue<float>());

        controls.Gameplay.Look.performed += ctx => SetMousePosition(ctx.ReadValue<Vector2>());



    }

    #region Perfomed
    private void OnMovePerformed(Vector2 ctx)
    {
        Vector2 moveInput = ctx;
        moveInputEvent.Invoke(moveInput.x, moveInput.y);
    }

    private void OnJumpPerformed(float ctx)
    {
        float jumpValue = ctx;
        jumpInputEvent.Invoke(jumpValue);
        //print(jumpValue);
    }

    private void OnFlyPerformed(float ctx)
    {
        float jumpValue = ctx;
        flyInputEvent.Invoke(jumpValue);
    }

    private void SetMousePosition(Vector2 ctx)
    {
        mousePosition = ctx;
        if(mousePosition.x <= 0)
        {
            mousePosition.x = 0;
        }
        else if (mousePosition.x >= Screen.width)
        {
            mousePosition.x = Screen.width;
        }

        if (mousePosition.y <= 0)
        {
            mousePosition.y = 0;
        }
        else if (mousePosition.y >= Screen.height)
        {
            mousePosition.y = Screen.height;
        }
    }

    #endregion 


    public Vector2 GetMousePosition()
    {
        return mousePosition;
    }
}
