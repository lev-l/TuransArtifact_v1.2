using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomataClose : MonoBehaviour
{
    void Start()
    {
        DeadMenu.Menu = gameObject;
        gameObject.SetActive(false);
    }
}
