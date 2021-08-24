using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public TerrainSetting TerrainSetting;
    public Vector2Int numberOfChunks;

    public static TerrainGenerator instance;
	// Use this for initialization
	void Awake ()
    {
        instance = this;
        TerrainSetting.MinMeshHeight = 0;
        TerrainSetting.MaxMeshHeight = 0;
        for (int x = 0; x < numberOfChunks.x; x++)
        {
            for (int y = 0; y < numberOfChunks.y; y++)
            {
                CreateChunk(x,y);
            }
        }

        int MapX = numberOfChunks.x * TerrainSetting.chunkSize.x;
        int MapY = numberOfChunks.y * TerrainSetting.chunkSize.y;
        transform.position = new Vector3(-MapX / 2f, 0, -MapY / 2f);
    }

    

    // Update is called once per frame
    void Update () {
		
	}

    private void CreateChunk(int gridX = 0, int gridY = 0)
    {
        CreateChunk(new Vector2Int(gridX, gridY));
    }

    private void CreateChunk(Vector2Int gridPosition)
    {
        float[,] heightMap;
        Mesh mesh = TerrainMeshGenerator.GenerateTerrainMesh(TerrainSetting, out heightMap, gridPosition).CreateMesh();
        GameObject chunk_go = new GameObject();
        
        chunk_go.transform.parent = transform;
        chunk_go.transform.position = new Vector3(gridPosition.x * TerrainSetting.chunkSize.x, 0, gridPosition.y * TerrainSetting.chunkSize.y);

        TerrainChunk chunk_data = chunk_go.AddComponent<TerrainChunk>();

        chunk_data.SetMesh(mesh);

        Material material = chunk_go.GetComponent<MeshRenderer>().sharedMaterial;
        TerrainSetting.UpdateMeshHeights(material);
        TerrainSetting.terrainTextures.ApplyToMaterial(material);
    }

    public float GetHighestPoint(float x, float z)
    {
        RaycastHit hit;
        if (!Physics.Raycast(
                new Vector3(x, TerrainSetting.MaxMeshHeight + 3, z),
                Vector3.down,
                out hit))
            throw new Exception("TerrrainGenerator.GetHighestPoint() -- raycat did not hit");
        return hit.point.y;
    }
}
