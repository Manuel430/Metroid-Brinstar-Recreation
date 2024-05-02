using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] Animator enemyAnim;

    Rigidbody2D enemyRBody;

    private void Awake()
    {
        enemyRBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(IsFacingRight())
        {
            enemyRBody.velocity = new Vector2(moveSpeed, enemyRBody.velocity.y);
        }
        else
        {
            enemyRBody.velocity = new Vector2(-moveSpeed, enemyRBody.velocity.y);
        }
    }

    private bool IsFacingRight()
    {
        return transform.localScale.x > Mathf.Epsilon;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            transform.localScale = new Vector2(-(Mathf.Sign(enemyRBody.velocity.x)), transform.localScale.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Breakable"))
        {
            transform.localScale = new Vector2(-(Mathf.Sign(enemyRBody.velocity.x)), transform.localScale.y);
        }
    }

}
