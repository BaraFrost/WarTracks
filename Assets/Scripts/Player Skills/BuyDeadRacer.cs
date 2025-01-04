using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void Start()
    {
        dRCount = PlayerPrefs.GetInt("DR");
        coinCounter = PlayerPrefs.GetInt("Coin");
    }

    public void DRBuy()
    {
        if (coinCounter >= dRPrice)
        {
            dRCount++;
            PlaySound(buyDRSound);
            coinCounter -= dRPrice;
            PlayerPrefs.SetInt("DR", dRCount);
            PlayerPrefs.Save();
            PlayerPrefs.SetInt("Coin", coinCounter);
            PlayerPrefs.Save();
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
