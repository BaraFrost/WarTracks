using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;
using PlayerPrefs = RedefineYG.PlayerPrefs;

public class BuyTank : MonoBehaviour
{
    [SerializeField]
    private int coinValue;

    public int tank12Bue = 0;

    public int tank22Bue = 0;

    public int tank13Bue = 0;

    public int tank23Bue = 0;

    [SerializeField]
    private GameObject value12Tank;
    [SerializeField]
    private GameObject value22Tank;
    [SerializeField]
    private GameObject value13Tank;
    [SerializeField]
    private GameObject value23Tank;

    [SerializeField]
    private GameObject value12TankImage;
    [SerializeField]
    private GameObject value22TankImage;
    [SerializeField]
    private GameObject value13TankImage;
    [SerializeField]
    private GameObject value23TankImage;
    [SerializeField]
    private int price12;
    [SerializeField]
    private int price13;
    [SerializeField]
    private int price22;
    [SerializeField]
    private int price23;
    [SerializeField]
    private int coinRemains;
    [SerializeField]
    private int tankPrice;
    [SerializeField]
    private int coin;
    [SerializeField]
    private int open13 = 0;
    [SerializeField]
    private int open23 = 0;

    [SerializeField]
    private Image tank12;
    [SerializeField]
    private Image tank121;
    [SerializeField]
    private int shadow12 = 0;
    [SerializeField]
    private int tank12ShadowOff;

    [SerializeField]
    private Image tank22;
    [SerializeField]
    private Image tank221;
    [SerializeField]
    private int shadow22 = 0;
    [SerializeField]
    private int tank22ShadowOff;

    [SerializeField]
    private Image tank13;
    [SerializeField]
    private Image tank131;
    [SerializeField]
    private int shadow13 = 0;
    [SerializeField]
    private int tank13ShadowOff;

    [SerializeField]
    private Image tank23;
    [SerializeField]
    private Image tank231;
    [SerializeField]
    private int shadow23 = 0;
    [SerializeField]
    private int tank23ShadowOff;

    [SerializeField]
    private AudioClip buyTankSound;
    [SerializeField]
    private AudioClip giveMoneySound;

    void Start()
    {
        // giveMoneySound = GetComponent<AudioSource>();
        open13 = PlayerPrefs.GetInt("13Open");
        open23 = PlayerPrefs.GetInt("23Open");
        tank12Bue = PlayerPrefs.GetInt("12TankBue");
        tank22Bue = PlayerPrefs.GetInt("22TankBue");
        tank13Bue = PlayerPrefs.GetInt("13TankBue");
        tank23Bue = PlayerPrefs.GetInt("23TankBue");
        shadow12 = PlayerPrefs.GetInt("12ShadowOff");
        shadow13 = PlayerPrefs.GetInt("13ShadowOff");
        shadow22 = PlayerPrefs.GetInt("22ShadowOff");
        shadow23 = PlayerPrefs.GetInt("23ShadowOff");

        coinValue = PlayerPrefs.GetInt("Coin");

        if (shadow12 == 1)
        {
            tank12.color = new Color(1, 1, 1, 1);
            tank121.color = new Color(1, 1, 1, 1);
        }
        else if (shadow12 == 0)
        {
            tank12.color = new Color(0.4339623f, 0.4339623f, 0.4339623f, 1f);
            tank121.color = new Color(0.4339623f, 0.4339623f, 0.4339623f, 1f);
        }
        if (shadow13 == 1)
        {
            tank13.color = new Color(1, 1, 1, 1);
            tank131.color = new Color(1, 1, 1, 1);
        }
        else if (shadow13 == 0)
        {
            tank13.color = new Color(0.4339623f, 0.4339623f, 0.4339623f, 1f);
            tank131.color = new Color(0.4339623f, 0.4339623f, 0.4339623f, 1f);
        }
        if (shadow22 == 1)
        {
            tank22.color = new Color(1, 1, 1, 1);
            tank221.color = new Color(1, 1, 1, 1);
        }
        else if (shadow22 == 0)
        {
            tank22.color = new Color(0.4339623f, 0.4339623f, 0.4339623f, 1f);
            tank221.color = new Color(0.4339623f, 0.4339623f, 0.4339623f, 1f);
        }
        if (shadow23 == 1)
        {
            tank23.color = new Color(1, 1, 1, 1);
            tank231.color = new Color(1, 1, 1, 1);
        }
        else if (shadow23 == 0)
        {
            tank23.color = new Color(0.4339623f, 0.4339623f, 0.4339623f, 1f);
            tank231.color = new Color(0.4339623f, 0.4339623f, 0.4339623f, 1f);
        }
    }
    void Update()
    {
        tank12Bue = PlayerPrefs.GetInt("12TankBue");
        tank22Bue = PlayerPrefs.GetInt("22TankBue");
        tank13Bue = PlayerPrefs.GetInt("13TankBue");
        tank23Bue = PlayerPrefs.GetInt("23TankBue");
        shadow12 = PlayerPrefs.GetInt("12ShadowOff");
        shadow13 = PlayerPrefs.GetInt("13ShadowOff");
        shadow22 = PlayerPrefs.GetInt("22ShadowOff");
        shadow23 = PlayerPrefs.GetInt("23ShadowOff");
        coinValue = PlayerPrefs.GetInt("Coin");

        

        if (shadow12 == 1)
        {
            tank12.color = new Color(1, 1, 1, 1);
            tank121.color = new Color(1, 1, 1, 1);
        }
        else if (shadow12 == 0)
        {
            tank12.color = new Color(0.4339623f, 0.4339623f, 0.4339623f, 1f);
            tank121.color = new Color(0.4339623f, 0.4339623f, 0.4339623f, 1f);
        }

        if (shadow13 == 1)
        {
            tank13.color = new Color(1, 1, 1, 1);
            tank131.color = new Color(1, 1, 1, 1);
        }
        else if (shadow13 == 0)
        {
            tank13.color = new Color(0.4339623f, 0.4339623f, 0.4339623f, 1f);
            tank131.color = new Color(0.4339623f, 0.4339623f, 0.4339623f, 1f);
        }
        if (shadow22 == 1)
        {
            tank22.color = new Color(1, 1, 1, 1);
            tank221.color = new Color(1, 1, 1, 1);
        }
        else if (shadow22 == 0)
        {
            tank22.color = new Color(0.4339623f, 0.4339623f, 0.4339623f, 1f);
            tank221.color = new Color(0.4339623f, 0.4339623f, 0.4339623f, 1f);
        }
        if (shadow23 == 1)
        {
            tank23.color = new Color(1, 1, 1, 1);
            tank231.color = new Color(1, 1, 1, 1);
        }
        else if (shadow23 == 0)
        {
            tank23.color = new Color(0.4339623f, 0.4339623f, 0.4339623f, 1f);
            tank231.color = new Color(0.4339623f, 0.4339623f, 0.4339623f, 1f);
        }
        if (tank12Bue == 1)
        {
            value12Tank.SetActive(false);
            value12TankImage.SetActive(false);
        }
        if (tank22Bue == 1)
        {
            value22Tank.SetActive(false);
            value22TankImage.SetActive(false);
        }
        if (tank13Bue == 1)
        {
            value13Tank.SetActive(false);
            value13TankImage.SetActive(false);
        }
        if (tank23Bue == 1)
        {
            value23Tank.SetActive(false);
            value23TankImage.SetActive(false);
        }
    }
    // Update is called once per frame
    void UpdateMoney()
    {
        coin = coinValue;
        coinRemains = coin - tankPrice;



    }
    public void On12TankBue()
    {
        tankPrice = price12;
        UpdateMoney();
        if (tank12Bue == 0 && coinRemains >= 0)
        {

            shadow12 = 1;
            PlayerPrefs.SetInt("12ShadowOff", shadow12);
            tank12.color = new Color(1, 1, 1, 1);
            tank121.color = new Color(1, 1, 1, 1);
            tank12Bue = 1;
            coinValue -= price12;
            PlayerPrefs.SetInt("Coin", coinValue);
            PlayerPrefs.SetInt("12TankBue", tank12Bue);
            open13 = 1;
            PlayerPrefs.SetInt("13Open", open13);
            PlayerPrefs.Save();
            PlaySound(buyTankSound);

            YG2.MetricaSend("coin_balance", new Dictionary<string, string>
        {
            { "12TankBue", tank12Bue.ToString() }
        });
        }
    }
    public void On22TankBue()
    {


        tankPrice = price22;
        UpdateMoney();
        if (tank22Bue == 0 && coinRemains >= 0)
        {
            shadow22 = 1;
            PlayerPrefs.SetInt("22ShadowOff", shadow22);
            tank22.color = new Color(1, 1, 1, 1);
            tank221.color = new Color(1, 1, 1, 1);
            coinValue -= price22;
            PlayerPrefs.SetInt("Coin", coinValue);
            tank22Bue = 1;
            PlayerPrefs.SetInt("22TankBue", tank22Bue);
            open23 = 1;
            PlayerPrefs.SetInt("23Open", open23);
            PlayerPrefs.Save();
            value22Tank.SetActive(false);
            PlaySound(buyTankSound);
            YG2.MetricaSend("coin_balance", new Dictionary<string, string>
        {
            
            { "22TankBue", tank22Bue.ToString() }
            
        });
        }
    }
    public void On13TankBue()
    {

        tankPrice = price13;
        UpdateMoney();
        if (tank13Bue == 0 && coinRemains >= 0 && open13 == 1)
        {
            shadow13 = 1;
            PlayerPrefs.SetInt("13ShadowOff", shadow13);
            tank13.color = new Color(1, 1, 1, 1);
            tank131.color = new Color(1, 1, 1, 1);
            coinValue -= price13;
            PlayerPrefs.SetInt("Coin", coinValue);
            tank13Bue = 1;
            PlayerPrefs.SetInt("13TankBue", tank13Bue);
            PlayerPrefs.Save();
            PlaySound(buyTankSound);

            YG2.MetricaSend("coin_balance", new Dictionary<string, string>
        {
            
            { "13TankBue", tank13Bue.ToString() }
            
        });
        }
    }
    public void On23TankBue()
    {

        tankPrice = price23;
        UpdateMoney();
        if (tank23Bue == 0 && coinRemains >= 0 && open23 == 1)
        {
            shadow23 = 1;
            PlayerPrefs.SetInt("23ShadowOff", shadow23);
            tank23.color = new Color(1, 1, 1, 1);
            tank231.color = new Color(1, 1, 1, 1);
            coinValue -= price23;
            PlayerPrefs.SetInt("Coin", coinValue);
            tank23Bue = 1;
            PlayerPrefs.SetInt("23TankBue", tank23Bue);
            PlayerPrefs.Save();
            PlaySound(buyTankSound);


            YG2.MetricaSend("coin_balance", new Dictionary<string, string>
        {
            
            { "23TankBue", tank23Bue.ToString() }
        });
        }
    }
    public void Give25()
    {
        coinValue += 25;
        PlaySoundGive(giveMoneySound);

        PlayerPrefs.SetInt("Coin", coinValue);
        PlayerPrefs.Save();
    }
    public void DeliteAllTanks()
    {
        PlayerPrefs.DeleteKey("12TankBue");
        PlayerPrefs.DeleteKey("13TankBue");
        PlayerPrefs.DeleteKey("22TankBue");
        PlayerPrefs.DeleteKey("23TankBue");
        shadow12 = 0;
        PlayerPrefs.SetInt("12ShadowOff", shadow12);
        shadow22 = 0;
        PlayerPrefs.SetInt("22ShadowOff", shadow22);
        shadow13 = 0;
        PlayerPrefs.SetInt("13ShadowOff", shadow13);
        shadow23 = 0;
        PlayerPrefs.SetInt("23ShadowOff", shadow23);
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip == null) return;

        // Создаём временный объект для звука
        GameObject tempSoundObject = new GameObject("TempAudio");
        AudioSource tempAudioSource = tempSoundObject.AddComponent<AudioSource>();

        // Настраиваем параметры звука
        tempAudioSource.clip = clip;
        tempAudioSource.volume = 0.35f; // Настройте громкость
        tempAudioSource.spatialBlend = 0; // 2D звук
        tempAudioSource.Play();

        // Уничтожаем объект после воспроизведения звука
        Destroy(tempSoundObject, clip.length);
    }

    private void PlaySoundGive(AudioClip clip)
    {
        if (clip == null) return;

        // Создаём временный объект для звука
        GameObject tempSoundObject = new GameObject("TempAudio");
        AudioSource tempAudioSource = tempSoundObject.AddComponent<AudioSource>();

        // Настраиваем параметры звука
        tempAudioSource.clip = clip;
        tempAudioSource.volume = 0.15f; // Настройте громкость
        tempAudioSource.spatialBlend = 0; // 2D звук
        tempAudioSource.Play();

        // Уничтожаем объект после воспроизведения звука
        Destroy(tempSoundObject, clip.length);
    }
}
