using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileToTarget : MonoBehaviour
{
    public float EndTile;
    private SpriteRenderer _renderer;
    private Transform _end;
    private Transform _self;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _self = GetComponent<Transform>();
        _end = _self.GetChild(0);
    }

    public void Tile(Vector3 target, float max)
    {
        Vector2 vectorDistance = _self.position - target;
        float distance = Mathf.Abs(vectorDistance.magnitude);
        if(distance <= max)
        {
            _renderer.size = new Vector2(distance, _renderer.size.y);
            _end.localPosition = new Vector2(distance + EndTile, 0);
        }
        else
        {
            _renderer.size = new Vector2(3, _renderer.size.y);
            _end.localPosition = new Vector2(3 + EndTile, 0);
        }
    }

    private Vector2 RoundVector(Vector2 vector)
    {
        Vector2 result = vector;

        result.x = Mathf.Abs(vector.x);
        result.y = Mathf.Abs(vector.y);

        return result;
    }
}
