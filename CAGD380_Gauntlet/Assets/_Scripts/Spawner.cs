using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Range(1, 3)]
    public static int level;

    public int spawnerHealth;
    public GameObject spawnLocation;
    public GameObject EnemyType;

    // Start is called before the first frame update
    void Start()
    {
        switch (level)
        {
            case 1:
                spawnerHealth = 1;
                break;
            case 2:
                spawnerHealth = 2;
                break;
            case 3:
                spawnerHealth = 3;
                break;
            default:
                break;
        }

        InvokeRepeating("SpawnEnemy", 3.0f, 3.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnEnemy()
    {
        GameObject.Instantiate(EnemyType, spawnLocation.transform.position, transform.rotation);
    }
}
