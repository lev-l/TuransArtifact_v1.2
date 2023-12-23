using System;
using UnityEngine;

public class METRICS : MonoBehaviour
{
    private Transform _transform;
    private float _xPosition;
    private float _xMax;
    private float _yPosition;
    private float _yMax;
    private float _timeAppoaxis;
    private bool _inJump;
    private event Action _appoaxis;

    private void Start()
    {
        _transform = GetComponent<Transform>();
        GetComponent<UnityMovement>().OnGround += Grounded;
        GetComponent<UnityMovement>().OnUnground += Ungrounded;

        _appoaxis += FixTimeAppoaxis;
    }

    private void FixedUpdate()
    {
        if (_inJump)
        {
            if (Mathf.Abs(_transform.position.x - _xPosition) > Mathf.Abs(_xMax - _xPosition))
            {
                _xMax = _transform.position.x;
            }
            if (Mathf.Abs(_transform.position.y - _yPosition) > Mathf.Abs(_yMax - _yPosition))
            {
                _yMax = _transform.position.y;
            }
            else
            {
                _appoaxis?.Invoke();
            }
        }
        Debug.Log((_yPosition - _transform.position.y) / (Time.time - _timeAppoaxis));
    }

    public void Ungrounded()
    {
        _xPosition = _transform.position.x;
        _yPosition = _transform.position.y;
        _xMax = _xPosition;
        _yMax = _yPosition;
        _inJump = true;
    }

    public void FixTimeAppoaxis()
    {
        _timeAppoaxis = Time.time;
    }

    public void Grounded()
    {
        Debug.Log($"X difference: {_xMax - _xPosition}; Y difference: {_yMax - _yPosition}");
        _inJump = false;
    }
}
