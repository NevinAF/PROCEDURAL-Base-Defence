using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class TerrainChunk : MonoBehaviour
{
    public MeshFilter _meshFilter { get; private set; }
    public MeshRenderer _meshRenderer { get; private set; }
    public MeshCollider _meshCollider { get; private set; }

    private void Awake()
    {
        if ((_meshFilter = GetComponent<MeshFilter>()) == null)
            _meshFilter = gameObject.AddComponent<MeshFilter>();
        if ((_meshRenderer = GetComponent<MeshRenderer>()) == null)
            _meshRenderer = gameObject.AddComponent<MeshRenderer>();
        if ((_meshCollider = GetComponent<MeshCollider>()) == null)
            _meshCollider = gameObject.AddComponent<MeshCollider>();
    }

    public void SetMesh(Mesh mesh)
    {
        _meshFilter.sharedMesh = mesh;
        _meshCollider.sharedMesh = mesh;
    }
}
