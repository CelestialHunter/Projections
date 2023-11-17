using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelScript : MonoBehaviour
{

    [SerializeField]
    private int shade;
    
    
    void Start()
    {
    }
    public void SetShade(Material shade)
    {
        GetComponent<Renderer>().material = shade;
    }
}
