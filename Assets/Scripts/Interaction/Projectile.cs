using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * The Projectile class handels the objects shot out of
 * Weapons. 
 */

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(TeamLayer))]
public class Projectile : MonoBehaviour
{
    private Rigidbody _rigidbody;
    public ProjectileData Data { get; private set; }

    //TODO: implement projectile owner and friendly fire
    public GameObject Owner { get; private set; }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.velocity = transform.forward * Data.bulletSpeed;
    }

    private void Update()
    {
        _rigidbody.AddForce(new Vector3(0, -1, 0));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (Data.ExplosionRadius > 0)
        {
            CreateExplosionGraphics(collision.contacts[0].point, Data.ExplosionRadius);
            Collider[] colliders = Physics.OverlapSphere(
                collision.contacts[0].point,
                Data.ExplosionRadius);
            foreach(Collider collider in colliders)
            {
                Rigidbody rb = collider.GetComponent<Rigidbody>();
                TeamLayer team = collider.GetComponent<TeamLayer>();
                Health hlth = collider.GetComponent<Health>();

                if (rb != null)
                    rb.AddExplosionForce(
                        Data.ExplosionForce,
                        collision.contacts[0].point,
                        Data.ExplosionRadius,
                        3.0F);

                if (team != null)
                    if(team != GetComponent<TeamLayer>().team)
                        if (hlth != null)
                        {
                            hlth.Hit(Data);
                        }
            }
        }
        else if(collision.gameObject.GetComponent<Health>() != null)
        {
            collision.gameObject.GetComponent<Health>().Hit(Data);
        }

        BulletOnHit();
    }

    private void CreateExplosionGraphics(Vector3 point, float explosionRadius)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = point;
        sphere.transform.localScale = Vector3.one * explosionRadius;
        sphere.GetComponent<Collider>().enabled = false;
        ShrinkAndDestroy sad = sphere.AddComponent<ShrinkAndDestroy>();
        sad.SetupVars(0.4f);
    }

    private void BulletOnHit()
    {
        Functions.RagDollGameObject(gameObject);
        ShrinkAndDestroy sad = gameObject.AddComponent<ShrinkAndDestroy>();
        sad.SetupVars(Data.afterHitLifeSpan);
        Destroy(this);
    }

    public void SetOwner(GameObject Owner)
    {
        this.Owner = Owner;
    }

    public void SetData(ProjectileData bulletData)
    {
        this.Data = bulletData;
    }
}

