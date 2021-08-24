using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Functions
{
    public static T[] GetObjectsInRadius<T>(Vector3 position, float radius)
    {
        Collider[] colliders = Physics.OverlapSphere(position, radius);
        List<T> typesInRadius = new List<T>();

        foreach (Collider collider in colliders)
        {
            if (collider.GetComponent<T>() != null)
            {
                typesInRadius.Add(collider.GetComponent<T>());
            }
        }

        return typesInRadius.ToArray();
    }

    public static bool False()
    {
        return false;
    }

    public static bool True()
    {
        return true;
    }

    public static Queue<T> MakeQueue<T>(T[] array)
    {
        Queue<T> queue = new Queue<T>();
        for (int i = 0; i < array.Length; i++)
        {
            queue.Enqueue(array[i]);
        }

        return queue;
    }

    public static T GetClosestObject<T>(Vector3 position, float radius)
    {
        Collider[] colliders = Physics.OverlapSphere(position, radius);

        float smallestDistance = radius;
        T objAtSmallestDistance = default(T);

        foreach (Collider collider in colliders)
        {
            if (collider.GetComponent<T>() != null)
            {
                Vector3 CPOB = collider.ClosestPointOnBounds(position);
                float distance = Vector3.Distance(CPOB, position);
                if (distance < smallestDistance)
                {
                    smallestDistance = distance;
                    objAtSmallestDistance = collider.GetComponent<T>();
                }
            }
        }

        return objAtSmallestDistance;
    }

    public static T GetFurthestObject<T>(Vector3 position, float radius)
    {
        Collider[] colliders = Physics.OverlapSphere(position, radius);

        float furthestDistance = radius;
        T objAtFurthestDistance = default(T);

        foreach (Collider collider in colliders)
        {
            if (collider.GetComponent<T>() != null)
            {
                Vector3 CPOB = collider.ClosestPointOnBounds(position);
                float distance = Vector3.Distance(CPOB, position);
                if (distance > furthestDistance)
                {
                    furthestDistance = distance;
                    objAtFurthestDistance = collider.GetComponent<T>();
                }
            }
        }

        return objAtFurthestDistance;
    }

    public static float FindClosestPoint(float current, float[] points)
    {
        return FindClosestPoint(current, points, (float x, float y) => { return Mathf.Abs(x - y); });
    }

    public static Vector2 FindClosestPoint(Vector2 current, Vector2[] points)
    {
        return FindClosestPoint(current, points, Vector2.Distance);
    }

    public static Vector3 FindClosestPoint(Vector3 current, Vector3[] points)
    {
        return FindClosestPoint(current, points, Vector3.Distance);
    }

    private static T FindClosestPoint<T>(T current, T[] points, Func<T, T, float> distanceFomula)
    {
        if (points.Length < 0)
            throw new System.ArgumentException("BuildObjects.FindClosestPoint() -- points array must have Length greater than 0");

        float minimunDistance = distanceFomula(current, points[0]);
        int arrayPosition = 0;

        for (int i = 1; i < points.Length; i++)
        {
            float distance = distanceFomula(current, points[i]);
            if (distance < minimunDistance)
            {
                minimunDistance = distance;
                arrayPosition = i;
            }
        }

        return points[arrayPosition];
    }

    public static int TeamToLayer(Team team, bool isBullet)
    {
        if (team == Team.None)
            return 0;
        else
            return 9 + (((int)team - 1)  * 2) + ((isBullet) ? 1 : 0);
    }

    public static void RagDollGameObject(GameObject gameObject)
    {
        MonoBehaviour[] monoBehaviours = gameObject.GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour item in monoBehaviours)
        {
            item.enabled = false;
        }

        if (gameObject.GetComponent<Rigidbody>() != null)
        {
            Rigidbody _rigidbody = gameObject.GetComponent<Rigidbody>();
            _rigidbody.useGravity = true;
            _rigidbody.isKinematic = false;
            _rigidbody.freezeRotation = false;
        }

        if (gameObject.GetComponent<Collider>() != null)
        {
            gameObject.GetComponent<Collider>().enabled = true;
        }
    }
}
