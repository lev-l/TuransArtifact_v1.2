using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissapearingPlatform : MonoBehaviour
{
    [SerializeField] private float _delay;
    [SerializeField] private float _timeIn; // time while the platform is active
    [SerializeField] private float _timeOut;
    private SpriteRenderer _renderer;
    private Collider2D _collider;

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();

        StartCoroutine(nameof(Moving));
    }

    private IEnumerator Moving()
    {
        yield return new WaitForSeconds(_delay);

        while (true)
        {
            yield return new WaitForSeconds(_timeIn);

            _collider.enabled = false;
            _renderer.color = new Color(0.28f, 0.28f, 0.28f);

            yield return new WaitForSeconds(_timeOut);

            _collider.enabled = true;
            _renderer.color = new Color(1, 1, 1);
        }
    }
}
