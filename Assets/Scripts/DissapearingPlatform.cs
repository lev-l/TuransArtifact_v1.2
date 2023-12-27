using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissapearingPlatform : MonoBehaviour
{
    [SerializeField] private float _delay; // time before dissapearing
    [SerializeField] private float _timeOff; // time while the platform is off
    private bool _notMoving;
    private SpriteRenderer _renderer;
    private Collider2D _collider;

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();
        _notMoving = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_notMoving)
        {
            StartCoroutine(nameof(Moving));
            _notMoving = false;
        }
    }

    private IEnumerator Moving()
    {
        yield return new WaitForSeconds(_delay);

        _collider.enabled = false;
        _renderer.color = new Color(0.28f, 0.28f, 0.28f);

        yield return new WaitForSeconds(_timeOff);

        _collider.enabled = true;
        _renderer.color = new Color(1, 1, 1);

        _notMoving = true;
    }
}
