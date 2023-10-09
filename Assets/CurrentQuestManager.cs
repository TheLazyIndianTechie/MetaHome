using System;
using Unity.VisualScripting;
using UnityEngine;
using WowQuests;

public class CurrentQuestManager : MonoBehaviour
{
    private int _currentValue, _fulfillmentValue;

    public static event Action<QuestData> OnCurrentQuestRetrieved;
    public static event Action<int> OnQuestCompleted; 
    public static event Action<bool> OnUserReadyToMetaport; 
    private void OnEnable()
    {
        QuestManager.OnQuestRetrieved += HandleQuestRetrieved;
        NFTMarketplacePanelManager.OnNFTMarketplaceClosed += QuestCompletionHandler;
        PropertyMarketplaceTrigger.OnMetaportToHome += QuestCompletionHandler;
        EnterHomeTrigger.OnEnterHomeInterior += QuestCompletionHandler;
        NFTMarketPlace.OnNFTFramed += QuestCompletionHandler;
    }

    private void OnDisable()
    {
        QuestManager.OnQuestRetrieved -= HandleQuestRetrieved;
        NFTMarketplacePanelManager.OnNFTMarketplaceClosed -= QuestCompletionHandler;
        PropertyMarketplaceTrigger.OnMetaportToHome -= QuestCompletionHandler;
        EnterHomeTrigger.OnEnterHomeInterior -= QuestCompletionHandler;
        NFTMarketPlace.OnNFTFramed -= QuestCompletionHandler;
    }

    private void HandleQuestRetrieved(QuestData questData)
    {
        OnCurrentQuestRetrieved?.Invoke(questData);

        if(questData.questId == 105)
        {
            OnUserReadyToMetaport?.Invoke(true);
        }
    }

    public void QuestCompletionHandler(int questId)
    {
        QuestData qd = (QuestData)Variables.Application.Get("ActiveQuestData");
        Debug.Log(qd.questName + " is completed");
        if (questId != qd.questId) return;
#if !(UNITY_EDITOR || UNITY_EDITOR_OSX || UNITY_EDITOR_64)
        ReactDataManager.Instance.CallCompleteQuest(qd.questId, gameObject.name, nameof(UpdateUserReward));
#endif
        OnQuestCompleted?.Invoke(qd.questId);
        qd.questId = -1;
        Variables.Application.Set("ActiveQuestData", qd);
    }
    
    public void UpdateUserReward(string userDataJSON)
    {
        Debug.Log("Update user reward method being called");
        ReactDataManager.Instance.GetUserData(userDataJSON);
        Debug.Log(userDataJSON);
    }
}
