using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorusGenerator : IFrameGenerator
{
    [SerializeField, Range(.01f, 50f)]
    float R1 = 1f;

    [SerializeField, Range(.01f, 50f)]
    float R2 = 2f;

    public override int[,] GenerateFrame()
    {
        ClearFrame();
        for (float theta = 0; theta < TAU; theta += theta_spacing)
        {
            float cosTheta = Mathf.Cos(theta);
            float sinTheta = Mathf.Sin(theta);

            for (float phi = 0; phi < TAU; phi += phi_spacing)
            {
                float cosPhi = Mathf.Cos(phi);
                float sinPhi = Mathf.Sin(phi);

                float circleX = R2 + R1 * cosTheta;
                float circleY = R1 * sinTheta;

                float x = circleX * cosPhi;
                float y = -circleY;
                float z = K2 + circleX * sinPhi;

                float ooz = 1 / z;

                int xp = (int)(screenWidth / 2 + K1 * ooz * x);
                int yp = (int)(screenHeight / 2 + K1 * ooz * y);

                // normal x = cosTheta * cosPhi
                // normal y = sinTheta
                // normal z = cosTheta * sinPhi

                // light direction = (0, 1, -1)

                // luminance = dot product between normal and light direction               
                float L = sinTheta - cosTheta * sinPhi;

                if (L > 0 && xp >= 0 && xp < screenWidth && yp >= 0 && yp < screenHeight)
                {
                    if (ooz > zBuffer[xp, yp])
                    {
                        zBuffer[xp, yp] = ooz;
                        frame[xp, yp] = (int)(L * shadeNo);
                    }
                }
            }
        }

        return frame;
    }

    public override int[,] GenerateFrame(float A, float B)
    {
        ClearFrame();
        float cosA = Mathf.Cos(A);
        float sinA = Mathf.Sin(A);

        float cosB = Mathf.Cos(B);
        float sinB = Mathf.Sin(B);

        for(float theta = 0; theta < TAU; theta += theta_spacing)
        {
            float cosTheta = Mathf.Cos(theta);
            float sinTheta = Mathf.Sin(theta);

            for (float phi = 0; phi < TAU; phi += phi_spacing)
            {
                float cosPhi = Mathf.Cos(phi);
                float sinPhi = Mathf.Sin(phi);

                float circleX = R2 + R1 * cosTheta;
                float circleY = R1 * sinTheta;

                float x = circleX * (cosB * cosPhi + sinA * sinB * sinPhi) - circleY * cosA * sinB;
                float y = circleX * (sinB * cosPhi - sinA * cosB * sinPhi) + circleY * cosA * cosB;
                float z = K2 + cosA * circleX * sinPhi + circleY * sinA;

                float ooz = 1 / z;

                int xp = (int)(screenWidth / 2 + K1 * ooz * x);
                int yp = (int)(screenHeight / 2 - K1 * ooz * y);

                float L = cosPhi * cosTheta * sinB - cosA * cosTheta * sinPhi - sinA * sinTheta + cosB * (cosA * sinTheta - cosTheta * sinA * sinPhi);

                if (L > 0 && xp >= 0 && xp < screenWidth && yp >= 0 && yp < screenHeight)
                {
                    if (ooz > zBuffer[xp, yp])
                    {
                        zBuffer[xp, yp] = ooz;
                        frame[xp, yp] = (int)(L * shadeNo);
                    }
                }
            }
        }

        return frame;
    }
}
