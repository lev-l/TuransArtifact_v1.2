using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenClose : MonoBehaviour
{
    public Transform MovableObject;
    public float Speed;
    public Vector3 OpenPosition;
    protected Vector3 _defoultPosition;
    protected bool _opened;

    protected virtual void Start()
    {
        _defoultPosition = MovableObject.position;
        _opened = false;
    }

    public void Move()
    {
        StopAllCoroutines();

        if (_opened)
        {
            StartCoroutine(MoveTo(_defoultPosition));
        }
        else
        {
            StartCoroutine(MoveTo(OpenPosition));
        }
    }

    public virtual IEnumerator MoveTo(Vector3 position)
    {
        _opened = !_opened;
        while (!IsEqual(MovableObject.position, position))
        {
            MovableObject.position = Vector3.MoveTowards(MovableObject.position,
                                                        position,
                                                        Speed * Time.deltaTime);
            yield return null;
        }
    }

    public bool IsEqual(Vector3 vector,
                        Vector3 vector1)
    {
        return IsBiggerOrSmaller(vector + new Vector3(0.025f, 0.025f), vector1)
                + IsBiggerOrSmaller(vector - new Vector3(0.025f, 0.025f), vector1)
                == 0;

    }

    public int IsBiggerOrSmaller(Vector3 bigger,
                            Vector3 smaller)
    {
        /// Returns -1 if "bigger" smaller, returns 1 if "bigger"
        /// is bigger and returns 0 if equal

        if (bigger.x + bigger.y
            > smaller.x + smaller.y)
        {
            return 1;
        }
        else if (bigger.x + bigger.y
                < smaller.x + smaller.y)
        {
            return -1;
        }
        else
        {
            return 0;
        }
    }
}
