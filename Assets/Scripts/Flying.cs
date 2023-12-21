using System.Collections;
using UnityEngine;

public class Flying : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private bool _fly;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_fly)
        {
            if(_rigidbody.velocity.y < -2)
            {
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x,
                                                        -2);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _fly = true;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            _fly = false;
        }
    }
}
