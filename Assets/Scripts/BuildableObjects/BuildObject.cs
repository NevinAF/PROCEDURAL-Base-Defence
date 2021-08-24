using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuildObject : MonoBehaviour
{
    private int cursorTrianglePosition = -1;
    public abstract float Width { get; }
    public abstract float Depth { get; }
    public abstract float Height { get; }
    public GameObject Graphics;
    public Material placingMat;

    public int CursorTrianglePosition
    {
        get
        {
            return cursorTrianglePosition;
        }

        set
        {
            cursorTrianglePosition = value;
            if (value >= 0)
                OnCursorEnter();
            else
                OnCursorExit();
        }
    }

    void Update()
    {
        if (CursorTrianglePosition >= 0)
        {
            OnCursorStay();
        }
    }

    public void BreakSelf()
    {
        GameObject.Destroy(gameObject);
    }

    public abstract void OnCursorEnter();
    public abstract void OnCursorExit();
    public abstract void OnCursorStay();
    
    public abstract Vector3 GetBuildPlacementPosition(BuildObject buildObject, Vector3 position);

    public GameObject GetCopyOfGraphics()
    {
        GameObject go = Instantiate(Graphics);
        go.transform.localScale = Vector3.Scale(this.transform.localScale, go.transform.localScale);
        return go;
    }
}
