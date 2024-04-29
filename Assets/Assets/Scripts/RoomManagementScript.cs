using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManagementScript : MonoBehaviour
{
    [SerializeField] GameObject virtualCAM;
    [SerializeField] Animator transitionAnim;

    private void Awake()
    {
        virtualCAM.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !collision.isTrigger)
        {
            virtualCAM.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            virtualCAM.SetActive(false);
            transitionAnim.SetTrigger("Transitioning");
        }
    }
}
