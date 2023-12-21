using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToTarget : MonoBehaviour
{
    private Transform _self;

    private void Awake()
    {
        _self = GetComponent<Transform>();
    }

    public void Rotate (Vector3 target)
    {
        Vector3 dir = target - _self.position;
        float angle = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);
        _self.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
