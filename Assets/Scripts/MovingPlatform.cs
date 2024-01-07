using System.Collections;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Vector3 _firstPoint, _secondPoint;
    [SerializeField] private float _speed, _delay;
    private Transform _transform;

    private void Start()
    {
        _transform = GetComponent<Transform>();

        StartCoroutine(nameof(Moving));
    }

    private IEnumerator Moving()
    {
        while (true)
        {
            while (_transform.position != _firstPoint)
            {
                yield return new WaitForEndOfFrame();
                MoveTo(_firstPoint);
            }
            yield return new WaitForSeconds(_delay);

            while (_transform.position != _secondPoint)
            {
                yield return new WaitForEndOfFrame();
                MoveTo(_secondPoint);
            }
            yield return new WaitForSeconds(_delay);
        }
    }

    private void MoveTo(Vector3 position)
    {
        _transform.position = Vector3.MoveTowards
                                        (_transform.position,
                                        position,
                                        _speed * Time.deltaTime
                                        );
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.transform.SetParent(_transform);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null);
    }
}
