using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinCount : MonoBehaviour
{
    public int coinValue;
    [SerializeField]
    private TMP_Text coin;
    void Start()
    {
        coinValue = PlayerPrefs.GetInt("Coin");

    }

    public void CoinValue()
    {
        coinValue = PlayerPrefs.GetInt("Coin");
    }
    // Update is called once per frame
    void Update()
    {
       /* if (coinValue <= 0)
        {
            coinValue = 0;
        }*/
        PlayerPrefs.SetInt("Coin", coinValue);
        PlayerPrefs.Save();
        coin.text = coinValue.ToString();
    }
}
