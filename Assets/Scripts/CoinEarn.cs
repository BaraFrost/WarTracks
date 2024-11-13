using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinEarn : MonoBehaviour
{
    public int coin;
    [SerializeField]
    private int coinEarn;
    
    [SerializeField]
    private CoinCount value;
    // Start is called before the first frame update
    void Start()
    {
        value = FindObjectOfType<CoinCount>();
    }

    public void OnCoinEarn()
    {
        value.coinValue += coinEarn;
        coin = value.coinValue;
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
