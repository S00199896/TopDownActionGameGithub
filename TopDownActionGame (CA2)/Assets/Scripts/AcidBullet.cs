using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidBullet : Bullet
{
    public GameObject ExplosionPrefab;

    private void CreateExplosion()
    {
        Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
    }

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        CreateExplosion();
        base.OnCollisionEnter2D(collision);
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        CreateExplosion();
        base.OnTriggerEnter2D(collision);
    }
}
