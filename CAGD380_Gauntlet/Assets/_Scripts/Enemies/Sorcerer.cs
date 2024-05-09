using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sorcerer : EnemyClass
{
    private void Awake()
    {
        GetComponent<MeshRenderer>().material.color = Color.blue;
        health = Spawner.spawnerHealth;
        speed = 5f;
        damage = 5f;
    }
}
