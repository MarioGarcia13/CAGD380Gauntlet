using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDController : Observer
{
    private bool _hitOtherPlayer;
    private bool _hitFood;
    private bool _lifeForceLow;

    private bool _isTurboOn;
    private float _currentHealth;
    private PlayerController _playerController;

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(50, 50, 100, 200));
        GUILayout.BeginHorizontal("box");
        GUILayout.Label("Health" + _currentHealth);
        GUILayout.EndHorizontal();

        if (_lifeForceLow)
        {
            GUILayout.BeginHorizontal("box");
            GUILayout.Label("WARNING LOW HEALTH");
            GUILayout.EndHorizontal();
        }

        GUILayout.EndArea();
    }

    /// <summary>
    /// receive a reference to subject that notified it
    /// It can then access the subjects properties and choose which one to
    /// display in the interface
    /// </summary>
    /// <param name="subject"></param>
    public override void Notify(Subject subject)
    {
        //throw new System.NotImplementedException();
        if (!_playerController)
        {
            _playerController = subject.GetComponent<PlayerController>();
        }

        if (_playerController)
        {
            //_isTurboOn = _playerController.IsTurboOn;
            _currentHealth = _playerController.CurrentHealth;
        }
    }
}
