using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;
using TMPro;

public class StationTriggers : MonoBehaviour
{
   
    public CinemachineVirtualCamera vCam;
    public AudioSource audioFile;
    public TextMeshProUGUI ActivityLog;
    public GameObject Panel;
    public GameObject metaportGameObject;

    private string _parentName;
    private TextMeshProUGUI _activityLog;

    private bool status = true;
    private void Awake()
    {
        _parentName = this.gameObject.name;
        //_activityLog = GameObject.Find("t_ActivityLog").GetComponent<TextMeshProUGUI>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && status)
        {
            Debug.Log(other.gameObject.name +" has entered collider "+ _parentName);
            string notify = "<b><color=#90ee90>" + other.gameObject.name + "</b></color> has entered collider <color=#FFCCCB>" + this.name + "</color>";

            Debug.Log(notify);
            Variables.Application.Set("PlayerActivity", notify);

            if (_activityLog !=null)
            {
                _activityLog.text += "<br>" + notify;
            }
            //ActivateLog();
            //Invoke(nameof(DeactivateLog), 5f);

            

            if (vCam !=null)
            {
                vCam.gameObject.SetActive(true);

                Debug.Log(vCam + "has been activated");
                string update = vCam + "has been activated";
                Variables.Application.Set("PlayerActivity", update);

                if (Panel != null)
                {
                    Invoke(nameof(ActivatePanel), 2f);
                }

                if(metaportGameObject != null)
                {
                    Invoke(nameof(DeactivateMetaportVisibility), 2f);
                }

                if (ActivityLog != null)
                {
                    ActivityLog.gameObject.SetActive(true);
                }
                //ActivateLog();
                //Invoke(nameof(DeactivateLog), 5f);

            }

            if (audioFile != null)
            {
                audioFile.PlayDelayed(2);
                Debug.Log("Started playing " + audioFile.name);
                //ActivateLog();
                //Invoke(nameof(DeactivateLog), 5f);
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log(other.gameObject.name + " has exited collider " + this.name);
            string notify = "<b><color=#90ee90>" + other.gameObject.name + "</b></color> has exited collider <color=#FFCCCB>" + this.name + "</color>";
            Variables.Application.Set("PlayerActivity", notify);
            if (_activityLog != null)
            {
                _activityLog.text += "<br>" + notify;
            }
            //ActivateLog();
            //Invoke(nameof(DeactivateLog), 5f);

            if (vCam !=null)
            {
                vCam.gameObject.SetActive(false);

                DeactivatePanel();
                ActivateMetaportVisibility();

                Debug.Log(vCam + "has been deactivated");
                string update = vCam + "has been deactivated";
                Variables.Application.Set("PlayerActivity", update);
                //ActivateLog();
                //Invoke(nameof(DeactivateLog), 5f);
            }

            if (audioFile != null)
            {
                audioFile.volume = 0.25f;
                Debug.Log("Stopped playing " + audioFile.name);
            }
        }
    }

    public void ActivateLog()
    {
        if (ActivityLog != null)
        {
            ActivityLog.gameObject.SetActive(true);
        }
    }

    public void DeactivateLog()
    {
        if (ActivityLog != null)
        {
            ActivityLog.gameObject.SetActive(false);
        }
    }

    public void ActivatePanel()
    {
        if (Panel != null)
        {
            Panel.SetActive(true);
        }
    }

    public void DeactivatePanel()
    {
        if (Panel != null)
        {
            Panel.SetActive(false);
        }
    }

    public void DeactivateMetaportVisibility()
    {
        if (metaportGameObject != null)
        {
            metaportGameObject.SetActive(false);
        }
    }

    public void ActivateMetaportVisibility()
    {
        if (metaportGameObject != null)
        {
            metaportGameObject.SetActive(true);
        }
    }

   public void InvertCheckStatus()
   {
   	status = !status;
        Invoke(nameof(InvertCheckStatus),4f);
   }

}

