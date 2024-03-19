using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamusAnimationScript : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] SamusScript samus;
    
    public void SetCutscene()
    {
        samus.SetCutscene(false);
    }

    public void PlayerAnimMove(float horizontal)
    {
        animator.SetFloat("Speed", Mathf.Abs(horizontal));
    }

    public void JumpingAnim(bool isJumping)
    {
        animator.SetBool("isJumping", isJumping);
    }

    public void SpinningAnim()
    {
        animator.SetTrigger("Spinning");
    }

    public void MorphballAnim(bool isMorphball)
    {
        animator.SetBool("inMorphball", isMorphball);
    }
}
