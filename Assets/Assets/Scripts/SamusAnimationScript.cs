using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamusAnimationScript : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] SamusScript samus;

    [Header("WeaponChecks")]
    [SerializeField] bool isVariaSuit;
    [SerializeField] bool isMissileSuit;
    [SerializeField] bool isIceBeam;

    [Header("Fire Positions")]
    [SerializeField] Transform firePoint;
    [SerializeField] Transform firePointUp;

    [Header("Bullet Prefabs")]
    [SerializeField] GameObject powerBeam;
    [SerializeField] GameObject variaBeam;
    [SerializeField] GameObject iceBeam;
    [SerializeField] GameObject normalMissile;
    [SerializeField] GameObject variaMissile;
    
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
        isMissileSuit = isMissile;
    }

    public void VariaAnim()
    {
        animator.SetTrigger("VariaSuit");
    }

    private void ChangeToVaria()
    {
        if(isVariaSuit == true)
        {
            return;
        }

        isVariaSuit = true;
    }

    public void IceBeamActive()
    {
        isIceBeam = true;
    }

    private void Shoot()
    {
        if (isIceBeam)
        {
            GameObject bullet = Instantiate(iceBeam, firePoint.position, firePoint.rotation);
            bullet.GetComponent<BeamBullet>().Init(samus.transform.localScale.x);
            return;
        }

        if (isVariaSuit)
        {
            GameObject bullet = Instantiate(variaBeam, firePoint.position, firePoint.rotation);
            bullet.GetComponent<BeamBullet>().Init(samus.transform.localScale.x);
            return;
        }
        else
        {
            GameObject bullet = Instantiate(powerBeam, firePoint.position, firePoint.rotation);
            bullet.GetComponent<BeamBullet>().Init(samus.transform.localScale.x);
        }
        
    }

    private void ShootUp()
    {
        if(isIceBeam)
        {
            GameObject bullet = Instantiate(iceBeam, firePointUp.position, firePointUp.rotation);
            bullet.GetComponent<BeamBullet>().Init(samus.transform.localScale.y);
            return;
        }

        if (isVariaSuit)
        {
            GameObject bullet = Instantiate(variaBeam, firePointUp.position, firePointUp.rotation);
            bullet.GetComponent<BeamBullet>().Init(samus.transform.localScale.y);
            return;
        }
        else
        {
            GameObject bullet = Instantiate(powerBeam, firePointUp.position, firePointUp.rotation);
            bullet.GetComponent<BeamBullet>().Init(samus.transform.localScale.y);
        }
    }

    private void ShootMissile()
    {
        if(isVariaSuit)
        {
            GameObject bullet = Instantiate(variaMissile, firePoint.position, firePoint.rotation);
            bullet.GetComponent<BeamBullet>().Init(samus.transform.localScale.x);
            return;
        }
        else
        {
            GameObject bullet = Instantiate(normalMissile, firePoint.position, firePoint.rotation);
            bullet.GetComponent<BeamBullet>().Init(samus.transform.localScale.x);
            return;
        }

    }

    private void ShootUpMissile()
    {
        if (isVariaSuit)
        {
            GameObject bullet = Instantiate(variaMissile, firePoint.position, firePoint.rotation);
            bullet.GetComponent<BeamBullet>().Init(samus.transform.localScale.y);
            return;
        }
        else
        {
            GameObject bullet = Instantiate(normalMissile, firePoint.position, firePoint.rotation);
            bullet.GetComponent<BeamBullet>().Init(samus.transform.localScale.y);
            return;
        }
    }
}
