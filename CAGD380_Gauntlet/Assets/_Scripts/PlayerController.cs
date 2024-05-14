using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : Subject
{
    private CharacterController controller;
    private Projectile projectile;
    private Vector3 playerVelocity;
    [SerializeField] float playerSpeed = 5f;

    private HUDController _hudController;
    [SerializeField] private float health = 100;

    private Vector3 move;

    public GameObject Sword;

    public float CurrentHealth
    {
        get { return health; }
    }
    private void Awake()
    {
        _hudController = gameObject.AddComponent<HUDController>();
        controller = GetComponent<CharacterController>();

        if (Sword != null)
        {
            Sword.SetActive(false);
        }
    }

    private void OnEnable()
    {
        if (_hudController)
        {
            Attach(_hudController);
        }
    }

    private void OnDisable()
    {
        if (_hudController)
        {
            Detach(_hudController);
        }
    }

    
    void FixedUpdate()
    {  
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        controller.Move(playerVelocity * Time.deltaTime);
    }
    

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 movement = context.ReadValue<Vector2>();
        move = new Vector3(movement.x, 0, movement.y);
    }

    public void OnMelee(InputAction.CallbackContext context)
    {
        Sword.SetActive(context.performed);
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.performed)
            projectile.Shoot();
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        NotifyObservers();

        if (health < 0)
        {
            Destroy(gameObject);
        }
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("food"))
        {
            if(health == 100)
            {
                Destroy(other.gameObject);
                return;  
            }
            else
            {
                health += 50;
                Destroy(other.gameObject);
            }

        }
        if (other.CompareTag("treasure"))
        {
            //increase score
            Destroy(other.gameObject);
        }
        if (other.CompareTag("enemy"))
        {
            health -= 10; //temporary
        }

    }
}