using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class LeaderboardTrigger : MonoBehaviour
{
    public CinemachineVirtualCamera vCam2;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")){

            Debug.Log("Player has entered the collider");

            vCam2.gameObject.SetActive(true);
            Debug.Log(vCam2 + " has been activated successfully");

            if (Input.GetKey(KeyCode.Slash))
            {
                vCam2.gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            Debug.Log("Player has exited the collider");

            vCam2.gameObject.SetActive(false);
            Debug.Log(vCam2 + " has been deactivated successfully");
        }
    }

}
