using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody2D _object;
    private UnityMovement _movement;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _object = GetComponent<Rigidbody2D>();
        _movement = GetComponent<UnityMovement>();
    }

    void Update()
    {
        if(_object.velocity.y > 0)
        {
            _animator.SetBool("IsJump", true);
        }
        if(_object.velocity.y < 0)
        {
            _animator.SetBool("Landing", true);
        }
        if(_object.velocity.y == 0)
        {
            _animator.SetBool("IsJump", false);
            _animator.SetBool("Landing", false);
        }
        if (_movement.grounded)
        {
            _animator.SetBool("IsJump", false);
            _animator.SetBool("Landing", false);
        }
        if(_object.velocity.x > 0)
        {
            _animator.SetBool("IsRun", true);
            _animator.SetBool("IsLeft", false);
        }
        if(_object.velocity.x < 0)
        {
            _animator.SetBool("IsRun", true);
            _animator.SetBool("IsLeft", true);
        }
        if(_object.velocity.x == 0)
        {
            _animator.SetBool("IsRun", false);
        }
    }
}
