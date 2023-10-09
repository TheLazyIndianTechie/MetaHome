using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;
using Unity.VisualScripting;

public class MainMenuManager : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void displayIframe();

    [DllImport("__Internal")]
    private static extern void gameLoaded();

    private void Start()
    {
        gameLoaded();

#if !UNITY_EDITOR && UNITY_WEBGL
                // disable WebGLInput.captureAllKeyboardInput so elements in web page can handle keyboard inputs
                WebGLInput.captureAllKeyboardInput = true;
#endif
    }
    public void CreateNewAvatar()
    {
        //redirect player to readyplayerme website...
        displayIframe();
    }

    public void ChangeAvatarUrl(string url)
    {
        string avatarUrl = url.ToString();

        if (avatarUrl != "")
        {
            GameManager.avatarUrlOfPlayer = avatarUrl;
            SceneManager.LoadScene((int)SceneIndex.LOBBY_SCENE);
        }
    }

    public void SetAvatarUrl(string url)
    {
        string avatarUrl = url.ToString();

        if (avatarUrl != "")
        {
            GameManager.avatarUrlOfPlayer = avatarUrl;
        }
    }

    public void ContinueToNext()
    {
        SceneManager.LoadScene((int)SceneIndex.LOBBY_SCENE);
    }

    public void GetPlayerName(string name)
    {
        Variables.Saved.Set("PlayerName", name);
    }

    public void GetPlayerCoins(int coins)
    {
        Variables.Saved.Set("Coins", coins);
    }

    public void GetPlayerEmail(string email)
    {
        Variables.Saved.Set("PlayerEmail", email);
    }

    //create one get for player mobile later..
}
