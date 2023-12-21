using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerObserver : MonoBehaviour
{
    protected bool _playerInArea = false;

    public virtual void PlayerCame(bool ins)
    {
        // ins meens do player came in area or from it
        _playerInArea = ins;
    }
}

public delegate void Function();

public class Ghost : PlayerObserver
{
    private GameObject _mask;
    private Transform _self;
    private UnityMovement _player;
    private Rope _rope;
    private Rigidbody2D _playerRigidbody;
    private Sticking _stick;
    private ContactFilter2D _filter;
    private Function _turn;
    private bool _able;

    private void Start()
    {
        _mask = Camera.main.transform.GetChild(0).gameObject;
        _self = GetComponent<Transform>();
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<UnityMovement>();
        _rope = _player.GetComponent<Rope>();
        _playerRigidbody = _player.GetComponent<Rigidbody2D>();
        _stick = GetComponent<Sticking>();
        _turn = InGhost;

        _filter.useTriggers = false;
        _filter.SetLayerMask(Physics2D.GetLayerCollisionMask(9));

        _able = true;
    }

    private void Update()
    {
        if (_able
            && Input.GetKeyDown(KeyCode.Q)
            && _rope.enabled)
        {
            _turn();
            _able = false;
            Invoke(nameof(Able), 0.5f);
        }
    }

    public void Able()
    {
        _able = true;
    }

    public void InGhost()
    {
        if (_playerInArea)
        {
            _mask.SetActive(true);
            _player.enabled = false;
            _player.GetComponent<GhostMovement>().enabled = true;

            _stick.Stick();
            _playerRigidbody.gravityScale = 0;
            _playerRigidbody.velocity = Vector2.zero;
            _player.gameObject.layer = 12;
            _player.AudioSystem.PlayGhost();
            
            _turn = OutGhost;
        }
    }

    public void OutGhost()
    {
        RaycastHit2D[] buffer = new RaycastHit2D[1];
        int count = _playerRigidbody.Cast(Vector2.zero, _filter, buffer);

        if (count == 0)
        {
            _mask.SetActive(false);
            _player.enabled = true;
            _player.GetComponent<GhostMovement>().enabled = false;

            _stick.UnStick();
            _playerRigidbody.gravityScale = 1;
            _player.gameObject.layer = 9;
            _self.position = _player.transform.position;
            _player.AudioSystem.PlayGhost();

            _turn = InGhost;
        }
    }
}
