using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectOpenClose : OpenClose
{
    public RectTransform MovableRectObject;

    protected override void Start()
    {
        _defoultPosition = MovableRectObject.anchoredPosition;
        _opened = false;
    }

    public override IEnumerator MoveTo(Vector3 position)
    {
        _opened = !_opened;
        while (!IsEqual(MovableObject.position, position))
        {
            MovableRectObject.anchoredPosition = Vector3.MoveTowards(MovableRectObject.anchoredPosition,
                                                        position,
                                                        Speed * Time.deltaTime);
            yield return null;
        }
    }
}
