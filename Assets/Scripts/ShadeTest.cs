using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadeTest : MonoBehaviour
{
    public PixelScript pixel;
    public ShadesManager shadeManager;

    int shadeNo;
    int currentShade = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        shadeNo = shadeManager.GetShadeNo();

        StartCoroutine(RunTest());
    }

    IEnumerator RunTest()
    {
        while(true)
        {
            pixel.SetShade(shadeManager.GetShade(currentShade));
            currentShade++;
            currentShade %= shadeNo;
            yield return new WaitForSeconds(1);
        }
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
