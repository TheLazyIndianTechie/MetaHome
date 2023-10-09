using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using WowQuests;

public class QuestCanvasManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text questNameDisplay, questDescriptionDisplay, questStatusDisplay;
    private Canvas questCanvas;
    private bool questCompletionStatus = false;

    private void Awake()
    {
        Debug.Log("QuestCanvasManager: Awake called");

        questCanvas = GetComponent<Canvas>();
        DeactivateQuestCanvas();
    }

    private void Start()
    {
        Debug.Log("QuestCanvasManager: Start called");

        // Display the active quest (if it's active)
        QuestData questData = (QuestData)Variables.Application.Get("ActiveQuestData");
        if (!string.IsNullOrEmpty(questData.questName) && questData.questId == -1)
        {
            UpdateQuestCompletedDisplay(questData.questId);
        }
        else
        {
            DeactivateQuestCanvas();
        }
    }

    private void OnEnable()
    {
        //Listen to events of coins being picked up
        CurrentQuestManager.OnCurrentQuestRetrieved += UpdateQuestDisplay;
        CurrentQuestManager.OnQuestCompleted += UpdateQuestCompletedDisplay;
        // CollectiblesManager.OnCollectiblePickedUp += UpdateQuestProgressDisplay;
        // QuestManager.OnQuestCompleted += UpdateQuestCompletedDisplay;
    }

    private void OnDisable()
    {
        //Stop listening to coin pickup events
        CurrentQuestManager.OnCurrentQuestRetrieved -= UpdateQuestDisplay;
        CurrentQuestManager.OnQuestCompleted -= UpdateQuestCompletedDisplay;
        // QuestManager.OnQuestRetrieved -= UpdateQuestDisplay;
        // CollectiblesManager.OnCollectiblePickedUp -= UpdateQuestProgressDisplay;
        // QuestManager.OnQuestCompleted -= UpdateQuestCompletedDisplay;
    }

    public void UpdateQuestDisplay(QuestData questData)
    {
        Debug.Log("QuestCanvasManager: UpdateQuestDisplay called - " + questData.questId + " " + questData.questName);
        ActivateQuestCanvas();
        questNameDisplay.SetText(questData.questName);
        questDescriptionDisplay.SetText(questData.questDescription);
    }

    public void UpdateQuestProgressDisplay(string collectibleMessage, string collectibleType, int count)
    {
        //Temporarily clearing text
        questStatusDisplay.SetText("");

        int currentCount = (int)Variables.Application.Get(collectibleType);

        
        string questProgressMessage = collectibleMessage + " " + currentCount + " " + collectibleType;

        if (!questCompletionStatus)
        {
            questStatusDisplay.SetText(questProgressMessage);
        }

        else
        {
            questStatusDisplay.SetText("<s> " + questProgressMessage + " </s>");
        }
        
    }

    public void UpdateQuestCompletedDisplay(int questId)
    {
        QuestData questData = (QuestData)Variables.Application.Get("ActiveQuestData");

        ActivateQuestCanvas();
        questNameDisplay.SetText(questData.questName);
        questDescriptionDisplay.SetText("Quest: " + questData.questName + " completed!");

        questCompletionStatus = true;
    }

    public void ActivateQuestCanvas()
    {
        questStatusDisplay.SetText("");
        questCanvas.enabled = true;
    }

    public void DeactivateQuestCanvas()
    {
        questCanvas.enabled = false;
    }
}
