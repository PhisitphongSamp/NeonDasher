using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FullSerializer;
using Proyecto26;
using UnityEngine.SceneManagement;

public class AuthHandler : MonoBehaviour
{
    public const string apiKey = "AIzaSyAGo7zLw4QDB9iVK9oXRNTmpHoKLktDOis";

    private static fsSerializer serializer = new fsSerializer();
    public delegate void EmailVerificationSuccess();
    public delegate void EmailVerificationFail();
    public delegate void LoginSuccess();

    public static string idToken;
    public static string userId;

    public void SignUp(string email, string password, User user)
    {
        var payLoad = $"{{\"email\":\"{ email}\",\"password\":\"{ password}\",\"returnSecureToken\":true}}";

        RestClient.Post($"https://www.googleapis.com/identitytoolkit/v3/relyingparty/signupNewUser?key={apiKey}",
            payLoad).Then(
            response =>
            {
                Debug.Log("Created User");
                var responseJson = response.Text;
                // Using the FullSerializer library: https://github.com/jacobdufault/fullserializer
                // to serialize more complex types (a Dictionary, in this case)
                var data = fsJsonParser.Parse(responseJson);
                object deserialized = null;
                serializer.TryDeserialize(data, typeof(Dictionary<string, string>), ref deserialized);
                var authResponse = deserialized as Dictionary<string, string>;
                DatabaseHandler.PostUser(user, authResponse["localId"], OnSignUp, authResponse["idToken"]);

                SendEmailVerification(authResponse["idToken"]);

            }).Catch(error => {
                UnityEngine.Debug.Log(error);

            });

    }
    private static void SendEmailVerification(string newIdToken)
    {
        var payLoad = $"{{\"requestType\":\"VERIFY_EMAIL\",\"idToken\":\"{ newIdToken}\"}}";
        RestClient.Post($"https://www.googleapis.com/identitytoolkit/v3/relyingparty/getOobConfirmationCode?key={apiKey}", payLoad);
    }

    public void OnSignUp()
    {
        Debug.Log("SignUp");
    }

    private void CheckEmailVerification(string newIdToken, EmailVerificationSuccess callback,EmailVerificationFail fallback)
    {
        var payLoad = $"{{\"idToken\":\"{newIdToken}\"}}";
        RestClient.Post($"https://www.googleapis.com/identitytoolkit/v3/relyingparty/getAccountInfo?key={apiKey}",
            payLoad).Then(
            response =>
            {
                var responseJson = response.Text;


                var data = fsJsonParser.Parse(responseJson);
                Debug.Log(data);
                object deserialized = null;

                serializer.TryDeserialize(data, typeof(UserData), ref deserialized);

                var authResponse = deserialized as UserData;

                if (authResponse.users[0].emailVerified)
                {
                    userId = authResponse.users[0].localId;

                    idToken = newIdToken;
                    callback();
                }
                else
                {
                    fallback();
                }

            });
    }

    public void SignIn(string email, string password)
    {
        var payLoad = $"{{\"email\":\"{ email}\",\"password\":\"{ password}\",\"returnSecureToken\":true}}";
        RestClient.Post($"https://www.googleapis.com/identitytoolkit/v3/relyingparty/verifyPassword?key={apiKey}", payLoad).Then(response => {
            var responseJson = response.Text;

            var data = fsJsonParser.Parse(responseJson);
            object deserialized = null;
            serializer.TryDeserialize(data, typeof(Dictionary<string, string>), ref deserialized);

            var authResponse = deserialized as Dictionary<string, string>;

            CheckEmailVerification(authResponse["idToken"], () => {
                Debug.Log("Email verified, getting user info");


                // This line changed
                DatabaseHandler.GetUser(userId, user => {

                    PlayerPrefs.SetString("email", email);
                    PlayerPrefs.SetString("username", user.name);
                    PlayerPrefs.SetString("password", password);
                    PlayerPrefs.SetString("userId", userId);
                    PlayerPrefs.Save();
                    LoginGame();

                    //PlayerPrefs.GetString("username");


                }, idToken);



            }, () => {
                OnEmailFail();
                Debug.Log("Email not verified");
            });
        }).Catch(err => {
            OnLoginFail();

        });
    }

    public void OnEmailFail()
    {

    }

    public void OnLoginFail()
    {
        Debug.Log("Login Fail");
    }

    public void LoginGame()
    {
        SceneManager.LoadScene(1);
    }
}
