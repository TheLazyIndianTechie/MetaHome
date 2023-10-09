using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;
using TMPro;
using System.Runtime.InteropServices;
using Mirror;

public class StationTriggersMultiplayer : NetworkBehaviour
{
    [DllImport("__Internal")]
    private static extern void leaderBoardToggle();

    public CinemachineVirtualCamera vCam;
    public AudioSource audioFile;
    public GameObject UIPanel;
    public GameObject PopUp;
  //  public TextMeshProUGUI ActivityLog;
    public bool isLeaderboardTrigger = false;

    private bool localCheck = false;

    public LeaderBoardManager lm;

    [SerializeField]
    private AudioSource bgMusic;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collider has been activated ");

        localCheck = other.GetComponent<NetworkIdentity>().isLocalPlayer;
        if (localCheck)
        {



            if (other.CompareTag("Player"))
            {
                Debug.Log(other.gameObject.name + " has entered collider " + this.name);
                string notify = other.gameObject.name + " has entered collider " + this.name;
                Variables.Application.Set("PlayerActivity", notify);
                ActivateLog();
                Invoke(nameof(DeactivateLog), 5f);

                bgMusic.Pause();

                if (vCam != null)
                {
                    if (isLeaderboardTrigger)
                    {
                        lm.ResetGui();
                        leaderBoardToggle();
                    }

                    vCam.gameObject.SetActive(true);

                    StartCoroutine(Panel(true));
                    PanelActive(true);

                    Debug.Log(vCam + "has been activated");
                    string update = vCam + "has been activated";
                    Variables.Application.Set("PlayerActivity", update);
                   // ActivityLog.gameObject.SetActive(true);
                    ActivateLog();
                    Invoke(nameof(DeactivateLog), 5f);

                }

                if (audioFile != null)
                {
                    audioFile.PlayDelayed(2);
                    Debug.Log("Started playing " + audioFile.name);
                    ActivateLog();
                    Invoke(nameof(DeactivateLog), 5f);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        localCheck = other.GetComponent<NetworkIdentity>().isLocalPlayer;
        if (localCheck)
        {

            if (other.CompareTag("Player"))
            {
                Debug.Log(other.gameObject.name + " has exited collider " + this.name);
                string notify = other.gameObject.name + " has exited collider " + this.name;
                Variables.Application.Set("PlayerActivity", notify);
                ActivateLog();
                Invoke(nameof(DeactivateLog), 5f);

                bgMusic.Play();

                if (vCam != null)
                {
                    if (isLeaderboardTrigger)
                    {
                        lm.ResetGui();
                        leaderBoardToggle();
                    }
                    vCam.gameObject.SetActive(false);

                    UIPanel.SetActive(false);
                    PanelActive(false);


                    Debug.Log(vCam + "has been deactivated");
                    string update = vCam + "has been deactivated";
                    Variables.Application.Set("PlayerActivity", update);
                    ActivateLog();
                    Invoke(nameof(DeactivateLog), 5f);
                }

                if (audioFile != null)
                {
                    audioFile.volume = 0.25f;
                    Debug.Log("Stopped playing " + audioFile.name);
                }
            }
        }
    }

    public void ActivateLog()
    {
      //  ActivityLog.gameObject.SetActive(true);
    }

    public void DeactivateLog()
    {
       // ActivityLog.gameObject.SetActive(false);
    }

    public void PanelActive(bool isActive)
    {
        if (isActive)
        {
            if (Input.GetKey("e"))
            {
                PopUp.SetActive(true);
            }

            if (Input.GetKey("f"))
            {
                PopUp.SetActive(false);
            }
        }
    }


    IEnumerator Panel(bool isActive)
    {
        if (isActive)
        {
            yield return new WaitForSeconds(2.0f);
            UIPanel.SetActive(true);
        }
        else
        {
            UIPanel.SetActive(false);
        }
        
        
    }
}

