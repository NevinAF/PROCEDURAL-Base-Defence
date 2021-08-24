using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PointLook))]
[RequireComponent(typeof(TeamLayer))]
public class TargetObject : MonoBehaviour
{
    public TargetPriority[] targetOrder;
    public Transform DefaultTarget;
    public float SightRange;
    private PointLook _pointLook;
    private AIMovement _AIMovement;
    private TeamLayer _teamLayer;
    private Transform currentTarget;

    // Use this for initialization
    void Start ()
    {
        currentTarget = null;
        _pointLook = GetComponent<PointLook>();
        _AIMovement = GetComponent<AIMovement>();
        _teamLayer = GetComponent<TeamLayer>();

        SetTarget(null);
	}

    private void SetTarget(Transform target)
    {
        _pointLook.target = target;

        if (_AIMovement != null)
        {
            _AIMovement.targetInfo = new AIMovement.TargetMovementInfo(target, SightRange);
        }

        currentTarget = target;
    }

    // Update is called once per frame
    void Update ()
    {
        Transform t = null;
		foreach(TargetPriority priority in targetOrder)
        {
            switch(priority)
            {
                case TargetPriority.Default:
                    t = DefaultTarget;
                    break;
                case TargetPriority.CloseEnemy:
                    //t = Functions.GetClosestObject<Movement>(transform.position, SightRange);
                    break;
                case TargetPriority.FarEnemy:
                    //t = Functions.GetFurthestObject<Transform>(transform.position, SightRange);
                    break;
            }
            if (t != null)
                break;
        }

        if(t != currentTarget)
        {
            SetTarget(t);
        }
	}



    [System.Serializable]
    public enum TargetPriority
    {
        Default,
        CloseTower,
        FarTower,
        CloseEnemy,
        FarEnemy,

        //LHealth,
        //HHealth,
    }
}
