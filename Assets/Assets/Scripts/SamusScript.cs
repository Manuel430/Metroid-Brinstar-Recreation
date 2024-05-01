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
    [SerializeField] SamusUpgradeCheck upgradeCheck;
    [SerializeField] Transform groundCheck;
    [SerializeField] Transform roofCheck;
    [SerializeField] Transform bombPoint;
    [SerializeField] GameObject morphBallBomb;
    [SerializeField] LayerMask groundLayer;

    [Header("Player Stats")]
    [SerializeField] float horizontal;
    [SerializeField] float speed;
    [SerializeField] float jump;

    [Header("Cutscene")]
    [SerializeField] bool inCutscene;
    [SerializeField] bool isMoving;

    [Header("Upgrades")]
    [SerializeField] bool isMorphBall;
    [SerializeField] bool canMissile;
    [SerializeField] int missileCount;
    [SerializeField] int maxMissileCount;
    [SerializeField] bool inVaria;
    [SerializeField] bool isAimingUp;
    [SerializeField] bool canBomb;

    #region Cutscene
    
    public bool SetCutscene (bool setCutscene)
    {
        inCutscene = setCutscene;
        if (inCutscene)
        {
            playerActions.Player.Disable();

        }
        else
        {
            playerActions.Player.Enable();
        }
        return inCutscene;
    }

    public bool GetCutscene ()
    {
        return inCutscene;
    }

    public void MissileExpand(int expansionNumber)
    {
        maxMissileCount += expansionNumber;

        missileCount = maxMissileCount;
    }

    public void SetAimingUp(bool setAimingUp)
    {
        isAimingUp = setAimingUp;
    }

    public bool GetAimingUp()
    {
        return isAimingUp;
    }

    public void SetBombTrue()
    {
        canBomb = true;
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
        playerActions.Player.Jump.canceled += Jump;

        playerActions.Player.Morphball.performed += Morphball;

        playerActions.Player.Aim.performed += AimingUp;
        playerActions.Player.Aim.canceled += AimingUp;

        playerActions.Player.Fire.performed += Firing;
        playerActions.Player.Fire.canceled += Firing;

        playerActions.Player.MissileSelect.performed += MissileSelect;

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
        if(inCutscene)
        {
            rBody.velocity = Vector2.zero;
            return;
        }

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

    private bool Ceiling()
    {
        return Physics2D.OverlapCircle(roofCheck.position, 0.2f, groundLayer);
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
            if (isMorphBall)
            {
                if (Ceiling())
                {
                    return;
                }

                samusAnim.MorphballAnim(false);
                isMorphBall = false;
                return;
            }

            samusAnim.SpinningAnim();
            rBody.velocity = new Vector2(rBody.velocity.x, jump);
        }

        if(context.canceled && rBody.velocity.y > 0f)
        {
            rBody.velocity = new Vector2(rBody.velocity.x, rBody.velocity.y * 0.5f);
        }
    }

    private void Morphball(InputAction.CallbackContext context)
    {
        if (inCutscene || horizontal != 0)
        {
            return;
        }

        if(upgradeCheck.GetMorphballCheck() && IsGrounded())
        {
            samusAnim.MorphballAnim(true);
            isMorphBall = true;
        }
        else
        {
        }

    }

    private void AimingUp(InputAction.CallbackContext context)
    {
        if(inCutscene)
        {
            return;
        }

        if (context.performed)
        {
            if (isMorphBall)
            {
                if (Ceiling())
                {
                    return;
                }

                samusAnim.MorphballAnim(false);
                isMorphBall = false;
                return;
            }
            isAimingUp = true;
            SetAimingUp(true);
            samusAnim.AimUpAnim(true);
        }

        if (context.canceled)
        {
            isAimingUp = false;
            SetAimingUp(false);
            samusAnim.AimUpAnim(false);
        }

    }

    private void Firing(InputAction.CallbackContext context)
    {
        if (inCutscene || horizontal != 0)
        {
            return;
        }

        if (context.performed)
        {
            if(isMorphBall)
            {
                if(canBomb)
                {
                    Debug.Log("Deploy Bomb");

                    GameObject bomb = Instantiate(morphBallBomb, bombPoint.position, bombPoint.rotation);
                }
                else
                {
                    Debug.Log("Cannot Bomb");
                }
                
                return;
            }

            if (canMissile)
            {
                missileCount--;

                if(missileCount <= 0)
                {
                    missileCount = 0;
                    samusAnim.MissileAnim(false);
                }
            }

            samusAnim.FiringAnim();
        }
    }

    private void MissileSelect(InputAction.CallbackContext context)
    {
        if (inCutscene || horizontal != 0 || missileCount == 0 || !IsGrounded() || isAimingUp || isMorphBall)
        {
            return;
        }

        if (upgradeCheck.GetMissileCheck())
        {
            if (!canMissile)
            {
                canMissile = true;
                samusAnim.MissileAnim(true);
            }
            else
            {
                canMissile = false;
                samusAnim.MissileAnim(false);
            }
        }
    }

}
