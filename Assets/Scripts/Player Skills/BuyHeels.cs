using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyHeels : MonoBehaviour
{
    [SerializeField]
    private int heelPrice;
    [SerializeField]
    private int heelCount;
    [SerializeField]
    private int coinCounter;
    [SerializeField]
    private AudioClip buyHeelSound;

    void Start()
    {
        heelCount = PlayerPrefs.GetInt("Heel");
        coinCounter = PlayerPrefs.GetInt("Coin");
    }

    public void HeelBuy()
    {
        if (coinCounter >= heelPrice)
        {
            heelCount++;
            PlaySound(buyHeelSound);
            coinCounter -= heelPrice;
            PlayerPrefs.SetInt("Heel", heelCount);
            PlayerPrefs.Save();
            PlayerPrefs.SetInt("Coin", coinCounter);
            PlayerPrefs.Save();
        }
    }

    private void Update()
    {
        heelCount = PlayerPrefs.GetInt("Heel");
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
