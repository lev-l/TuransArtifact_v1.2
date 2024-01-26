using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCooldown : MonoBehaviour
{
    [SerializeField] private float _cooldown;
    private Animator _animator;
    private bool ready = true;
    private bool _pureLight; // not a switch

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _pureLight = GetComponent<TimedSwitch>() == null;
    }

    public bool Use()
    {
        if (ready)
        {
            ready = false;
            StartCoroutine(nameof(Cooling));
            if (_pureLight)
            {
                _animator.Play("RopeAttached");
            }
            return true;
        }
        return false;
    }

    private IEnumerator Cooling()
    {
        yield return new WaitForSeconds(_cooldown);
        ready = true;
    }

    private void OnDisable()
    {
        ready = true;
    }
}
