using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMovement : MonoBehaviour
{
    public float Speed;
    private Transform _self;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _self = GetComponent<Transform>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _rigidbody.velocity = Vector2.zero;
        Vector2 movement = new Vector2();
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        _self.Translate(movement * Time.deltaTime * Speed);
    }
}
