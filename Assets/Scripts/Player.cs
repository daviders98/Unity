using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController _controller;
    [SerializeField]
    private float _speed = 3.5f;
    private float _gravity = 9.81f;
    [SerializeField]
    private GameObject _effect;
    [SerializeField]
    private GameObject _hitMarkerPrefab;
    private AudioSource _bullet;

    private int _currentAmmo;
    private int _maxAmmo = 50;
    private bool _doneReload = true;
    private UIManager uiManager;
    public bool _hasCoin = false;
    [SerializeField]
    private GameObject _weapon;
    private bool haveWeapon = false;
    // Start is called before the first frame update
    void Start()
    {
        //hide mouse cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        _controller = GetComponent<CharacterController>();
        _bullet = GetComponent<AudioSource>();
        if (_bullet == null)
        {
            Debug.LogError("no hay cancion");
        }
        _currentAmmo = _maxAmmo;
        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && _currentAmmo > 0 && _doneReload == true && haveWeapon)
        {
            shoot();
        }
        else
        {
            _bullet.Stop();
            _effect.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        calculateMovement();
        if (Input.GetKeyDown(KeyCode.R) && _doneReload == true)
        {
            _doneReload = false;
            StartCoroutine(reload());
        }
    }
    void calculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput);
        Vector3 velocity = direction * _speed;
        velocity.y -= _gravity;
        velocity = transform.TransformDirection(velocity);
        _controller.Move(velocity * Time.deltaTime);
    }
    void shoot()
    {
        _effect.SetActive(true);
        _currentAmmo--;
        uiManager.updateAmmo(_currentAmmo);
        if (_bullet.isPlaying == false)
        {
            _bullet.Play();
        }
        Ray rayOrigin = Camera.main.ViewportPointToRay(new Vector3(.5f, .5f, 0));
        RaycastHit hitInfo;
        if (Physics.Raycast(rayOrigin, out hitInfo))
        {
            Debug.Log("hit: " + hitInfo);
            GameObject hitmark = Instantiate(_hitMarkerPrefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
            StartCoroutine(deleteHitmark(hitmark));
            Destructable crate = hitInfo.transform.GetComponent<Destructable>();
            if (crate != null)
            {
                crate.destroyCrate();
            }
        }
    }
    IEnumerator deleteHitmark(GameObject hit)
    {
        yield return new WaitForSeconds(1f);
        Destroy(hit);
    }
    IEnumerator reload()
    {
        yield return new WaitForSeconds(1.5f);
        _currentAmmo = _maxAmmo;
        uiManager.updateAmmo(_currentAmmo);
        _doneReload = true;
    }
    public void giveCoin()
    {
        _hasCoin = true;
    }
    public void removeCoin()
    {
        _hasCoin = false;
        uiManager.removedCoin();
    }
    public void enableWeapon()
    {
        haveWeapon = true;
        _weapon.SetActive(true);
        uiManager.textEnable();
    }
}
