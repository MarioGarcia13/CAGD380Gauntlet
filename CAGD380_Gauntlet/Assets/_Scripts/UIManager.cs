using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    public GameObject[] playerUIPanels;
    public GameObject[] joinMessages;


    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.PlayerJoinedGame += PlayerJoinedGame;
        GameManager.instance.PlayerLeftGame += PlayerLeftGame;
    }

    void PlayerJoinedGame(PlayerInput playerInput)
    {
        ShowUIPanel(playerInput);
    }

    void PlayerLeftGame(PlayerInput playerInput)
    {
        HideUiPanel(playerInput);
    }

    void ShowUIPanel(PlayerInput playerInput)
    {
        playerUIPanels[playerInput.playerIndex].SetActive(true);
        playerUIPanels[playerInput.playerIndex].GetComponent<PlayerUI>().AssignPlayer(playerInput.playerIndex);
        joinMessages[playerInput.playerIndex].SetActive(false);
    }

    void HideUiPanel(PlayerInput playerInput)
    {
        playerUIPanels[playerInput.playerIndex].SetActive(false);
        joinMessages[playerInput.playerIndex].SetActive(false);
    }
}
