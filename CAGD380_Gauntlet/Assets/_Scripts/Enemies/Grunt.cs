using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grunt : EnemyClass
{

    private void Awake()
    {
        GetComponent<MeshRenderer>().material.color = Color.yellow;
        health = Spawner.spawnerHealth;
        speed = 5f;
        damage = 5f;
    }
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
    }


}
