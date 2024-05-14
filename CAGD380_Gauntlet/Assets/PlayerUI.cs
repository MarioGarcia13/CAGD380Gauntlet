using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public TextMeshProUGUI playerName;
    public TextMeshProUGUI playerScore;
    public TextMeshProUGUI playerHealth;

    PlayerController player;

    public void AssignPlayer(int index)
    {
        StartCoroutine(AssignPlayerDelay(index));
    }

    IEnumerator AssignPlayerDelay(int index)
    {
        yield return new WaitForSeconds(0.01f);
        player = GameManager.instance.playerList[index].GetComponent<PlayerInputHandler>().playerController;

        SetupInfoPanel();
    }

    void SetupInfoPanel()
    {
        if (player != null)
        {
            player.OnScoreChanged += UpdateScore;
            player.OnHealthChanged += UpdateHealth;
            playerName.text = player.thisPlayersName.ToString();
        }

    }

    private void UpdateScore(int score)
    {
        playerScore.text = score.ToString();
    }

    private void UpdateHealth(float health)
    {
        playerHealth.text = health.ToString("00.0");
    }
}
