using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class userAccountLobby : MonoBehaviour
{
    public Text usernametext;

    private void Start()
    {
        if (UserAccountManager.IsLoggedIn)
        {
            usernametext.text = "Logged In As: " + UserAccountManager.PlayerUsername;

        }
    }

    public void LogOut()
    {
        if (UserAccountManager.IsLoggedIn)
        {
            UserAccountManager.instance.LoggedOut();
        }
    }
}
