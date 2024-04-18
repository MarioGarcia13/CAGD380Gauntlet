using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerClass : MonoBehaviour
{
    public string ClassName;

    [Header("Player Attributes")]
    [SerializeField] float speed;
    [SerializeField] float strength;
    [SerializeField] float magic;
    [SerializeField] float health;
    [SerializeField] float armor;

    public void OnMelee(InputAction.CallbackContext context)
    {

    }public void OnProjectile(InputAction.CallbackContext context)
    {

    }
}
