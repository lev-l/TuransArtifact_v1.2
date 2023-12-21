using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sticking : MonoBehaviour
{
    protected Rigidbody2D _rigidbody;

    protected void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Stick()
    {
        _rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    public void UnStick()
    {
        _rigidbody.constraints = RigidbodyConstraints2D.None;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
    }
}
