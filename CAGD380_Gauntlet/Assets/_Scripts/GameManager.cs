using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField] private InputAction joinAction;
    [SerializeField] private InputAction leaveAction;

    public GameObject[] spawnPoints;

    public List<PlayerInput> playerList = new List<PlayerInput>();

    public static GameManager instance = null;

    public event System.Action<PlayerInput> PlayerJoinedGame;
    public event System.Action<PlayerInput> PlayerLeftGame;
    

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");

        joinAction.Enable();
        joinAction.performed += context => JoinAction(context);

        leaveAction.Enable();
        leaveAction.performed += context => LeaveAction(context);
    }
    private void Start()
    {
        PlayerInputManager.instance.JoinPlayer(0, -1, null);
    }

    void OnPlayerJoined(PlayerInput playerInput)
    {
        playerList.Add(playerInput);

        if(PlayerJoinedGame != null)
        {
            PlayerJoinedGame(playerInput);
        }
    }
    
    void OnPlayerLeft(PlayerInput playerInput)
    {
        //can setup narrator sound here for player leaving.
    }

    void JoinAction(InputAction.CallbackContext context)
    {
        PlayerInputManager.instance.JoinPlayerFromActionIfNotAlreadyJoined(context);
    }
    void LeaveAction(InputAction.CallbackContext context)
    {
        if (playerList.Count > 1)
        {
            foreach (var player in playerList)
            {
                foreach (var device in player.devices)
                {
                    if (device != null && context.control.device == device)
                    {
                        UnregisterPlayer(player);
                        return;
                    }
                }
            }
        }
    }

    void UnregisterPlayer(PlayerInput playerInput)
    {
        playerList.Remove(playerInput);

        if (PlayerLeftGame != null)
        {
            PlayerLeftGame(playerInput);
        }

        Destroy(playerInput.transform.parent.gameObject);
    }
}
