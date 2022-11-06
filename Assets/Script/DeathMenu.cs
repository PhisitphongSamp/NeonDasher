using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    //public string mainMenuLevel;

    public PauseMenu pauseButton;

    //public GameObject bottonController;

    public ScoreManager scoreManager;

    public void RestartGame()
    {
        scoreManager.LastScore();
        SceneManager.LoadScene(2);
        Time.timeScale = 1f;
        pauseButton.gameObject.SetActive(true);
        //bottonController.gameObject.SetActive(true);
      
    }

    public void QuitToMain()
    {
        scoreManager.LastScore();
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
        pauseButton.gameObject.SetActive(true);
        //bottonController.gameObject.SetActive(true);
    }
}
