using System.Collections;
using UnityEngine;

public class RopeAnimation : MonoBehaviour
{
    private Animator _animator;
    private RotateToTarget _rotator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rotator = GetComponent<RotateToTarget>();
    }

    public IEnumerator RopeAttaching(Vector2 target)
    {
        _animator.Play("Rope");
        _rotator.Rotate(target);
        yield return new WaitForSeconds(0.1f);
        _animator.Play("IdleRope");
    }
}