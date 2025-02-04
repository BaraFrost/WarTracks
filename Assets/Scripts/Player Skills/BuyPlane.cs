using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;
using PlayerPrefs = RedefineYG.PlayerPrefs;

public class BuyPlane : MonoBehaviour
{
    [SerializeField]
    private int planePrice;
    [SerializeField]
    private int planeCount;
    [SerializeField]
    private int coinCounter;
    [SerializeField]
    private AudioClip buyPlaneSound;

    void Start()
    {
        planeCount = PlayerPrefs.GetInt("Plane");
        coinCounter = PlayerPrefs.GetInt("Coin");
    }

    public void PlaneBuy()
    {
        if (coinCounter >= planePrice)
        {
            planeCount++;
            PlaySound(buyPlaneSound);
            coinCounter -= planePrice;
            PlayerPrefs.SetInt("Plane", planeCount);
            PlayerPrefs.SetInt("Coin", coinCounter);
            PlayerPrefs.Save();

            YG2.MetricaSend("coin_balance", new Dictionary<string, string>
        {
            { "plane", planeCount.ToString() }
        });
        }
    }

    private void Update()
    {
        planeCount = PlayerPrefs.GetInt("Plane");
        coinCounter = PlayerPrefs.GetInt("Coin");
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
}
