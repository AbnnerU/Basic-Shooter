using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine;


[Serializable]public class MoveAnimation: UnityEvent { }
[Serializable] public class ReverseMoveAnimation : UnityEvent { }
[Serializable] public class IdleAnimation : UnityEvent { }
[Serializable] public class FlyingAnimation : UnityEvent { }
[Serializable] public class ReverseFlyAnimation : UnityEvent { }


public class PlayerController : MonoBehaviour
{
    [SerializeField] private BodyManager body;
    [SerializeField] private GasBarManeger gasBar;

    [SerializeField] private int groundLayer;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float flyForce;

    public MoveAnimation moveAnimation;
    public ReverseMoveAnimation reverseMove;
    public IdleAnimation idleAnimation;
    public FlyingAnimation flyingAnimation;
    public ReverseFlyAnimation reverseFly;

    private string[] states = new string[] { "OnGround", "Jumped", "OnAir", "Flying" };

    private Rigidbody2D rb;

    private float vertical;
    private float horizontal;

    private bool moving;

    private bool currentLookDir;

    private string playerState;




    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moving = false;

        currentLookDir = body.PlayerlookToRigth();

    }

    private void Update()
    {

        if (playerState == states[0])
        {
            rb.drag = 15;
        }
        else
        {
            rb.drag = 1;
        }

        //Direção que o jogador esta olhando

        if (currentLookDir != body.PlayerlookToRigth())
        {
            currentLookDir = body.PlayerlookToRigth();

            OnMoveInput(horizontal, 0);
        }


        //Gas bar
        if (playerState == states[3])
        {
            if (gasBar.GetBarValue() > 0)
            {
                gasBar.DecreaseBar();
            }
        }





    }

    private void FixedUpdate()
    {
        if (moving)
        {
            rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);
        }

        if (playerState == states[1])
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * jumpForce);
            playerState = states[2];
        }

        if (playerState == states[3])
        {
            if (gasBar.GetBarValue() > 0)
            {
                rb.AddForce(Vector2.up * flyForce);
            }
        }


    }

    #region OnInputs

    public void OnMoveInput(float horizontal, float vertical)
    {
        this.vertical = vertical;
        this.horizontal = horizontal;

        if (horizontal == 0)
        {
            if (playerState == states[0])
            {
                idleAnimation.Invoke();
            }

            moving = false;
        }
        else if (horizontal > 0)
        {
            if (playerState == states[0])
            {
                if (currentLookDir == true)
                {
                    moveAnimation.Invoke();
                }
                else
                {
                    reverseMove.Invoke();
                }

            }
            else
            {
                if (currentLookDir == true)
                {
                    flyingAnimation.Invoke();
                }
                else
                {
                    reverseFly.Invoke();
                }

            }


            moving = true;
        }
        else if (horizontal < 0)
        {
            if (playerState == states[0])
            {
                if (body.PlayerlookToRigth() == true)
                {
                    reverseMove.Invoke();
                }
                else
                {
                    moveAnimation.Invoke();
                }

            }
            else
            {
                if (body.PlayerlookToRigth() == true)
                {
                    reverseFly.Invoke();
                }
                else
                {
                    flyingAnimation.Invoke();
                }

            }


            moving = true;
        }
    }

    public void OnJumpInput(float value)
    {
        if (playerState == states[0] && playerState != states[3])
        {
            flyingAnimation.Invoke();
            playerState = states[1];
        }
    }

    public void OnFlyInput(float value)
    {
        if (value >= 1)
        {
            if (playerState == states[2])
            {
                playerState = states[3];
            }
        }
        else
        {
            playerState = states[2];
        }
    }

    #endregion

    private void OnTriggerStay2D(Collider2D collision)
    {
        //print(collision.gameObject.layer);
        if (collision.gameObject.layer == groundLayer)
        {
          
            playerState = states[0];

            if (gasBar.GetBarValue() < gasBar.GetBarMaxValue())
            {
                gasBar.IncreaseBar();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == groundLayer)
        {
            playerState = states[0];

            OnMoveInput(horizontal, 0);

            //print(horizontal + " + " + playerState);
        }

    }

}
