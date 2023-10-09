using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RedirectManager : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void launchWowRun();

    [DllImport("__Internal")]
    private static extern void openExternalUrl(string url);


    public void OpenExternalUrl(string url)
    {
        openExternalUrl(url);
    }

    public void LaunchWowRun()
    {
        launchWowRun();
    }

    public void GoToMetaHome()
    {
        SceneManager.LoadScene((int)SceneIndex.METAHOME_EXTERIOR_SCENE);
    }

}
