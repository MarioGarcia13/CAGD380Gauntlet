using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Range(1, 3)]
    public int level;

    public static int spawnerHealth;
    public GameObject spawnLocation;
    public GameObject EnemyType;
    public bool isNearby = false;

    // Start is called before the first frame update
    void Start()
    {
        switch (level)
        {
            case 1:
                spawnerHealth = 1;
                GetComponent<MeshRenderer>().material.color = Color.red;
                break;
            case 2:
                spawnerHealth = 2;
                GetComponent<MeshRenderer>().material.color = Color.yellow;
                break;
            case 3:
                spawnerHealth = 3;
                GetComponent<MeshRenderer>().material.color = Color.green;
                break;
            default:
                break;
        }
        InvokeRepeating("SpawnEnemy", 10f, 60f);
    }
    public void SpawnEnemy()
    {
       GameObject.Instantiate(EnemyType, spawnLocation.transform.position, transform.rotation);
    }


}
