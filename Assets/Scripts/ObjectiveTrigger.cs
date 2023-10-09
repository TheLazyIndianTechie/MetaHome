using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ObjectiveTrigger : MonoBehaviour
{
    public GameObject ObjectivePanel;
    public GameObject Collectibles;
    public GameObject QuestUI;
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            /*Text prefab = Instantiate(questText, transform.position, Quaternion.identity);
            prefab.transform.SetParent(canvas.transform, false);
            */
            QuestUI.SetActive(true);
        }
    }

}
