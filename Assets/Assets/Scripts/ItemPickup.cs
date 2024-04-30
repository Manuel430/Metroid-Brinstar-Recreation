using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [Header("Item ID")]
    [SerializeField] int upgradeID;

    [Header("Upgrade Checks")]
    [SerializeField] SamusUpgradeCheck upgradeCheck;
    [SerializeField] SamusScript missileUpgrade;
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
                    missileUpgrade.MissileExpand(5);
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
