﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetractingSpikes : MonoBehaviour
{
    public AnimationCurve Retracting;
    [SerializeField] private float _delay;
    [SerializeField] private float _timeOut; // time the spikes are out
    [SerializeField] private float _timeIn;
    private Transform _transform;
    private Collider2D _collider;
    private Vector3 _initialPosition;

    private void Start()
    {
        _transform = GetComponent<Transform>();
        _collider = GetComponent<Collider2D>();
        _initialPosition = _transform.position;

        StartCoroutine(nameof(Moving));
    }

    private IEnumerator Moving()
    {
        float yPosition;
        float currentTime;
        float maxTime = Retracting.keys[Retracting.length - 1].time;
        yield return new WaitForSeconds(_delay);

        while (true)
        {
            yield return new WaitForSeconds(_timeOut);

            //Retracting
            currentTime = 0;
            while (currentTime < Retracting.keys[Retracting.length - 1].time)
            {
                yield return new WaitForEndOfFrame();
                yPosition = Retracting.Evaluate(currentTime);
                _transform.position = Vector3.up * yPosition + _initialPosition;

                currentTime += Time.deltaTime;
            }
            _collider.enabled = false;


            yield return new WaitForSeconds(_timeIn);

            //Deploying
            currentTime = maxTime;
            while (currentTime > 0)
            {
                yield return new WaitForEndOfFrame();
                yPosition = Retracting.Evaluate(currentTime);
                _transform.position = Vector3.up * yPosition + _initialPosition;

                currentTime -= Time.deltaTime;
            }
            _collider.enabled = true;
        }
    }
}
