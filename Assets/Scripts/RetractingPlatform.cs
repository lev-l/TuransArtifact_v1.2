using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetractingPlatform : MonoBehaviour
{
    public AnimationCurve Retracting;
    [SerializeField] private bool _isReversed;
    [SerializeField] private float _timeUp;
    [SerializeField] private float _timeDown;
    private float _targetRetractedRotation;
    private Transform _transform;

    private void Start()
    {
        _targetRetractedRotation = _isReversed ? -90f : 90f;
        _transform = GetComponent<Transform>();

        StartCoroutine(nameof(Moving));
    }

    private IEnumerator Moving()
    {
        float zAxis;
        float currentTime;
        float maxTime = Retracting.keys[Retracting.length - 1].time;

        while (true)
        {
            // erect possition
            yield return new WaitForSeconds(_timeUp);


            currentTime = 0;
            while (currentTime < Retracting.keys[Retracting.length - 1].time)
            {
                yield return new WaitForEndOfFrame();
                zAxis = Retracting.Evaluate(currentTime) * _targetRetractedRotation;
                _transform.rotation = Quaternion.Euler(0, 0, zAxis);
                
                currentTime += Time.deltaTime;
            }
            _transform.rotation = Quaternion.Euler(0, 0, _targetRetractedRotation);

            // retracted position
            yield return new WaitForSeconds(_timeDown);


            currentTime = maxTime;
            while(currentTime > 0)
            {
                yield return new WaitForEndOfFrame();
                zAxis = Retracting.Evaluate(currentTime) * _targetRetractedRotation;
                _transform.rotation = Quaternion.Euler(0, 0, zAxis);

                currentTime -= Time.deltaTime;
            }
            _transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
