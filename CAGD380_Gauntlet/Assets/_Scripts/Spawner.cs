using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject spawnLocation;
    public GameObject EnemyType;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        SpawnEnemy();
    }

    IEnumerator SpawnDelay()
    {     
        yield return new WaitForSeconds(3f);
        SpawnEnemy();
        
    }

    public void SpawnEnemy()
    {
        StartCoroutine(SpawnDelay());
        GameObject.Instantiate(EnemyType, spawnLocation.transform.position, transform.rotation);
    }
}
