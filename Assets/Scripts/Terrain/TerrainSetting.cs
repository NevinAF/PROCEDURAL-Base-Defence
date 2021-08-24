using System;
using UnityEngine;

[CreateAssetMenu()]
public class TerrainSetting : ScriptableObject
{
    public NoiseSetting noiseSetting;
    public Vector2Int chunkSize;
    public TextureData terrainTextures;
    public AnimationCurve animationCurve;
    public float heightScale;
    private float _maxMeshHeight = 0;
    private float _minMeshHeight = 0;

    public float MaxMeshHeight
    {
        get
        {
            return _maxMeshHeight;
        }
        set
        {
            _maxMeshHeight = value;
        }
    }

    public float MinMeshHeight
    {
        get
        {
            return _minMeshHeight;
        }
        set
        {
            _minMeshHeight = value;
        }
    }

    public void UpdateMeshHeights(Material material)
    {
        this.terrainTextures.UpdateMeshHeights(material, MinMeshHeight, MaxMeshHeight);
    }

    internal void TryMaxAndMin(float localMinHeight, float localMaxHeight)
    {
        if (localMinHeight > MinMeshHeight)
            MinMeshHeight = localMinHeight;
        if (localMaxHeight > MaxMeshHeight)
            MaxMeshHeight = localMaxHeight;
    }
}