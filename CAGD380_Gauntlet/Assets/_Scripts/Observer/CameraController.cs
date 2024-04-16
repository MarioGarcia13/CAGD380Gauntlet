using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : Observer
{
    private bool _hitOtherPlayer;
    private bool _hitFood;
    private bool _lifeForceLow;
    private Vector3 _initialPosition;
    //private float _shakeMagnitude = 0.1f;
    private PlayerController _playerController;

    private void OnEnable()
    {
        _initialPosition = gameObject.transform.localPosition;
    }

    /// <summary>
    /// Simple BAD version of screenshake
    /// IEnumerator is better or some kind of coroutine
    /// </summary>
    private void Update()
    {
        if (_lifeForceLow)
        {
            gameObject.transform.localPosition = _initialPosition + (Random.insideUnitSphere * _shakeMagnitude);
        }
        else
        {
            gameObject.transform.localPosition = _initialPosition;
        }


    }
    /// <summary>
    /// All we do here in Notify is get the reference to the BikeController
    /// and thenset our local reference to the _isTurboOn
    /// The actual screenshake is turned on/off thru Update
    /// </summary>
    /// <param name="subject"></param>
    public override void Notify(Subject subject)
    {
        if (!_playerController)
        {
            _playerController = subject.GetComponent<PlayerController>();
        }
        if (_playerController)
        {
            _lifeForceLow = _playerController.IsTurboOn;
        }
    }
}
