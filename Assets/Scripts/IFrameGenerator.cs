using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IFrameGenerator : MonoBehaviour
{

    internal const float TAU = Mathf.PI * 2f;

    internal int screenWidth;
    internal int screenHeight;

    internal int shadeNo = 8;

    internal int[,] frame;
    internal float[,] zBuffer;

    [SerializeField, Range(0.01f, 1f)]
    internal float theta_spacing = 0.07f;

    [SerializeField, Range(0.01f, 1f)]
    internal float phi_spacing = 0.02f;

    [SerializeField, Range(.01f, 100f)]
    internal float K2 = 5f;

    [SerializeField, Range(.01f, 100f)]
    internal float K1 = 1f;

    public virtual void InitFrame(int screenWidth, int screenHeight, int shadeNo)
    {
        this.screenWidth = screenWidth;
        this.screenHeight = screenHeight;
        this.shadeNo = shadeNo-1;

        frame = new int[screenWidth, screenHeight];
        zBuffer = new float[screenWidth, screenHeight];

        ClearFrame();
    }
    
    internal void ClearFrame()
    {
        for (int i = 0; i < screenWidth; i++)
        {
            for (int j = 0; j < screenHeight; j++)
            {
                frame[i, j] = 0;
                zBuffer[i, j] = 0;
            }
        }
    }
    
    public abstract int[,] GenerateFrame();

    public abstract int[,] GenerateFrame(float A, float B);
}
