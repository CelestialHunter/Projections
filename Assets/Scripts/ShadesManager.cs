using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShadesManager", menuName = "ScriptableObjects/ShadesManager", order = 1)]
public class ShadesManager : ScriptableObject
{
    public Material[] shades;

    public Material GetShade(int shade)
    {
        return shades[Mathf.Clamp(shade, 0, shades.Length - 1)];
    }

    public int GetShadeNo()
    {
        return shades.Length;
    }

    public void InitShades()
    {
        for (int i = 0; i < shades.Length; i++)
        {
            int computedShade = i * 255 / GetShadeNo();
            shades[i].SetColor(Shader.PropertyToID("_Color"), new Color(computedShade, computedShade, computedShade));
        }
    }
}
