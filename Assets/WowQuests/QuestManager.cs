using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace WowQuests
{
    public class QuestManager : MonoBehaviour
    {
        private QuestCollectionData _myQuestCollectionData;

        private QuestData _myQuest;

        public Dictionary<int, QuestData> QuestHashMap;

        public static event Action<QuestData> OnQuestRetrieved;

        public static event Action<int> OnQuestCompleted;

        public static event Action<bool> OnAllQuestsCompleted;

        private void OnEnable()
        {
            QuestTriggerScript.OnQuestTriggered += RetrieveQuestsDataFromVisualScripting;
        }

        private void OnDisable()
        {
            QuestTriggerScript.OnQuestTriggered -= RetrieveQuestsDataFromVisualScripting;
        }

        public void RetrieveQuestsDataFromVisualScripting(int questId)
        {
            QuestData activeQuestData = (QuestData)Variables.Application.Get("ActiveQuestData");
            QuestCollectionData questCollectionData = (QuestCollectionData)Variables.Application.Get("QuestCollectionData");
            if (questCollectionData.quests.Count > 0)
            {
                QuestData result = questCollectionData.quests.Find(x => x.questId == questId);
                Debug.Log(result.questId + result.questDescription + result.questName);
                QuestData questData = new QuestData(result.questId, result.questName, result.questDescription, result.itemId, result.fulfillmentValue);
                Variables.Application.Set("ActiveQuestData", questData);
                OnQuestRetrieved?.Invoke(result);
            }
            else if (questCollectionData.quests.Count == 0)
            {
                OnAllQuestsCompleted?.Invoke(true);
                Debug.Log("User has completed all quests");
            }
        }
    }
}
