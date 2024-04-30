using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : Subject
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    [SerializeField] float playerSpeed = 5f;
    private float gravityValue = -9.81f;

    private HUDController _hudController;
    [SerializeField] private float health = 100;

    private Vector3 move;

    public float CurrentHealth
    {
        get { return health; }
    }
    private void Awake()
    {
        _hudController = gameObject.AddComponent<HUDController>();
        controller = GetComponent<CharacterController>();
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
    void Update()
    {  
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 movement = context.ReadValue<Vector2>();
        move = new Vector3(movement.x, 0, movement.y);
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
}