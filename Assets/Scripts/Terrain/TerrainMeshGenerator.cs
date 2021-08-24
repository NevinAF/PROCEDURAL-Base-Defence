using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TerrainMeshGenerator
{
    public static MeshData GenerateTerrainMesh(TerrainSetting terrainSetting, out float[,] heightMap, Vector2Int gridOffset )
    {
        int vertsPerX = terrainSetting.chunkSize.x + 1;
        int vertsPerY = terrainSetting.chunkSize.y + 1;
        MeshData meshData = new MeshData(vertsPerX, vertsPerY);
        heightMap = 
            Noise.GenerateHeightMap(
                terrainSetting.noiseSetting,
                vertsPerX,
                vertsPerY,
                new Vector2 (gridOffset.x * (float)terrainSetting.chunkSize.x, gridOffset.y * (float)terrainSetting.chunkSize.y)
                );

        float localMaxHeight = 0;
        float localMinHeight = 0;
        for (int z = 0, i = 0; z < vertsPerY; z++)
        {
            for (int x = 0; x < vertsPerX; x++, i++)
            {
                float height =
                           heightMap[x, z] *
                           terrainSetting.heightScale *
                           terrainSetting.animationCurve.Evaluate(heightMap[x, z]);
                if (height > localMaxHeight)
                    localMaxHeight = height;
                else if (height < localMinHeight)
                    localMinHeight = height;


                meshData.AddVertex(
                    worldPosition: new Vector3(
                        x: x,
                        y: height,
                        z: z),
                    index: i);
            }
        }
        terrainSetting.TryMaxAndMin(localMinHeight, localMaxHeight);

        for (int z = 0, i = 0; z < vertsPerY - 1; z++)
        {
            for (int x = 0; x < vertsPerX - 1; x++, i++)
            {
                int a = x + 0 + ((z + 0) * vertsPerX);
                int b = x + 1 + ((z + 0) * vertsPerX);
                int c = x + 0 + ((z + 1) * vertsPerX);
                int d = x + 1 + ((z + 1) * vertsPerX);
                meshData.AddTriangle(a, c, b);
                meshData.AddTriangle(b, c, d);
            }
        }

        return meshData;
    }
}

public class MeshData
{
    int width;
    int height;
    Vector3[] vertices;
    Vector2[] uvs;
    int[] triangles;
    int triangleIndex;

    public MeshData(int width, int height)
    {
        this.width = width;
        this.height = height;
        vertices = new Vector3[width * height];
        uvs = new Vector2[width * height];
        triangles = new int[(width - 1) * (height - 1) * 6];
    }

    public void AddVertex(Vector3 worldPosition, int index)
    {
        vertices[index] = worldPosition;
        uvs[index] = new Vector2(worldPosition.x / (float)width, worldPosition.z / (float)height);
    }

    public void AddTriangle(int a, int b, int c)
    {
        triangles[triangleIndex + 0] = a;
        triangles[triangleIndex + 1] = b;
        triangles[triangleIndex + 2] = c;
        triangleIndex += 3;
    }
    
    public Mesh CreateMesh()
    {
        if(true)
        {
            FlatShading();
        }

        Mesh mesh = new Mesh
        {
            vertices = vertices,
            uv = uvs,
            triangles = triangles
        };

        mesh.RecalculateNormals();

        return mesh;
    }

    void FlatShading()
    {
        Vector3[] flatShadedVertices = new Vector3[triangles.Length];
        Vector2[] flatShadedUvs = new Vector2[triangles.Length];

        for (int i = 0; i < triangles.Length; i++)
        {
            flatShadedVertices[i] = vertices[triangles[i]];
            flatShadedUvs[i] = uvs[triangles[i]];
            triangles[i] = i;
        }

        vertices = flatShadedVertices;
        uvs = flatShadedUvs;
    }
}