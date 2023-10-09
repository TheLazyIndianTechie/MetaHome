using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeNFT : MonoBehaviour
{

    public Material[] material;
    public int Number;
    Renderer rend;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSelect(int Number)
    {
        rend.sharedMaterial = material[Number];
    }

}
