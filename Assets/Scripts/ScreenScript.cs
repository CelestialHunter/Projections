using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class ScreenScript : MonoBehaviour
{
    public ShadesManager shadeManager;

    public int screenWidth;
    public int screenHeight;
    
    public GameObject pixelPrefab;

    public PixelScript[,] pixelMatrix;

    [SerializeField]
    IFrameGenerator frameGenerator;

    [SerializeField, Range(.01f, 1f)]
    float simulationDelay = 0.1f;

    [SerializeField]
    float A;

    [SerializeField]
    float B;

    [SerializeField]
    float aStep = 0.04f;

    [SerializeField]
    float bStep = 0.02f;

    void Start()
    {
        shadeManager.InitShades();        

        pixelMatrix = new PixelScript[screenWidth, screenHeight];
        GenerateScreen();

        if (frameGenerator != null)
        {
            frameGenerator.InitFrame(screenWidth, screenHeight, shadeManager.GetShadeNo());
            SetFrame(frameGenerator.GenerateFrame());
        }

        StartCoroutine(RunScreen());
    }

    private void Update()
    {
        
    }

    IEnumerator RunScreen()
    {
        while(true)
        {            
            yield return new WaitForSeconds(simulationDelay);
            SetFrame(frameGenerator.GenerateFrame(A, B));
            A += aStep;
            B += bStep;
        }
    }

    public void GenerateScreen()
    {
        for (int x = 0; x < screenWidth; x++)
        {
            for (int y = 0; y < screenHeight; y++)
            {
                GameObject pixel = Instantiate(pixelPrefab, new Vector3(x, y, 0), Quaternion.identity);
                pixel.transform.parent = transform;
                pixelMatrix[x, y] = pixel.GetComponent<PixelScript>();
            }
        }
    }

    public void SetFrame(int[,] frame)
    {
        for (int x = 0; x < screenWidth; x++)
        {
            for (int y = 0; y < screenHeight; y++)
            {
                pixelMatrix[x, y].SetShade(shadeManager.GetShade(frame[x,y]));
            }
        }
    }

    //public void VomitFrameToLog()
    //{
    //    StringBuilder sb = new StringBuilder();
    //    for(int i = 0; i < screenHeight; i++)
    //    {
    //        for (int j = 0; j < screenWidth; j++)
    //        {
    //            sb.Append(pixelMatrix[j, i].GetShade());
    //            sb.Append(' ');
    //        }
    //        sb.AppendLine();
    //    }

    //    Debug.Log(sb.ToString());
    //}
}
