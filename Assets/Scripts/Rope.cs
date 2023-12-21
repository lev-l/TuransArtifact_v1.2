using System;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public float Distance;
    public Vector2 Force;
    private Rigidbody2D _rigidbody;
    private RopeAnimation _animator;
    private UnityMovement _movement;
    private Transform _self;
    private Camera _camera;
    private RaycastHit2D[] _hitsBuffer = new RaycastHit2D[5];
    private List<RaycastHit2D> _hitsList = new List<RaycastHit2D>(5);

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<RopeAnimation>();
        _movement = GetComponent<UnityMovement>();
        _self = GetComponent<Transform>();

        _camera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _movement.AudioSystem.PlayRope();
            Vector3 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 distanceToMouse = mousePosition - _self.position;
            StartCoroutine(_animator.RopeAttaching(mousePosition));

            int count = _rigidbody.Cast(distanceToMouse.normalized, _hitsBuffer, Distance);

            _hitsList.Clear();
            for (int i = 0; i < count; i++)
            {
                _hitsList.Add(_hitsBuffer[i]);
            }

            for(int i = _hitsList.Count - 1; i >= 0; i--)
            {
                RaycastHit2D hit = _hitsList[i];

                if (hit.distance <= Distance
                    && hit.collider.CompareTag("Light"))
                {
                    Vector2 distanceToHit = hit.centroid - (Vector2)_self.position;
                    hit.collider.GetComponent<Animator>().Play("RopeAttached");

                    _movement.AddForce(Force * distanceToHit.normalized);
                    break;
                }
            }
        }
    }
}
