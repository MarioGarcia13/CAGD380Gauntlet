using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PlayerProjectilePool : MonoBehaviour
{
    public int maxPoolSize = 10;
    public int stackDefaultCapacity = 5;
    private IObjectPool<Projectile> _pool;
    private Transform playerTransform;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public IObjectPool<Projectile> Pool
    {
        get
        {
            if (_pool == null)
                _pool = new ObjectPool<Projectile>(CreatedPooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, true, stackDefaultCapacity, maxPoolSize);
            return _pool;
        }
    }

    private Projectile CreatedPooledItem()
    {
        //use var only inside of function
        var go = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        Projectile projectile = go.AddComponent<Projectile>();

        go.name = "Projectile";
        projectile.Pool = Pool;

        return projectile;
    }

    private void OnReturnedToPool(Projectile projectile)
    {
        projectile.gameObject.SetActive(false);
    }

    private void OnTakeFromPool(Projectile projectile)
    {
        projectile.gameObject.SetActive(true);
    }

    private void OnDestroyPoolObject(Projectile projectile)
    {
        Destroy(projectile.gameObject);
    }

    public void Spawn()
    {
        // Get a projectile from the pool
        var projectile = Pool.Get();

        // Set the projectile's position to the player's position
        projectile.transform.position = playerTransform.position;
    }
}
