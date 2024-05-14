using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientObjectPool : MonoBehaviour
{
    private PlayerProjectilePool _pool;

    private void Start()
    {
        //_pool = gameObject.AddComponent<PlayerProjectilePool>();
    }

    private void OnGUI()
    {
        //if (GUILayout.Button("Spawn"))
           // _pool.Spawn();
    }
}

