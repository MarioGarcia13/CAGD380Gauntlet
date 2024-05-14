using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : Subject
{
    private CharacterController controller;
    private PlayerProjectilePool projectilePool;
    public Projectile projectile;
    private Vector3 playerVelocity;
    [SerializeField] float playerSpeed = 5f;

    private HUDController _hudController;
    [SerializeField] private float health = 700;

    public enum playerName
    {
        Player1,
        Player2,
        Player3,
        Player4
    }

    public playerName thisPlayersName = playerName.Player1;

    private int keys = 0;
    private int score = 0;

    private Vector3 move;

    public GameObject sword;
    public GameObject bow;
    public Transform projectileSpawnPoint;

    public event System.Action<int> OnScoreChanged;
    public event System.Action<float> OnHealthChanged;

    public float CurrentHealth
    {
        get { return health; }
    }
    private void Awake()
    {
        _hudController = gameObject.AddComponent<HUDController>();
        controller = GetComponent<CharacterController>();

        if (sword != null)
        {
            sword.SetActive(false);
        }
        if (bow != null)
        {
            sword.SetActive(false);
        }
    }

    private void Start()
    {
        projectilePool = GetComponent<PlayerProjectilePool>();
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
            if (OnHealthChanged != null)
            {
                OnHealthChanged(health);
            }
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
        sword.SetActive(context.performed);
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        bow.SetActive(context.performed);
        if(context.performed)
            projectilePool._pool.Get();

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
            if (OnScoreChanged != null)
            {
                OnScoreChanged(score);
            }
        }
        if (other.CompareTag("key"))
        {
            keys++;
            score += 100;
            Destroy(other.gameObject);
            if (OnScoreChanged != null)
            {
                OnScoreChanged(score);
            }
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
            if (OnHealthChanged != null)
            {
                OnHealthChanged(health);
            }
        }
        if (other.CompareTag("portal"))
        {

            TheEnd();
        }
    }
}