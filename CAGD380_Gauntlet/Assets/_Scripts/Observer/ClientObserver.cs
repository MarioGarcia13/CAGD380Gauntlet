using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientObserver : MonoBehaviour
{
    private PlayerController _playerController;

    private void Start()
    {
        _playerController = (PlayerController)FindObjectOfType(typeof(PlayerController));
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Damage Player"))
            if (_playerController)
                _playerController.TakeDamage(15f);
    }
}
