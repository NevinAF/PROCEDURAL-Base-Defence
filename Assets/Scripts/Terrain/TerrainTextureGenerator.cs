using System;
using UnityEngine;

public static class TerrainTextureGenerator
{
    public static Texture2D GenerateTerrainTexture(TerrainSetting terrainSetting, float[,] heightMap)
    {
        Texture2D texture = new Texture2D(heightMap.GetLength(0), heightMap.GetLength(1));
        Color[] colors = new Color[heightMap.GetLength(0) * heightMap.GetLength(1)];

        
        for (int y = 0, i = 0; y < heightMap.GetLength(1); y++)
        {
            for (int x = 0; x < heightMap.GetLength(0); x++, i++)
            {
                //float height = heightMap[x, y];
                Color color = new Color(255, 0, 255);
                //foreach(TerrainSetting.TerrainTextures terrainTexture in terrainSetting.terrainTextures)
                //{
                //    if(terrainTexture.maxHeight > height)
                //    {
                //        color = terrainTexture.color;
                 //       break;
                //    }
                //}

                colors[i] = color;
            }
        }

        texture.SetPixels(colors);
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.filterMode = FilterMode.Point;
        texture.Apply();

        return texture;
    }
}