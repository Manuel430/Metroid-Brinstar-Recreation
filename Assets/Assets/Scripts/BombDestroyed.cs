using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombDestroyed : MonoBehaviour
{
    private void DestroyBomb()
    {
        Destroy(gameObject);
    }
}
