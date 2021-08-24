using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BuildModeHandeler))]
[RequireComponent(typeof(TerrainModeHandeler))]
public class BaseDefenceInputHandeler : MonoBehaviour
{
    public Player _player;
    public Controls controls;
    public CameraMouseRaycast _cameraMouseRaycast;
    public PlayerMovement _playerMovement;
    
    private ModeHandeler[] handelers;
   

    public GameObject Moused_go { get; private set; }

    // Use this for initialization
    void Awake()
    {
        if (controls == null)
        {
            //TODO: Set controls to default if not set
            throw new System.NotImplementedException();
        }
        else
        {
            //TODO: Make Controls updateable and DONT FORGET TO UPDATE MOVEMENT
            _playerMovement.SetControls(
                controls.StrafeLeft,
                controls.StrafeRight,
                controls.WalkForwards,
                controls.WalkBackwords,
                controls.Jump,
                controls.Sprint);
        }

        handelers = new ModeHandeler[]
        {
            new BuildModeHandeler(),
            new TerrainModeHandeler(),
            new WeaponModeHandler()
        };
        foreach(ModeHandeler handeler in )
        {
            handeler.SetHandeler(this);
        }
    }

    private void Start()
    {
        _cameraMouseRaycast.AddOnChangeListener(
            
            );
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(controls.ChangeMode))
        {
            CycleInputMode();
        }
    }

    public void SetInputMode(int inputMode)
    {
        int temp = this.inputMode;
        this.inputMode.SetValue(inputMode);

        handelers[inputMode].enabled = true;
        handelers[temp].enabled = false;
    }

    public void CycleInputMode()
    {
        SetInputMode( (inputMode == (int) InputMode.Build) ? 0 : inputMode + 1 );
    }
}
