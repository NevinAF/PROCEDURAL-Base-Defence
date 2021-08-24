using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainModeHandeler : ModeHandeler
{
    public float selectRadius = 3.2f;
    public BoolAttribute flatenMode;
    public BoolAttribute toggleFlatenMode;

    private void Awake()
    {

    }
    // Use this for initialization
    void Start ()
    {
        flatenMode.Start();
        toggleFlatenMode.Start();
	}

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    // Update is called once per frame
    void Update ()
    {
        if (toggleFlatenMode && Input.GetKeyDown(controls.SnapBuild))
        {
            flatenMode.SetValue(!flatenMode);
        }
        else
        {
            if (Input.GetKey(controls.SnapBuild))
                flatenMode.SetValue(true);
            else
                flatenMode.SetValue(false);
        }

        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            if (MainHandeler.Moused_go != null)
            {
                if (MainHandeler.Moused_go.GetComponent<MeshFilter>() != null)
                {
                    UserModifyTerrain(Input.GetMouseButtonDown(0));
                }
            }
        }
    }

    private Vector3 hitPoint;
    private TerrainChunk[] chunksInRadius;
    private Mesh mesh;
    private Vector3[] verts;

    public override InputMode Mode
    {
        get
        {
            return InputMode.TerrainManipulation;
        }
    }

    private void UserModifyTerrain(bool isPrimaryMouse)
    {
        
        hitPoint = _cameraMouseRaycast.hitData.point;

        chunksInRadius = Functions.GetObjectsInRadius<TerrainChunk>(hitPoint, selectRadius);

        foreach (TerrainChunk chunk in chunksInRadius)
        {
            mesh = chunk._meshFilter.sharedMesh;
            verts = mesh.vertices;

            for (int i = 0; i < verts.Length; i++)
            {
                Vector3 vertexWorldPosition = verts[i] + chunk.transform.position;
                float distance = Vector3.Distance(vertexWorldPosition, hitPoint);
                if (distance < selectRadius)
                {

                    // ----------  Flaten mode
                    if (flatenMode)
                    {

                        verts[i] = new Vector3(verts[i].x, hitPoint.y, verts[i].z);
                    }
                    // ---------- Dig/fill mode
                    else if (distance < selectRadius)
                    {
                        verts[i] += Vector3.up * ((isPrimaryMouse) ? 0.4f : -0.4f);
                    }


                }
            }

            mesh.vertices = verts;
            chunk.SetMesh(mesh);
        }
    }


}
