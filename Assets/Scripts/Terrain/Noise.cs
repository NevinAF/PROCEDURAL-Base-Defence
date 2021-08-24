using System;
using UnityEngine;

public static class Noise
{
    public static float[,] GenerateHeightMap(NoiseSetting noiseSetting, int vertsPerX, int vertsPerY, Vector2 sampleCentre)
    {
        float[,] noiseMap = new float[vertsPerX, vertsPerY];

        System.Random prng = new System.Random(noiseSetting.seed);
        Vector2[] octaveOffsets = new Vector2[noiseSetting.octaves];

        float maxPossibleHeight = 0;
        float amplitude = 1;
        float frequency = 1;

        for (int i = 0; i < noiseSetting.octaves; i++)
        {
            float offsetX = prng.Next(-100000, 100000) + noiseSetting.offset.x + sampleCentre.x;
            float offsetY = prng.Next(-100000, 100000) - noiseSetting.offset.y + sampleCentre.y;
            octaveOffsets[i] = new Vector2(offsetX, offsetY);

            maxPossibleHeight += amplitude;
            amplitude *= noiseSetting.persistance;
        }

        float maxLocalNoiseHeight = 0.5f;
        float minLocalNoiseHeight = 0.5f;
        float sampleScale = noiseSetting.scale / 100f;
        for (int y = 0; y < vertsPerY; y++)
        {
            for (int x = 0; x < vertsPerX; x++)
            {
                amplitude = 1;
                frequency = 1;
                float noiseHeight = 0;

                for (int i = 0; i < noiseSetting.octaves; i++)
                {
                    float sampleX = (x + octaveOffsets[i].x) * sampleScale * frequency;
                    float sampleY = (y + octaveOffsets[i].y) * sampleScale * frequency;
                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY);
                    noiseHeight += perlinValue * amplitude;

                    amplitude *= noiseSetting.persistance;
                    frequency *= noiseSetting.lacunarity;
                }

                if (noiseHeight > maxLocalNoiseHeight)
                {
                    maxLocalNoiseHeight = noiseHeight;
                }
                if (noiseHeight < minLocalNoiseHeight)
                {
                    minLocalNoiseHeight = noiseHeight;
                }

                noiseMap[x, y] = noiseHeight;
                
                if (true)
                {
                    //float normalizedHeight = noiseMap[x, y] / maxPossibleHeight;
                    //noiseMap[x, y] = Mathf.Clamp(normalizedHeight, 0, int.MaxValue);
                    noiseMap[x, y] =
                        Mathf.InverseLerp(0, maxPossibleHeight, noiseMap[x, y]);
                } /*else
                {
                    noiseMap[x, y] = 
                        Mathf.InverseLerp(minLocalNoiseHeight, maxLocalNoiseHeight, noiseMap[x, y]);
                }*/
            }
        }
        return noiseMap;
    }
}