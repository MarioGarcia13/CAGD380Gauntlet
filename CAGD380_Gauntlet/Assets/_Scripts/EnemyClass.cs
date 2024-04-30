using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyClass : MonoBehaviour
{
    public float health = 2f;
    public float speed = 1f;
    public float damage = 1f;

    public virtual void Attack()
    {
        Debug.Log("attacked the player");
    }

    public virtual void Roam()
    {

    }
}
