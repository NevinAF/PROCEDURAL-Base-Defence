using System.Collections;
using UnityEngine;
using System;
using UnityEngine.Events;
using System.Collections.Generic;

public abstract class ModeHandeler
{
    public Player _player { get; private set; }
    public abstract InputMode Mode { get; }
    protected BaseDefenceInputHandeler MainHandeler;
    protected Controls controls;
    protected CameraMouseRaycast _cameraMouseRaycast;

    public ModeHandeler(BaseDefenceInputHandeler mainHandeler)
    {
        this.MainHandeler = mainHandeler;

        controls = MainHandeler.controls;
        _cameraMouseRaycast = MainHandeler._cameraMouseRaycast;
        _player = MainHandeler._player;
    }

    

}
