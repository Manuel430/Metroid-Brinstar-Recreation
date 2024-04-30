using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamusAnimationScript : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] SamusScript samus;

    [Header("Fire Positions")]
    [SerializeField] Transform firePoint;
    [SerializeField] Transform firePointUp;

    [Header("Bullet Prefabs")]
    [SerializeField] GameObject powerBeam;
    
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

    public void AimUpAnim(bool isAimUp)
    {
        animator.SetBool("AimUp", isAimUp);
    }

    public void FiringAnim()
    {
        animator.SetTrigger("Firing");
    }

    public void MissileAnim(bool isMissile)
    {
        animator.SetBool("InMissile", isMissile);
    }

    public void VariaAnim()
    {
        animator.SetTrigger("VariaSuit");
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(powerBeam, firePoint.position, firePoint.rotation);
        bullet.GetComponent<BeamBullet>().Init(samus.transform.localScale.x);
    }

    private void ShootUp()
    {
        GameObject bullet = Instantiate(powerBeam, firePointUp.position, firePointUp.rotation);
        bullet.GetComponent<BeamBullet>().Init(samus.transform.localScale.y);
    }
}
