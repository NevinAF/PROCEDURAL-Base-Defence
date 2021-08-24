using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class WeaponData : ScriptableObject
{
    [Tooltip("One Shot per XXX seconds")]
    public float attackRate = 1;
    public float Range = 6;

    public ProjectileData[] projectiles;
}

[System.Serializable]
public class ProjectileData
{
    public float damage = 1;
    [Tooltip("XXX units per second.")]
    public float bulletSpeed = 3f;
    public Vector3 bulletSize = new Vector3(.2f, .2f, .2f);
    public float maxLifeSpan = 30;
    public float afterHitLifeSpan = 3;
    public float ExplosionRadius = 0;
    public float ExplosionForce;
    public GameObject BulletPrefab;
    public Vector3 OffectEulerDirection = new Vector3(0, 0, 0);

    public void OnValidate()
    {
        if (BulletPrefab.GetComponent<Projectile>() == null)
        {
            Debug.Log("Invalid bullet GO");
            BulletPrefab = null;
        }
    }

    public GameObject InstantiateSelf(
        Vector3 position, 
        Quaternion rotation, 
        Transform parent, 
        Team team, 
        GameObject owner)
    {
        GameObject projectile_go = GameObject.Instantiate(BulletPrefab, position, rotation, parent);
        projectile_go.transform.localEulerAngles += OffectEulerDirection;
        projectile_go.transform.localScale = bulletSize;
        projectile_go.GetComponent<Projectile>().SetData(this);
        projectile_go.GetComponent<Projectile>().SetOwner(owner);
        projectile_go.GetComponent<TeamLayer>().team = team;

        return projectile_go;
    }

    public GameObject InstantiateSelf(
        Vector3 position,
        Quaternion rotation,
        Transform parent)
    {
        return InstantiateSelf(position, rotation, parent, Team.None, null);
    }
}
