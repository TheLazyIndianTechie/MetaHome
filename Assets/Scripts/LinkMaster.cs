using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LinkMaster : MonoBehaviour
{
    public static string url = "https://metaport.to/decentraland";


    [DllImport("__Internal")]
    private static extern void LinkBetweenWorlds(string url);

    public void LaunchPage()
    {
        LinkBetweenWorlds(url);
    }

    private void Start()
    {
        Invoke(nameof(LaunchPage), 4f);
    }
}
