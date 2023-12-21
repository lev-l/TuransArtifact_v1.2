using System.Collections;
using UnityEngine;

public class Throw : PlayerObserver
{
    public KeyCode Key;
    public float Max;
    private GameObject _arrow;
    private Transform _arrowTip;
    private Transform _self;
    private Rigidbody2D _rigidbody;
    private Sticking _stick;
    private RotateToTarget _arrowRotater;
    private TileToTarget _arrowTiler;
    private Camera _camera;

    void Start()
    {
        _arrow = transform.GetChild(1).gameObject;
        _arrowTip = _arrow.transform.GetChild(0);
        _rigidbody = GetComponent<Rigidbody2D>();
        _self = GetComponent<Transform>();
        _arrowRotater = _arrow.GetComponent<RotateToTarget>();
        _arrowTiler = _arrow.GetComponent<TileToTarget>();
        _stick = GetComponent<Sticking>();
        _arrow.SetActive(false);
        _camera = Camera.main;
    }

    void Update()
    {
        if (_playerInArea)
        {
            if (Input.GetKey(Key))
            {
                _arrow.SetActive(true);
                Vector2 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
                _arrowRotater.Rotate(mousePosition);
                _arrowTiler.Tile(mousePosition, Max);
            }
            if (Input.GetKeyUp(Key))
            {
                _arrow.SetActive(false);
                _stick.UnStick();
                _rigidbody.AddForce((_arrowTip.position - _self.position) * 5, ForceMode2D.Impulse);
            }
        }
    }

    public override void PlayerCame(bool ins)
    {
        // ins meens do player came in area or from it
        _playerInArea = ins;
        if (_arrow.activeSelf)
        {
            _arrow.SetActive(ins);
        }
    }
}
