using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PlayerProjectilePool : MonoBehaviour
{
    public ObjectPool<Projectile> _pool;

    private PlayerController _playerController;

    private void Start()
    {
        _playerController = GetComponent<PlayerController>();
        _pool = new ObjectPool<Projectile>(CreateProjectile, OnTakeProjectileFromPool, OnReturnProjectileToPool, OnDestroyProjectile, true, 10, 15);
    }

    private Projectile CreateProjectile()
    {
        Projectile projectile = Instantiate(_playerController.projectile, _playerController.projectileSpawnPoint.position, _playerController.projectileSpawnPoint.rotation);

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
