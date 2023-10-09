using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasTrigger : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject Panel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Panel.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Panel.SetActive(false);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
