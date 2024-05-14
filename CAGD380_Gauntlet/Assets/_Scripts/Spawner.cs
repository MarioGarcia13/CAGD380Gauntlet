using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Range(1, 3)]
    public int level;


    public static int spawnerHealth;
    public GameObject spawnLocation;
    public GameObject EnemyType;

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

        InvokeRepeating("SpawnEnemy", 5.0f, 10.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnEnemy()
    {
        GameObject.Instantiate(EnemyType, spawnLocation.transform.position, transform.rotation);
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("sword") || other.CompareTag("projectile"))
        {
            spawnerHealth -= 1;

            if (spawnerHealth <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
