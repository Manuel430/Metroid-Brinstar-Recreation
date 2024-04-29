using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
