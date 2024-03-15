using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.InputSystem;

public class SamusScript : MonoBehaviour
{
    [Header("Outside Inputs")]
    Rigidbody2D rBody;
    PlayerActionsScript playerActions;
    [SerializeField] SamusAnimationScript samusAnim;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;

    [Header("Player Stats")]
    [SerializeField] float horizontal;
    [SerializeField] float speed;
    [SerializeField] float jump;

    [Header("Cutscene")]
    [SerializeField] bool inCutscene;
    [SerializeField] bool isMoving;

    #region Cutscene
    
    public bool SetCutscene (bool setCutscene)
    {
        inCutscene = setCutscene;
        return inCutscene;
    }

    public bool GetCutscene ()
    {
        return inCutscene;
    }

    #endregion

    private void Awake()
    {
        rBody = GetComponent<Rigidbody2D>();
        playerActions = new PlayerActionsScript();
        playerActions.Player.Enable();

        playerActions.Player.Movement.performed += Move;
        playerActions.Player.Movement.canceled += Move;
        playerActions.Player.Jump.performed += Jump;
    }

    private void Update()
    {
        if(!inCutscene && IsGrounded())
        {
            samusAnim.JumpingAnim(false);
        }
        else
        {
            samusAnim.JumpingAnim(true);
        }
    }

    private void FixedUpdate()
    {
        rBody.velocity = new Vector2(horizontal * speed, rBody.velocity.y);
    }

    private void Flip()
    {
        if (inCutscene)
        {
            return;
        }

        transform.localScale = new Vector3(1 * horizontal, 1, 1);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Move(InputAction.CallbackContext context)
    {
        if (inCutscene)
        {
            return;
        }

        if(context.performed)
        {
            horizontal = context.ReadValue<Vector2>().x;
            Flip();
        }
        else if(context.canceled)
        {
            horizontal = 0f;
        }

        samusAnim.PlayerAnimMove(horizontal);
        
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if (inCutscene)
        {
            return;
        }

        if(context.performed && IsGrounded())
        {
            rBody.velocity = new Vector2(rBody.velocity.x, jump);
        }

        if(context.canceled && rBody.velocity.y > 0f)
        {
            rBody.velocity = new Vector2(rBody.velocity.x, rBody.velocity.y * 0.5f);
        }
    }

}
