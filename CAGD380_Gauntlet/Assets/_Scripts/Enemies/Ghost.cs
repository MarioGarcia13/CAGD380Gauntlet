using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : EnemyClass
{
    private void Awake()
    {
        GetComponent<MeshRenderer>().material.color = Color.white;
        health = Spawner.spawnerHealth;
        speed = 5f;
        damage = 5f;
    }
}
