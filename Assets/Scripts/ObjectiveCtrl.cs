using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ObjectiveCtrl : MonoBehaviour
{
    public GameObject ObjectivePanel;
    public GameObject ObjectiveCompleted;
    public TextMeshProUGUI SubHeading;
    public TextMeshProUGUI Body;
    public TextMeshProUGUI Coinreward;

    public List<Objective> objectives;
    [HideInInspector] public int index = 0;
    // Start is called before the first frame update
    private void Start()
    {

        objectives[index].isActive = true;
        ObjectiveHandler(ref index);
    }
    private void Update()
    {

        if (objectives[index].isCompleted)
        {
            if (index == objectives.Count - 1)
            {
                objectives[index].isActive = false;
                ObjectiveHandler(ref index);
                ObjectiveCompleted.SetActive(true);
            }
            else
            {
                ObjectiveChecker(ref index);
            }
        }

    }

    void ObjectiveChecker(ref int index)
    {
        if (objectives[index].isCompleted)
        {

            objectives[index].isActive = false;
            index++;
            objectives[index].isActive = true;
            ObjectiveHandler(ref index);
        }
    }

    void ObjectiveHandler(ref int index)
    {
        if (objectives[index].isActive)
        {
            SubHeading.text = objectives[index].Subheading;
            Body.text = objectives[index].description;
            Coinreward.text = objectives[index].CoinRewards.ToString();
        }
        else
        {
            SubHeading.text = "";
            Body.text = "";
            Coinreward.text = "";

        }
 
    }
}
