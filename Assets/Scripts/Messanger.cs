using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Messanger : MonoBehaviour
{
    public PlayerObserver[] Observers;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Message(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Message(false);
    }

    private void Message(bool info)
    {
        foreach (PlayerObserver observer in Observers)
        {
            observer.PlayerCame(info);
        }
    }
}
