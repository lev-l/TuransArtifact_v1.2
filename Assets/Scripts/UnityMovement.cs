using System;
using System.Collections;
using UnityEngine;

public class UnityMovement : MonoBehaviour
{
    public Audio AudioSystem;
    public float Speed;
    public float JumpForce;
    public bool grounded { get; private set; }
    private Rigidbody2D _rigidbody;
    private GhostMovement _ghost;
    private float _xSpeed;
    private ContactFilter2D _filter;

    public event Action OnGround;
    public event Action OnUnground;

    void Start()
    {
        grounded = false;
        _rigidbody = GetComponent<Rigidbody2D>();
        _ghost = GetComponent<GhostMovement>();
        AudioSystem = GetComponent<Audio>();

        _filter.useTriggers = false;
        _filter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        _filter.useLayerMask = true;

        StartCoroutine(GetGround());
    }

    void Update()
    {
        Vector2 newVelocity = _rigidbody.velocity;

        newVelocity.x = Input.GetAxis("Horizontal") * Speed + _xSpeed;
        if (grounded
            && Input.GetKeyDown(KeyCode.Space))
        {
            newVelocity.y = Input.GetAxis("Jump") * JumpForce;
            OnUnground?.Invoke(); // only works after you jump, think if you need to move it anywhere
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
            grounded = grounds > 0;
            if (!before  && grounded
                && !_ghost.enabled)
            {
                AudioSystem.PlayJump();
                OnGround?.Invoke();
            }

            yield return new WaitForSeconds(0.01f);
        }
    }

    public void AddForce(Vector2 force)
    {
        _rigidbody.velocity = force;
        _xSpeed = force.x;
        Invoke(nameof(SetXSpeed0), 0.2f);
    }

    public void SetXSpeed0()
    {
        _xSpeed = 0;
    }
}
