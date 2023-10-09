using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;


public class PlayerTransformHandler : MonoBehaviour
{
    public Transform PlayerTransform;

    [SerializeField]
    private float playerX, playerY, playerZ;

    [SerializeField]
    private Vector3 playerRot;

    [SerializeField]
    private string previousSceneToCheck;

    

    // Start is called before the first frame update
    void Start()
    {
        string previousScene = (string)Variables.Application.Get("PreviouslyLoadedScene");

        if (string.Equals(previousScene, previousSceneToCheck))
        {
            
            PlayerTransform.SetPositionAndRotation(new Vector3(playerX, playerY, playerZ), Quaternion.LookRotation(Vector3.back));
        }

        else
        {
            PlayerTransform.SetPositionAndRotation(new Vector3(PlayerTransform.position.x, PlayerTransform.position.y, PlayerTransform.position.z), Quaternion.identity);
        }

        
    }

   
}
