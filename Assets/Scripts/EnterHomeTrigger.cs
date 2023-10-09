using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterHomeTrigger : MonoBehaviour
{
    // [SerializeField]
    // private GameObject exteriorDoorCanvas;
    //
    // [SerializeField]
    // private TMP_Text loadingStatus;
    public static event Action<int> OnEnterHomeInterior;

    private bool _isPlayerInTrigger = false;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _isPlayerInTrigger)
        {
           // StartCoroutine(OpenDoor());
           OnEnterHomeInterior?.Invoke(103);
           SceneManager.LoadSceneAsync((int)SceneIndex.METAHOME_INTERIOR_SCENE);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isPlayerInTrigger = true;
            // exteriorDoorCanvas?.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isPlayerInTrigger = false;
            // exteriorDoorCanvas?.SetActive(false);
        }
    }

    IEnumerator OpenDoor()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync((int)SceneIndex.METAHOME_INTERIOR_SCENE);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            // loadingStatus.text = "Loading Status: " + (asyncLoad.progress * 100) + "%";
            yield return null;
        }
    }
}