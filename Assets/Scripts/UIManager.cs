using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _ammoText;
    [SerializeField]
    private GameObject _coin;
    public void updateAmmo(int count)
    {
        _ammoText.text = "Ammo: " + count;
    }
    public void colectedCoin()
    {
        _coin.SetActive(true);
    }
    public void removedCoin()
    {
        _coin.SetActive(false);
    }
    public void textEnable()
    {
        _ammoText.gameObject.SetActive(true);
    }
}
