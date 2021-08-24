using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildModeHandeler : ModeHandeler
{
    
    public BuildObject selectedBuildObject;

    public BoolAttribute previewBuild;
    public float snapRange = 4.0f;
    public BoolAttribute snapToggle;
    public BoolAttribute snapEnabled;

    private GameObject buildOutlineGO;
    private bool buildAble;
    private Vector3 buildPosition;

    public override InputMode Mode
    {
        get
        {
            return InputMode.Build;
        }
    }

    // Use this for initialization
    void Start ()
    {
        previewBuild.Start();
        snapEnabled.Start();
        snapToggle.Start();
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        DestroyBuildOutlineGO();
    }

    private void UpdateBuild(bool mouseDown = false)
    {
        if (_cameraMouseRaycast.CastHit)
        {
            buildPosition = _cameraMouseRaycast.hitData.point;
            buildAble = true;
        }
        else
        {
            buildPosition = _cameraMouseRaycast.GetDistancePoint();
            buildAble = false;
        }

        if (snapEnabled)
        {
            BuildObject buildObject = Functions.GetClosestObject<BuildObject>(buildPosition, snapRange);
            if (buildObject != null)
            {
                buildPosition =
                    buildObject.GetBuildPlacementPosition(
                        selectedBuildObject,
                        buildPosition
                        );
                buildAble = true;
            }
        }

        if (mouseDown && buildAble)
        {
            DestroyBuildOutlineGO();

            GameObject build = Instantiate(selectedBuildObject.gameObject);
            build.transform.position = buildPosition;
        }
        else if (previewBuild)
        {
            if (buildOutlineGO == null)
            {
                buildOutlineGO = selectedBuildObject.GetCopyOfGraphics();
                buildOutlineGO.GetComponent<Renderer>().material = selectedBuildObject.placingMat;

            }
            buildOutlineGO.transform.position = buildPosition;

            if (buildAble)
            {
                buildOutlineGO.GetComponent<Renderer>().material.color = Color.white;
            }
            else
            {
                buildOutlineGO.GetComponent<Renderer>().material.color = Color.red;
            }
        }
        else
        {
            DestroyBuildOutlineGO();
        }
    }

    private void DestroyBuildOutlineGO()
    {
        if (buildOutlineGO != null)
        {
            GameObject.Destroy(buildOutlineGO.gameObject);
            buildOutlineGO = null;
        }
    }

    // Update is called once per frame
    void Update ()
    {
        if (Input.GetKeyDown(controls.TogglePreviewBuild))
        {
            DestroyBuildOutlineGO();
            previewBuild.SetValue(!previewBuild);
        }

        if(snapToggle && Input.GetKeyDown(controls.SnapBuild))
        {
            snapEnabled.SetValue(!snapEnabled);
        } else
        {
            if (Input.GetKey(controls.SnapBuild))
                snapEnabled.SetValue(true);
            else
                snapEnabled.SetValue(false);
        }

        if (selectedBuildObject != null)
            UpdateBuild(Input.GetMouseButtonDown(0));
    }
}
