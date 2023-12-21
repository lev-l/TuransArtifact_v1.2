using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
    public Vector2 Velocity;
    public bool grounded { get; protected set; }
    [SerializeField] protected float MinGroundNormalY;
    [SerializeField] protected float GravityModifier;
    protected float _additiveXSpeed;
    protected Vector2 _targetVelocity;
    protected Vector2 _groundNormal;
    protected Rigidbody2D _rigidbody;
    protected ContactFilter2D _contactFilter;
    protected RaycastHit2D[] _hitsBuffer = new RaycastHit2D[10];
    protected List<RaycastHit2D> _hitsList = new List<RaycastHit2D>(10);

    protected const float minMoveDistance = 0.001f;
    protected const float shellRadius = 0.01f;

    public void AddForce(Vector2 force)
    {
        Velocity.y = force.y;

        _additiveXSpeed = force.x;
        Invoke(nameof(SetXSpeed0), 0.2f);
    }

    public void SetXSpeed0()
    {
        _additiveXSpeed = 0;
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        _contactFilter.useTriggers = false;
        _contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        _contactFilter.useLayerMask = true;
    }

    private void Update()
    {
        _targetVelocity = Vector2.zero;
        ComputeVelocity();
    }

    protected virtual void ComputeVelocity()
    {

    }

    private void FixedUpdate()
    {
        Velocity += GravityModifier * Physics2D.gravity * Time.deltaTime;
        Velocity.x = _targetVelocity.x + _additiveXSpeed;

        grounded = false;

        Vector2 deltaPosition = Velocity * Time.deltaTime;

        Vector2 moveAlongGround = new Vector2(_groundNormal.y, -_groundNormal.x);

        Vector2 move = moveAlongGround * deltaPosition.x;

        Movement(move, false);

        move = Vector2.up * deltaPosition.y;

        Movement(move, true);
    }

    private void Movement(Vector2 move, bool yMovement)
    {
        float distance = move.magnitude;

        if (distance > minMoveDistance)
        {
            int count = _rigidbody.Cast(move, _contactFilter, _hitsBuffer, distance + shellRadius);
            _hitsList.Clear();
            for (int i = 0; i < count; i++)
            {
                _hitsList.Add(_hitsBuffer[i]);
            }

            for (int i = 0; i < _hitsList.Count; i++)
            {
                Vector2 currentNormal = _hitsList[i].normal;
                if (currentNormal.y > MinGroundNormalY)
                {
                    grounded = true;
                    if (yMovement)
                    {
                        _groundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }

                float projection = Vector2.Dot(Velocity, currentNormal);
                if (projection < 0)
                {
                    Velocity = Velocity - projection * currentNormal;
                }

                float modifiedDistance = _hitsList[i].distance - shellRadius;
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }
        }

        _rigidbody.position = _rigidbody.position + move.normalized * distance;
    }
}
