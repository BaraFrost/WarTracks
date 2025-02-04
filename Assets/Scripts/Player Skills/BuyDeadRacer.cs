using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;
using PlayerPrefs = RedefineYG.PlayerPrefs;

public class BuyDeadRacer : MonoBehaviour
{
    [SerializeField]
    private int dRPrice;
    [SerializeField]
    private int dRCount;
    [SerializeField]
    private int coinCounter;
    [SerializeField]
    private AudioClip buyDRSound;

   

    public void DRBuy()
    {
        if (coinCounter >= dRPrice)
        {
            dRCount++;
            PlaySound(buyDRSound);
            coinCounter -= dRPrice;
            PlayerPrefs.SetInt("DR", dRCount);
            
            PlayerPrefs.SetInt("Coin", coinCounter);
            PlayerPrefs.Save();

            YG2.MetricaSend("coin_balance", new Dictionary<string, string>
        {
            { "DR", dRCount.ToString() }
        });
        }
    }

    private void Update()
    {
        dRCount = PlayerPrefs.GetInt("DR");
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
