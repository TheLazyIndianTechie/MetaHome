using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Transform cameraTransform;

    private Transform localTransform;
    // Start is called before the first frame update
    void Start()
    {
        localTransform = GetComponent<Transform>();
        cameraTransform = Camera.main.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cameraTransform)
        {
            localTransform.LookAt(2 * localTransform.position - cameraTransform.position);
        }
    }
}
