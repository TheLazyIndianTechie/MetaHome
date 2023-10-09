using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetaportTrigger : MonoBehaviour
{
    [SerializeField] private GameObject dialogueCanvas, metaportCanvas;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            dialogueCanvas?.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            dialogueCanvas?.SetActive(false);
            metaportCanvas?.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (dialogueCanvas.activeInHierarchy)
        {
            if (Input.GetKey(KeyCode.Y))
            {
                //Deactivate Dialogue box and Open the requisite canvas
                dialogueCanvas?.SetActive(false);
                metaportCanvas?.SetActive(true);
            }

            if (Input.GetKey(KeyCode.N))
            {
                //Deactivate Dialogue box
                dialogueCanvas?.SetActive(false);
            }
        }

        else if (!dialogueCanvas.activeInHierarchy)
        {
            if (Input.GetKey(KeyCode.F))
            {
                dialogueCanvas?.SetActive(true);
            }
        }

        if (metaportCanvas.activeInHierarchy)
        {
            if (Input.GetKey(KeyCode.X))
            {
                metaportCanvas?.SetActive(false);
            }
        }
    }
}
