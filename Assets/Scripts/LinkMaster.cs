using System.Runtime.InteropServices;
using UnityEngine;
using WowPortals.PortalScripts;

public class LinkMaster : MonoBehaviour
{
    public static string URL = "https://world-bridge.metaport.to/";
    
    [DllImport("__Internal")]
    private static extern void LinkBetweenWorlds(string url);
    
    private void OnEnable() => PortalManager.OnPortalActivated += LaunchPage;
    private void OnDisable() => PortalManager.OnPortalActivated -= LaunchPage;

    public static void LaunchPage()
    {
        Debug.Log("Launching page");
        LinkBetweenWorlds(URL);
    }
}
