using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collectibles : MonoBehaviour
{
    // Start is called before the first frame update
    
    public GameObject HealthParticleEffect;
    public GameObject StaminaParticleEffect;
    public GameObject DamageParticleEffect;
    public GameObject ObjectiveText;
    public Text FloatingText;
    public Canvas canvas;
    public GameObject TaskComplete;
    [SerializeField]public int totalcount;

    [HideInInspector]public static int count = 0;
    [HideInInspector] public ObjectiveCtrl objectivectrl;
    [HideInInspector] public int ind;
    StaminaCtrl staminaCtrl;  
     Health healthctrl;
    void Awake()
    {
        staminaCtrl = GameObject.FindGameObjectWithTag("Player").GetComponent<StaminaCtrl>();
        healthctrl = GameObject.Find("HealthBar").GetComponent<Health>();
        objectivectrl = GameObject.Find("ObjectiveCtrl").GetComponent<ObjectiveCtrl>();
        ind = objectivectrl.index;
      
    }

    void Update()
    {
        if(totalcount == count)
        {
            objectivectrl.objectives[ind].isCompleted = true;
            /*
            ObjectiveText.SetActive(true);            
           StartCoroutine(Objectivedisable(ObjectiveText));
            totalcount++;
            Objectives[0].SetActive(false);
            TaskComplete.SetActive(true);
            */
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && gameObject.tag == "Health")
        {
            count += 1;
            Debug.Log(count);
            Instantiate(HealthParticleEffect, gameObject.transform.position, Quaternion.identity);
            ShowText("+10",Color.green);
            //  Destroy(gameObject);
            DestroyImmediate(gameObject, true);
            healthctrl.Heal(10);
            Destroy(HealthParticleEffect);
            
            //Destroy(FloatingText, 1f);

        }
        else if (other.gameObject.tag == "Player" && gameObject.tag == "Stamina")
        {
            count++;
            Debug.Log(count);
            Instantiate(StaminaParticleEffect, gameObject.transform.position, Quaternion.identity);
            ShowText("+10",Color.blue);
            DestroyImmediate(gameObject, true);
            staminaCtrl.Heal(10);
            Destroy(StaminaParticleEffect);
           
            //  Destroy(FloatingText, 1f);
        }
        else if (other.gameObject.tag == "Player" && gameObject.tag == "Damage")
        {
            
            Instantiate(DamageParticleEffect, gameObject.transform.position, Quaternion.identity);
            DestroyImmediate(gameObject, true);
            ShowText("-20",Color.red);
          
           // staminaCtrl.Damage(20);
            healthctrl.Damage(20);
            Destroy(DamageParticleEffect);
            //
        }
    }

    void ShowText(string text, Color color)
    {
        Debug.Log("Floating  Text Called");
        Text prefab = Instantiate(FloatingText, transform.position, Quaternion.identity);
        prefab.transform.SetParent(canvas.transform, false);
        prefab.GetComponent<Text>().text = text;
        prefab.GetComponent<Text>().color = color;
        Destroy(FloatingText,1f);
    }

    private IEnumerator Objectivedisable(GameObject x)
    {
        yield return new WaitForSeconds(2f);
        x.SetActive(false);
        
    }


}