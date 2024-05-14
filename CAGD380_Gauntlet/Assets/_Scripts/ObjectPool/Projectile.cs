using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Projectile : MonoBehaviour
{
    public IObjectPool<Projectile> Pool { get; set; }

    private float _speed = 10f;


    [SerializeField]
    private float timeToSelfDestruct = 3f;

    private void Start()
    {
        
    }

    private void OnEnable()
    {
        //AttackPlayer();
        StartCoroutine(SelfDestruct());
    }

    private void OnDisable()
    {
        ResetProjectile();
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(timeToSelfDestruct);
        //TakeDamage(maxHealth);
    }

    private void ReturnToPool()
    {
        Pool.Release(this);
    }

    private void ResetProjectile()
    {
        //_currentHealth = maxHealth;
    }

    public void Shoot()
    {
        Debug.Log("attack player behaviors go here");
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }

    public void TakeDamage(float amount)
    {
        //_currentHealth -= amount;

        //if (_currentHealth <= 0.0f)
            //ReturnToPool();
    }
}
