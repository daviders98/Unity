using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField]
    private AudioClip _coinSound;

    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
            
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Player _player = other.GetComponent<Player>();
            if (_player != null)
            {
                _player.giveCoin();
                AudioSource.PlayClipAtPoint(_coinSound, transform.position, 1f);
                UIManager uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
                if (uiManager != null)
                {
                    uiManager.colectedCoin();
                }
                Destroy(this.gameObject);
            }
        }
    }
}
