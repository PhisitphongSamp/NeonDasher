using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RegisterUI : MonoBehaviour
{
    public AuthHandler authHandler;
    public TMP_InputField userInput;
    public TMP_InputField passwordInput;
    public TMP_InputField passwordConfirmInput;
    public TMP_InputField emailInput;

    public void OnRegister()
    {
        if(passwordInput.text == passwordConfirmInput.text && passwordInput.text.Length >= 8 && passwordInput.text.Length >0)
        {
            if(userInput.text.Length > 0 && emailInput.text.Length > 0)
            {
                authHandler.SignUp(emailInput.text, passwordInput.text, new User(userInput.text));
            }
        }
    }


}
