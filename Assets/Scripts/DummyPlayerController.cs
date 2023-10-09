using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyPlayerController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player has entered the Dummy Player vicinity");
            this.GetComponentInParent<Animator>().SetTrigger("WavingOn");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player has exited the Dummy Player vicinity");
            this.GetComponentInParent<Animator>().SetTrigger("WavingOff");
        }
    }
}
