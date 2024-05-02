using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demon : EnemyClass
{
    private void Awake()
    {
        GetComponent<MeshRenderer>().material.color = Color.red;
        health = Spawner.spawnerHealth;
        speed = 5f;
        damage = 5f;
    }
}
