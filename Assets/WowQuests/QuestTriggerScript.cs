using System;
using UnityEngine;
using Unity.VisualScripting;
namespace WowQuests
{
    public class QuestTriggerScript : MonoBehaviour
    {
        [SerializeField] private int questId;

        public static event Action<int> OnQuestTriggered;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            
            var questData = (QuestData)Variables.Application.Get("ActiveQuestData");
            Debug.Log("OnTriggerEnter - ActiveQuest QuestID = " + questData.questId + ", Trigger QuestID = " + questId);
            if (questData.questId != -1) return;
            
            OnQuestTriggered?.Invoke(questId);

            Destroy(gameObject);
        }
    }
}