using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class Controls : ScriptableObject
{
    public string Name;

    public KeyCode StrafeLeft;
    public KeyCode StrafeRight;
    public KeyCode WalkForwards;
    public KeyCode WalkBackwords;
    public KeyCode Jump;
    public KeyCode Sprint;
    public KeyCode Inventory;
    public KeyCode ChangeMode;

    //BuildControls
    public KeyCode SnapBuild;
    public KeyCode TogglePreviewBuild;

    //TerrainManipulation
    public KeyCode FlatenTerrain;
    
}
