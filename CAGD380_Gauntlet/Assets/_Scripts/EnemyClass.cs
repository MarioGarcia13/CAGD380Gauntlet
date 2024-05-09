using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class EnemyClass : MonoBehaviour
{
    public float health = 2f;
    public float speed = 1f;
    public float damage = 1f;
    public float attackRadius = .3f;
    public float attackCooldown = 2.5f;
    public float lastAttackTime;
    private Transform player;
    private Rigidbody rb;

    private void Awake()
    {
        GetComponent<Material>().color = Color.white;
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Roam();
    }

    public virtual void Attack()
    {
        Debug.Log("attacked the player");
    }

    public virtual void Roam()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRadius)
        {
            if(Time.time - lastAttackTime >= attackCooldown)
            {
                Attack();
                lastAttackTime = Time.time;
            }
            rb.velocity = Vector3.zero;
            return;
        }
        Vector3 moveDirection = (player.position - transform.position).normalized;
        rb.velocity = moveDirection * speed;
    }
}
