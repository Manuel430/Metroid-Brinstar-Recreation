using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SamusHealth : MonoBehaviour
{
    [SerializeField] int health;
    [SerializeField] int maxHealth;
    
    [SerializeField] TextMeshProUGUI healthText;
    
    [SerializeField] GameObject gameOverUI;

    private void Awake()
    {
        healthText.text = health.ToString();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        healthText.text = health.ToString();

        if(health <= 0)
        {
            health = 0;
            GameOver();

        }
    }

    public void RestoreHealth()
    {
        health = maxHealth;
        healthText.text = health.ToString();
    }

    private void GameOver()
    {
        gameOverUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;
    }

}
