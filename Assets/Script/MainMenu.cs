using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string playGameLevel;

    public void PlayGame()
    {
        Application.LoadLevel(playGameLevel);
    }

    public void LeaderBoard()
    {
        SceneManager.LoadScene(3);
    }
    
    public void QuitGame()
    {
        SceneManager.LoadScene(0);
    }
}
