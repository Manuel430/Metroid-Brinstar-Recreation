using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SamusScript : MonoBehaviour
{
    [Header("Outside Inputs")]
    Rigidbody2D rBody;
    PlayerActionsScript playerActions;
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
        playerActions.Player.Jump.performed += Jump;
    }

    private void Update()
    {
        //rBody.velocity = new Vector2(horizontal * speed, rBody.velocity.y);
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

        horizontal = context.ReadValue<Vector2>().x;
        
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if (inCutscene)
        {
            return;
        }

        if(context.performed && IsGrounded())
        {
            rBody.velocity = new(rBody.velocity.x, jump);
        }

    }

}
