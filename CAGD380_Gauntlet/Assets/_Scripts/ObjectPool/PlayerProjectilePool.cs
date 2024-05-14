using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PlayerProjectilePool : MonoBehaviour
{
    public int maxPoolSize = 2;
    public int stackDefaultCapacity = 2;

    public ObjectPool<Projectile> _pool;

    private PlayerController _playerController;

    private void Start()
    {
        _playerController = GetComponent<PlayerController>();
        _pool = new ObjectPool<Projectile>(CreateProjectile, OnTakeProjectileFromPool, OnReturnProjectileToPool, OnDestroyProjectile, true, stackDefaultCapacity, maxPoolSize);
    }

    private Projectile CreateProjectile()
    {
        Projectile projectile = Instantiate(_playerController.projectile, _playerController.projectileSpawnPoint.position, transform.rotation);

        projectile.SetPool(_pool);

        return projectile;
    }

    private void OnTakeProjectileFromPool(Projectile projectile)
    {
        projectile.transform.position = _playerController.projectileSpawnPoint.position;

        projectile.gameObject.SetActive(true);
    }

    private void OnReturnProjectileToPool(Projectile projectile)
    {
        projectile.gameObject.SetActive(false);
    }

    private void OnDestroyProjectile(Projectile projectile)
    {
        Destroy(projectile.gameObject);
    }
}
