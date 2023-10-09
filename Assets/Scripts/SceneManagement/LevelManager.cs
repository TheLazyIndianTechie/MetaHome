using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;

public class LevelManager : MonoBehaviour
{
    public static void LoadMetaHome()
    {
        SceneManager.LoadSceneAsync((int)SceneIndex.LOBBY_SCENE);
        Debug.Log("Loaded Scene: " + SceneIndex.LOBBY_SCENE);
    }

    public static void LoadMetaHomeExterior()
    {
        SceneManager.LoadSceneAsync((int)SceneIndex.METAHOME_EXTERIOR_SCENE);
        Debug.Log("Loaded Scene: " + SceneIndex.METAHOME_EXTERIOR_SCENE);
    }

    public static void LoadMetaHomeInterior()
    {
        SceneManager.LoadSceneAsync((int)SceneIndex.METAHOME_INTERIOR_SCENE);
        Debug.Log("Loaded Scene: " + SceneIndex.METAHOME_INTERIOR_SCENE);
    }

    public static void LoadMetaPortOrbit()
    {
        SceneManager.LoadSceneAsync((int)SceneIndex.METAPORT_ORBIT);
        Debug.Log("Loaded Scene: " + SceneIndex.METAPORT_ORBIT);
    }

    public static void LoadMetaPortPortal(string url)
    {
        
        LinkMaster.URL = url;

        SceneManager.LoadSceneAsync((int)(SceneIndex.METAPORT_LOADER));
    }

    public void SetMetaPortDestination(string destination)
    {
        Variables.Application.Set("MetaPortDestination", destination);
    }
}
