using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    [HideInInspector]public ObjectiveTrigger objectiveTrigger;
   
    void Start()
    {
        objectiveTrigger = GetComponentInParent<ObjectiveTrigger>();  
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Collision Detected");
            objectiveTrigger.ObjectivePanel.SetActive(true);
            objectiveTrigger.Collectibles.SetActive(true);
            objectiveTrigger.QuestUI.SetActive(false);
            Destroy(transform.parent.gameObject);
            //  DestroyImmediate(questText, true);
        }
    }
}
