using System.Collections;
using UnityEngine;

public class TimedSwitch : MonoBehaviour
{
    [SerializeField] GameObject[] _linkedObjects;
    [SerializeField] float _activatedTime;
    private bool _notActive;

    public void Activate()
    {
        if (_notActive)
        {
            foreach (GameObject gameObject in _linkedObjects)
            {
                gameObject.SetActive(true);
            }
            StartCoroutine(nameof(Disactivate));
        }
    }

    public IEnumerator Disactivate()
    {
        yield return new WaitForSeconds(_activatedTime);

        foreach(GameObject gameObject in _linkedObjects)
        {
            gameObject.SetActive(false);
        }
    }
}