using System;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public float Distance;
    public Vector2 Force;
    public Ables PlayerAbilities;
    private Rigidbody2D _rigidbody;
    private RopeAnimation _animator;
    private UnityMovement _movement;
    private Transform _self;
    private Camera _camera;
    private bool _teleporting;
    private RaycastHit2D[] _hitsBuffer = new RaycastHit2D[15];
    private List<RaycastHit2D> _hitsList = new List<RaycastHit2D>(20);

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<RopeAnimation>();
        _movement = GetComponent<UnityMovement>();
        _self = GetComponent<Transform>();
        _teleporting = false;
        _camera = Camera.main;
    }

    void Update()
    {
        if (PlayerAbilities.PhysicsTeleportation && Input.GetKeyDown(KeyCode.Q))
        {
            _teleporting = true;
        }
        if (PlayerAbilities.PhysicsTeleportation && Input.GetKeyUp(KeyCode.Q))
        {
            _teleporting = false;
        }


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
                LightCooldown lamp = hit.collider.GetComponent<LightCooldown>();

                if (hit.distance <= Distance
                    && lamp)
                {
                    TimedSwitch lampSwitch = lamp.GetComponent<TimedSwitch>();
                    if (lamp.Use())
                    {
                        Vector2 distanceToHit = hit.collider.transform.position - _self.position;

                        if (_teleporting)
                        {
                            _movement.transform.Translate(distanceToHit);
                        }
                        else
                        {
                            _movement.AddForce(Force * distanceToMouse.normalized);
                        }
                        if (lampSwitch)
                        {
                            lampSwitch.Activate();
                        }
                        break;
                    }
                }
            }
        }
    }
}
