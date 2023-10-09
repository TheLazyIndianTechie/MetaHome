using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;


public class EnterHomeTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject doorOpenPanel;

    [SerializeField]
    private GameObject slidingDoor;

    private Animator _anim;

    //public Transform playerTransform;

    public string sceneToLoad;

    //[SerializeField]
    //private float playerXTransform, playerYTransform, playerZTransform;

    private void Awake()
    {
        if (slidingDoor != null)
        {
            _anim = slidingDoor.GetComponent<Animator>();

        }
    

    }


    private bool isPlayerActive = false;

    private void Start()
    {
        if (doorOpenPanel != null)
        {
            doorOpenPanel.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (doorOpenPanel != null)
            {
                doorOpenPanel.SetActive(false);
            }

            if (!isPlayerActive)
            {
                Debug.Log("Player is not ready to enter");
            }

            else
            {
                StartCoroutine(OpenDoor());
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log(other.name + " has entered collider ");
            isPlayerActive = true;

            string s = SceneManager.GetActiveScene().name;

            Debug.Log("Previous Scene: " + s);

            Variables.Application.Set("PreviouslyLoadedScene", s);
            Debug.Log("Scene set to: " + Variables.Application.Get("PreviouslyLoadedScene"));


            if (doorOpenPanel != null)
            {
                doorOpenPanel.SetActive(true);
            }

            
        }
    }


    IEnumerator OpenDoor()
    {
        
        if (sceneToLoad != null)
        {

            if (_anim != null)
            {
                _anim.SetTrigger("OpenDoor");
            }

            yield return new WaitForSecondsRealtime(2f);

            SceneManager.LoadSceneAsync(sceneToLoad);
            //playerTransform.SetPositionAndRotation(new Vector3(playerXTransform, playerYTransform, playerZTransform), Quaternion.LookRotation(Vector3.forward));

            yield return null;
                
        }
        
    }

}
