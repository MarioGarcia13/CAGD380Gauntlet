using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : Subject
{
    private CharacterController controller;
    private Projectile projectile;
    private Vector3 playerVelocity;
    [SerializeField] float playerSpeed = 5f;

    private HUDController _hudController;
    [SerializeField] private float health = 700;

    private int keys = 0;
    private int score = 0;

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

    private void Update()
    {
        //decrease health every second.
        if (health <= 0)
        {
            GameOver();
        }
        else
        {
            health -= 1 * Time.deltaTime;
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
    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void TheEnd()
    {
        SceneManager.LoadScene("TheEnd");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("food"))
        {
            if (health == 700)
            {
                Destroy(other.gameObject);
                return;
            }
            else
            {
                health += 500;
                Destroy(other.gameObject);
            }
        }
        if (other.CompareTag("treasure"))
        {
            score += 100;
            Destroy(other.gameObject);
        }
        if (other.CompareTag("key"))
        {
            keys++;
            score += 100;
            Destroy(other.gameObject);
        }
        if (other.CompareTag("door"))
        {
            if (keys <= 0)
            {
                return;
            }
            keys--;
            Destroy(other.gameObject);

        }
        if (other.CompareTag("enemy"))
        {
            health -= 10; //temporary
        }
        if (other.CompareTag("portal"))
        {
            TheEnd();
        }
    }
}