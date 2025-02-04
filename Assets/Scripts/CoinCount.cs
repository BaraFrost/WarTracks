using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YG;
using PlayerPrefs = RedefineYG.PlayerPrefs;

public class CoinCount : MonoBehaviour
{
    public int coinValue;
    [SerializeField]
    private TMP_Text coin;
   
    void Update()
    {

        coinValue = PlayerPrefs.GetInt("Coin");
       
        coin.text = coinValue.ToString();

       
    }
}
