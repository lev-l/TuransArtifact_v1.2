using System;
using System.Collections;
using UnityEngine;

public class UnityMovement : MonoBehaviour
{
    public Audio AudioSystem;
    public float Speed;
    public float JumpForce;
    public bool grounded { get; private set; }
    private bool _wantsToJump = false;
    private Rigidbody2D _rigidbody;
    private float _xSpeed;
    private ContactFilter2D _filter;

    public event Action OnGround;
    public event Action OnUnground;

    void Start()
    {
        grounded = false;
        _rigidbody = GetComponent<Rigidbody2D>();
        AudioSystem = GetComponent<Audio>();

        _filter.useTriggers = false;
        _filter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        _filter.useLayerMask = true;
        OnUnground += SetGroundedFalse;

        StartCoroutine(GetGround());
    }

    void Update()
    {
        Vector2 newVelocity = _rigidbody.velocity;

        newVelocity.x = Input.GetAxis("Horizontal") * Speed + _xSpeed;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _wantsToJump = true;
            StartCoroutine(nameof(ResetJumpDelay));
        }
        if (_wantsToJump && grounded)
        {
            newVelocity.y = Jump();
        }

        _rigidbody.velocity = newVelocity;
    }

    public IEnumerator GetGround()
    {
        RaycastHit2D[] buffer = new RaycastHit2D[10];
        while (true)
        {
            int grounds = Physics2D.Raycast(_rigidbody.position, Vector2.down, _filter, buffer, 0.7f);

            bool before = grounded;
            if(grounds > 0)
            {
                grounded = true;
            }
            else if (before == true)
            {
                StartCoroutine(nameof(SetGroundedFalseDelay));
            }

            if (!before && grounds > 0)
            {
                AudioSystem.PlayJump();
                OnGround?.Invoke();
            }

            yield return new WaitForSeconds(0.01f);
        }
    }

    public IEnumerator SetGroundedFalseDelay()
    {
        yield return new WaitForSeconds(0.1f);
        grounded = false;
    }

    private void SetGroundedFalse()
    {
        grounded = false;
    }

    public void AddForce(Vector2 force)
    {
        _rigidbody.velocity = force;
        _xSpeed = force.x;
        Invoke(nameof(SetXSpeed0), 0.2f);
    }

    private IEnumerator ResetJumpDelay()
    {
        yield return new WaitForSeconds(0.1f);
        _wantsToJump = false;
    }

    private float Jump()
    {
        float newY = Input.GetAxis("Jump") * JumpForce;
        _wantsToJump = false;
        OnUnground?.Invoke();

        return newY;
    }

    public void SetXSpeed0()
    {
        _xSpeed = 0;
    }
}
