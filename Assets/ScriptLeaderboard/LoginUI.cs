using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoginUI : MonoBehaviour
{
    public AuthHandler authHandler;
    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;
    public void OnLogin()
    {
        if (passwordInput.text.Length  > 0)
        {
            if ( emailInput.text.Length > 0)
            {
                authHandler.SignIn(emailInput.text, passwordInput.text);
            }
        }
    }
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Exit Game");
    }
}

