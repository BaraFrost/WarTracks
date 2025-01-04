using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinEarn : MonoBehaviour
{
    public int coin;
    [SerializeField]
    private int coinEarn;
    
    [SerializeField]
    private int coinValues;
    // Start is called before the first frame update
    void Start()
    {
        coinValues = PlayerPrefs.GetInt("Coin");
    }

    public void OnCoinEarn()
    {
       coinValues += coinEarn;
        PlayerPrefs.SetInt("Coin", coinValues);
        PlayerPrefs.Save();
    }

    // Update is called once per frame
    void Update()
    {
        coinValues = PlayerPrefs.GetInt("Coin");
    }
}
