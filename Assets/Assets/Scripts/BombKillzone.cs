using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombKillzone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Breakable"))
        {
            Destroy(collision.gameObject);
        }
    }
}
