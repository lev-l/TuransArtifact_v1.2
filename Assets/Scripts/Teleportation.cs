using System.Collections;
using UnityEngine;

public class Teleportation : MonoBehaviour
{
    public Teleportation OtherTeleport;
    public Ables PlayerAbles;
    private bool _blocked;
    private GameObject _subject;

    private void Start()
    {
        _blocked = !PlayerAbles.Teleportation;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.E)
            && _subject
            && !_blocked)
        {
            _subject.transform.position = OtherTeleport.transform.position;
            _subject.GetComponent<Audio>().PlayTeleportation();
            _subject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            OtherTeleport.Block();
            _subject = null;
        }
    }

    public void Block()
    {
        _blocked = true;
        StartCoroutine(Unblocke());
    }

    public void Kill()
    {
        _blocked = true;
    }

    private IEnumerator Unblocke()
    {
        yield return new WaitForSeconds(0.5f);

        _blocked = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Audio>())
        {
            _subject = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Audio>())
        {
            _subject = null;
        }
    }
}
