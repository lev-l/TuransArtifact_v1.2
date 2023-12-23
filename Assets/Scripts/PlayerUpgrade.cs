using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgrade : MonoBehaviour
{
    public Ables SelfAbles;

    void Start()
    {
        GetComponent<Flying>().enabled = SelfAbles.Fly;
    }
}
