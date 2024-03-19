using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamusUpgradeCheck : MonoBehaviour
{
    [SerializeField] bool canMorphball;

    public void SetMorphballCheck(bool itemCheck)
    {
        canMorphball = itemCheck;
    }

    public bool GetMorphballCheck()
    {
        return canMorphball;
    }
}
