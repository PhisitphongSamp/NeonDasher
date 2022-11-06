using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FullSerializer;
using Proyecto26;

public static class DatabaseHandler 
{
    public const string url = "https://fir-8eddb-default-rtdb.asia-southeast1.firebasedatabase.app/";
    public const string keyAuth = "AIzaSyAGo7zLw4QDB9iVK9oXRNTmpHoKLktDOis";

    private static fsSerializer serializer = new fsSerializer();
    public delegate void PostUserCallback();
    public delegate void GetUserCallback(User user);

    public static void PostUser(User user, string userId, PostUserCallback callback, string idToken)
    {
        Debug.Log("POST USER FUNTION");
        RestClient.Put<User>($"{url}User/userData/{userId}.json?auth={idToken}", user).Then(response => { callback(); }).Catch(error =>
        {
            Debug.Log(error);
        }
        );

    }

    public static void GetUser( string userId, GetUserCallback callback, string idToken)
    {
        RestClient.Get<User>($"{url}User/userData/{userId}.json?auth={idToken}").Then(user => { callback(user); }).Catch(error =>
        {
            Debug.Log(error);
        }
        );
    }
}
