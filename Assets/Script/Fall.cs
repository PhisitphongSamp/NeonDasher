using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fall : MonoBehaviour
{
    
    public DeathMenu theDeathScreen;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            theDeathScreen.gameObject.SetActive(true);
            Time.timeScale = 0f;

        }
    }
}
