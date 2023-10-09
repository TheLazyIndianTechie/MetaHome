using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
using Unity.VisualScripting;

public class InitializeGame : MonoBehaviour
{
    [SerializeField]
    private TMP_Text loadingStatus;

    [SerializeField]
    private GameObject btnPlay;
    
    private string questjson = "";

    // Lifecycle methods
    private void Awake()
    {
        Variables.Application.Set("ActiveQuestData", new QuestData());    
    }

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
        ReactDataManager.Instance.CallGetQuestData(gameObject.name, nameof(FetchQuestData));
#else
        FetchUserData("");
        FetchQuestData("");
#endif
        StartCoroutine(LoadYourAsyncScene());
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

        Debug.Log("Debugging User Data Read" + JsonUtility.ToJson(userData));

        ReactDataManager.Instance.GetUserData(JsonUtility.ToJson(userData));
        
        InitializationLogger("The variables have been set");
#endif
    }
    public void FetchQuestData(string questJSON)
    {

#if !(UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_OSX || UNITY_EDITOR_WIN)
        ReactDataManager.Instance.GetUserQuestsData(questJSON);

        Debug.Log("Unity Debug Log - Getting Quest Data " + questJSON);

        loadingStatus.text = "Retrieving list of quests...";

        
#else
        QuestCollectionData questCollectionData = (QuestCollectionData)Variables.Application.Get("QuestCollectionData");
        /*QuestData questData = new();
        QuestData questData1 = new(9);
        questCollectionData.quests.Add(questData);
        questCollectionData.quests.Add(questData1);*/

        Debug.Log("Debugging User Data Read" + JsonUtility.ToJson(questCollectionData));

        ReactDataManager.Instance.GetUserQuestsData(JsonUtility.ToJson(questCollectionData));
#endif

    }


    public void UpdateUserOwnedNFT(string userNftJSON)
    {
        ReactDataManager.Instance.GetUserOwnedNFTCollectionData(userNftJSON);
        // StartCoroutine(LoadYourAsyncScene());
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