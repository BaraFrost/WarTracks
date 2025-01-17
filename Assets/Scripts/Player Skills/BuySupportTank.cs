using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuySupportTank : MonoBehaviour
{
    [SerializeField]
    private int sTPrice;
    [SerializeField]
    private int sTCount;
    [SerializeField]
    private int coinCounter;
    [SerializeField]
    private AudioClip buySTSound;

    void Start()
    {
        sTCount = PlayerPrefs.GetInt("ST");
        coinCounter = PlayerPrefs.GetInt("Coin");
    }

    public void STBuy()
    {
        if (coinCounter >= sTPrice)
        {
            sTCount++;
            PlaySound(buySTSound);
            coinCounter -= sTPrice;
            PlayerPrefs.SetInt("ST", sTCount);
            PlayerPrefs.Save();
            PlayerPrefs.SetInt("Coin", coinCounter);
            PlayerPrefs.Save();
        }
    }

    private void Update()
    {
        sTCount = PlayerPrefs.GetInt("ST");
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
