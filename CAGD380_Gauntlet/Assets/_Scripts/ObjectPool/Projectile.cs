using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Projectile : MonoBehaviour
{
    private float destroyTime = 1f;
    private float speed = 10f;
    private float damage = 2f;
    private Rigidbody rb;

    private ObjectPool<Projectile> _pool;

    private Coroutine deactivateProjectileAfterTime;

    private void OnEnable()
    {
        deactivateProjectileAfterTime = StartCoroutine(DeactivateProjectile());
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        rb.velocity = transform.forward * speed;
    }

    public void SetPool(ObjectPool<Projectile> pool)
    {
        _pool = pool;
    }

    private void ReturnToPool()
    {
        _pool.Release(this);
    }

    private IEnumerator DeactivateProjectile()
    {
        float elapsedTime = 0f;
        while(elapsedTime < destroyTime)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
            
        }
        ReturnToPool();
    }
}
