using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamBullet : MonoBehaviour
{
    [SerializeField] float speed = 20f;
    Rigidbody2D rBody;

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
