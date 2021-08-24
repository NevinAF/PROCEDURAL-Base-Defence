using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Camera))]
public class CameraMouseRaycast : MonoBehaviour
{
    public static readonly float maxCastDistance = 5f;

    private Camera _camera;
    private GameObject hitGO;
    public GameObject HitGO
    {
        get
        {
            return hitGO;
        }

        set
        {
            hitGO = value;
            OnGOChange.Invoke(hitGO);
        }
    }

    private class HitGOEvent : UnityEvent<GameObject> { }
    private class HitDataEvent : UnityEvent<RaycastHit> { }

    private HitGOEvent OnGOChange;
    private HitDataEvent OnHitChange;
    public RaycastHit hitData;
    public Vector3 CastPosition { get; private set; }
    public Ray CastRay { get; private set; }
    public bool CastHit { get; private set; }

    public void AddOnChangeListener(UnityAction<GameObject> listener)
    {
        OnGOChange.AddListener(listener);
    }

    public void RemoveOnChangeListener(UnityAction<GameObject> listener)
    {
        OnGOChange.RemoveListener(listener);
    }

    public void AddOnChangeListener(UnityAction<RaycastHit> listener)
    {
        OnHitChange.AddListener(listener);
    }

    public void RemoveOnChangeListener(UnityAction<RaycastHit> listener)
    {
        OnHitChange.RemoveListener(listener);
    }

    // Use this for initialization
    void Awake()
    {
        OnGOChange = new HitGOEvent();
        OnHitChange = new HitDataEvent();
        HitGO = null;
        _camera = gameObject.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        CastPosition = new Vector3(Screen.width / 2, Screen.height / 2);

        CastRay = _camera.ScreenPointToRay(CastPosition);

        if (Physics.Raycast(CastRay, out hitData, maxCastDistance))
        {
            CastHit = true;
            OnHitChange.Invoke(hitData);
            if (HitGO != hitData.collider.gameObject)
            {
                HitGO = hitData.collider.gameObject;
            }
        }
        else
        {
            CastHit = false;
            if (HitGO != null)
                HitGO = null;
        }
    }

    public Vector3 GetDistancePoint(float distance)
    {
        return CastRay.GetPoint(distance);
    }

    public Vector3 GetDistancePoint()
    {
        return GetDistancePoint(maxCastDistance);
    }
}
