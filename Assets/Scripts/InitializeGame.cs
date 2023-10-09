using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class InitializeGame : MonoBehaviour
{
    [SerializeField]
    private TMP_Text loadingStatus;

    [SerializeField]
    private GameObject btnPlay;

    // Lifecycle methods
    private void Start()
    {
 /*       loadingStatus.SetText("Data is being initialized...");

#if !(UNITY_EDITOR || UNITY_EDITOR_OSX || UNITY_EDITOR_64)
        StartCoroutine(GetUserData());
        // ReactDataManager.Instance.CallGetUserData(gameObject.name, nameof(FetchUserData));
#else
        FetchUserData("");
#endif*/
    }

    public void OnPlayPressed()
    {
        loadingStatus.SetText("Data is being initialized...");
        
        btnPlay.SetActive(false);

#if !(UNITY_EDITOR || UNITY_EDITOR_OSX || UNITY_EDITOR_64)
        ReactDataManager.Instance.CallGetUserData(gameObject.name, nameof(FetchUserData));
#else
        FetchUserData("");
#endif
    }

    public void FetchUserData(string userDataJSON)
    {
#if !(UNITY_EDITOR || UNITY_EDITOR_OSX || UNITY_EDITOR_64)
        ReactDataManager.Instance.GetUserData(userDataJSON);
        Debug.Log("Unity Debug Log - Getting User Data " + userDataJSON);
        
        loadingStatus.text = "Data has been initialized... Loading scene...";
        
        InitializationLogger("The variables have been set");
#else
        UserData userData = new UserData();
        ReactDataManager.Instance.GetUserData(JsonUtility.ToJson(userData));
        
        InitializationLogger("The variables have been set");
#endif

        StartCoroutine(LoadYourAsyncScene());
    }

    private void InitializationLogger(string initializationMessage)
    {
        Debug.Log("Status " + initializationMessage);
    }

    IEnumerator LoadYourAsyncScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync((int)SceneIndex.LOBBY_SCENE);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            loadingStatus.text = "Loading Status: " + (asyncLoad.progress * 100) + "%";
            yield return null;
        }
    }
}