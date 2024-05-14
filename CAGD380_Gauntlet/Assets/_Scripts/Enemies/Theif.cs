using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Theif : EnemyClass
{
    private void Awake()
    {
        GetComponent<MeshRenderer>().material.color = Color.magenta;
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
