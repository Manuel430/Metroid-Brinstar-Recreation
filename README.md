# NES Metroid Brinstar Recreation
## About
NES Metroid Brinstar Recreation is the recreation of the first area of the NES game Metroid. While not one-to-one, the attempt was mostly focused on programming the main character, Samus, as she explored the halls of each area, gathering different upgrades that make her stronger.

## Coding
### Upgrades
Upgrades are the most important part of Metroid, as those abilities are what allow Samus to explore the rest of the game. Collecting the items is a simple task, as each item holds a special value that will determine what ability Samus will gain from it.
```cs
{
    [Header("Item ID")]
    [SerializeField] int upgradeID;

    [Header("Upgrade Checks")]
    [SerializeField] SamusUpgradeCheck upgradeCheck;
    [SerializeField] SamusScript SamusUpdate;
    [SerializeField] SamusAnimationScript variaUpgrade;
    [SerializeField] GameObject limiter_01;
    [SerializeField] GameObject limiter_02;
    [SerializeField] bool checker = true;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            switch (upgradeID)
            {
                case 1:
                    Debug.Log("Morphball Aquired");
                    upgradeCheck.SetMorphballCheck(checker);
                    break;
                case 2:
                    Debug.Log("Missile Aquired");
                    upgradeCheck.SetMissileCheck(checker);
                    SamusUpdate.SetIconActive();
                    SamusUpdate.MissileExpand(5);
                    break;
                case 3:
                    Debug.Log("Varia Aquired");
                    variaUpgrade.VariaAnim();
                    break;
                case 4:
                    Debug.Log("Health Aquired");
                    break;
                case 5:
                    Debug.Log("Bomb Aquired");
                    SamusUpdate.SetBombTrue();
                    break;
                case 6:
                    Debug.Log("Long Beam Aquired");
                    limiter_01.SetActive(false);
                    limiter_02.SetActive(false);
                    break;
                case 7:
                    Debug.Log("Ice Beam Aquired");
                    variaUpgrade.IceBeamActive();
                    break;
                default:
                    Debug.Log("No item Aquired");
                    break;
            }

            Destroy(gameObject);
        }
    }
}
```

Some of the abilities, such as the Morphball or Missiles, are in their own set in their own script to determine is Samus could use this ability.
```cs
public class SamusUpgradeCheck : MonoBehaviour
{
    [SerializeField] bool canMorphball;
    [SerializeField] bool canMissile;

    public void SetMorphballCheck(bool itemCheck)
    {
        canMorphball = itemCheck;
    }

    public bool GetMorphballCheck()
    {
        return canMorphball;
    }

    public void SetMissileCheck(bool itemCheck)
    {
        canMissile = itemCheck;
    }

    public bool GetMissileCheck()
    {
        return canMissile;
    }
}
```

### Morph Ball
Technically, Samus can already use this ability from the start, however without the ability collected, the script that checks Samus' possible abilities will return the input, therefore avoid the usage until the player collects the item.

```cs
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
    }
```

The only way to exit out of the Morph Ball, the player will either have to aim up or jump, as they bring her back into her normal stance. In this form, she is able to move around in small spaces, with the addition that she cannot go out of her normal stance if there is a ceiling above her.

```cs
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
```

```cs
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
```

### Missile
Missiles are controlled similarly to the Morph Ball, where Samus is capable of using the ability, but until she collects the item needed for the upgrade, she cannot use it. The only problem is that due to the enemies not dropping anything, Samus can only use five missiles, which those missiles can break the red doors.

```cs
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
```

```cs
            if (canMissile)
            {
                missileCount--;

                if(missileCount <= 0)
                {
                    missileCount = 0;
                    missileIcon.SetActive(false);
                    samusAnim.MissileAnim(false);
                }
```

### Beam
The simplest way Samus could defeat enemies, with a press of the button, she can shoot out a small beam of energy that will either destroy a block or damaged most enemies.
```cs
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
                    missileIcon.SetActive(false);
                    samusAnim.MissileAnim(false);
                }
            }

            samusAnim.FiringAnim();
        }
    }
```

```cs
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(isMissile)
        {
            if (collision.CompareTag("Ground"))
            {
                Destroy(gameObject);
            }
            else if (collision.CompareTag("Breakable") || collision.CompareTag("Missile"))
            {
                Destroy(collision.gameObject);
                Destroy(gameObject);
            }
            return;
        }

        if(collision.CompareTag("Ground") || collision.CompareTag("Limit"))
        {
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Breakable"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
```

However, this ability is very limited, having a short range that will destroy the energy when it comes into contact. That is why after collecting the long beam ability, those limiters are gone, allowing for a farther range to attack any enemy she can aim at.
```cs
                case 6:
                    Debug.Log("Long Beam Aquired");
                    limiter_01.SetActive(false);
                    limiter_02.SetActive(false);
                    break;
```

### Bomb
