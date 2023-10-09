using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarTrigger : MonoBehaviour
{
    public GameObject carUI;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            carUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            carUI.SetActive(false);
        }
    }

}
