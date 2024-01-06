using System.Collections;
using UnityEngine;

[RequireComponent(typeof(LightCooldown))]
public class TimedSwitch : MonoBehaviour
{
    [SerializeField] Sprite _activatedSprite, _unactivatedSprite;
    [SerializeField] GameObject[] _linkedObjects;
    [SerializeField] GameObject[] _linkedObjectsToDisactivate;
    [SerializeField] float _activatedTime;
    [SerializeField] bool _timed;
    private SpriteRenderer _renderer;
    private bool _notActive;

    private void Start()
    {
        _notActive = true;
        _renderer = GetComponent<SpriteRenderer>();
    }

    public void Activate()
    {
        if (_notActive)
        {
            foreach (GameObject gameObject in _linkedObjects)
            {
                gameObject.SetActive(true);
            }
            foreach (GameObject disObject in _linkedObjectsToDisactivate)
            {
                disObject.SetActive(false);
            }

            _renderer.sprite = _activatedSprite;
            _notActive = false;

            if (_timed)
            {
                StartCoroutine(nameof(Disactivate));
            }
        }
    }

    public IEnumerator Disactivate()
    {
        yield return new WaitForSeconds(_activatedTime);

        foreach(GameObject gameObject in _linkedObjects)
        {
            gameObject.SetActive(false);
        }
        foreach (GameObject disObject in _linkedObjectsToDisactivate)
        {
            disObject.SetActive(true);
        }

        _renderer.sprite = _unactivatedSprite;
        _notActive = true;
    }
}
