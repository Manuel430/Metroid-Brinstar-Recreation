using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamBullet : MonoBehaviour
{
    [SerializeField] float speed = 20f;
    Rigidbody2D rBody;
    [SerializeField] bool isMissile;

    private void Awake()
    {
        rBody = GetComponent<Rigidbody2D>();
    }

    public void Init(float direction)
    {
        if(direction > 0)
        {
            rBody.velocity = transform.right * speed;
        }
        else
        {
            rBody.velocity = transform.right * -speed;
        }
    }

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
}
