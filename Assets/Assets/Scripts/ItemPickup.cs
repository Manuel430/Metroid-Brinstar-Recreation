using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [Header("Item ID")]
    [SerializeField] int upgradeID;

    [Header("Upgrade Checks")]
    [SerializeField] SamusUpgradeCheck upgradeCheck;
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
                default:
                    Debug.Log("No item Aquired");
                    break;
            }

            Destroy(gameObject);
        }
    }
}
