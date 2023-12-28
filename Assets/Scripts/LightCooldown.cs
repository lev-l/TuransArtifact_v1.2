using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCooldown : MonoBehaviour
{
    [SerializeField] private float _cooldown;
    private bool ready = true;

    public bool Use()
    {
        if (ready)
        {
            ready = false;
            StartCoroutine(nameof(Cooling));
            return true;
        }
        return false;
    }

    private IEnumerator Cooling()
    {
        yield return new WaitForSeconds(_cooldown);
        ready = true;
    }
}
