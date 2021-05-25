using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkShop : MonoBehaviour
{
    [SerializeField]
    public AudioSource _winSound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //else, debug get out of here
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Player _player = other.GetComponent<Player>();
                if (_player._hasCoin)
                {
                    _player.removeCoin();
                    _winSound.Play();
                    _player.enableWeapon();
                }
                else
                {
                    Debug.Log("getouttaherexd");
                }
            }
        }
    }
}
