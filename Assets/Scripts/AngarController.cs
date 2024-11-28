using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AngarController : MonoBehaviour
{
    [SerializeField]
    private int price12;
    [SerializeField]
    private int price13;
    [SerializeField]
    private int price22;
    [SerializeField]
    private int price23;

    [SerializeField]
    private int tank12Bue;
    [SerializeField]
    private int tank22Bue;
    [SerializeField]
    private int tank13Bue;
    [SerializeField]
    private int tank23Bue;

    public int goBuy = 0;

    [SerializeField]
    private NextLevel nextLevel;

    [SerializeField]
    private int coinValue;

    void Start()
    {
        tank12Bue = PlayerPrefs.GetInt("12TankBue");
        tank22Bue = PlayerPrefs.GetInt("22TankBue");
        tank13Bue = PlayerPrefs.GetInt("13TankBue");
        tank23Bue = PlayerPrefs.GetInt("23TankBue");
        coinValue = PlayerPrefs.GetInt("Coin");
    }

    void Update()
    {
        coinValue = PlayerPrefs.GetInt("Coin");

        if (nextLevel.enter == 1) // Проверяем, что nextLevel.enter == 1
        {
            if (tank12Bue == 0 && coinValue >= price12)
            {
                goBuy = 1;
                PlayerPrefs.SetInt("NextLevel", goBuy);
                PlayerPrefs.Save();
            }
            else if (tank13Bue == 0 && coinValue >= price13)
            {
                goBuy = 1;
                PlayerPrefs.SetInt("NextLevel", goBuy);
                PlayerPrefs.Save();
            }

        }
    }
}
