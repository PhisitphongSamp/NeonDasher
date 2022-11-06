using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public PauseMenu pauseButton;
    //public GameObject buttonController;

    public void PauseGame()
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        pauseButton.gameObject.SetActive(false);
        //buttonController.gameObject.SetActive(false);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        pauseButton.gameObject.SetActive(true);
        //buttonController.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        
        SceneManager.LoadScene(2);
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        pauseButton.gameObject.SetActive(true);
        //buttonController.gameObject.SetActive(true);
    }

    public void QuitToMain()
    {

        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
        pauseButton.gameObject.SetActive(true);
        //buttonController.gameObject.SetActive(true);
        
    }
}
