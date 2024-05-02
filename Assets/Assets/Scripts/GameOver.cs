using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public void ReturnToTitle()
    {
        SceneManager.LoadScene(0);
    }
}
