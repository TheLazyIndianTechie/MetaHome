using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public void LoadMetaHome()
    {
        SceneManager.LoadSceneAsync((int)SceneIndex.LOBBY_SCENE);
        Debug.Log("Loaded Scene: " + SceneIndex.LOBBY_SCENE);
    }


    public void LoadMetaHomeExterior()
    {
        SceneManager.LoadSceneAsync((int)SceneIndex.METAHOME_EXTERIOR_SCENE);
        Debug.Log("Loaded Scene: " + SceneIndex.METAHOME_EXTERIOR_SCENE);
    }

    public void LoadMetaHomeInterior()
    {
        SceneManager.LoadSceneAsync((int)SceneIndex.METAHOME_INTERIOR_SCENE);
        Debug.Log("Loaded Scene: " + SceneIndex.METAHOME_INTERIOR_SCENE);
    }

    public void LoadMetaPortOrbit()
    {
        SceneManager.LoadSceneAsync((int)SceneIndex.METAPORT_ORBIT);
        Debug.Log("Loaded Scene: " + SceneIndex.METAPORT_ORBIT);
    }

    public void LoadMetaPortPortal(string url)
    {
        LinkMaster.url = url;
        SceneManager.LoadSceneAsync((int)(SceneIndex.METAPORT_LOADER));
    }
}
