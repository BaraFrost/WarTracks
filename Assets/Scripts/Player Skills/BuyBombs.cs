using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BuyBombs : MonoBehaviour
{
    [SerializeField]
    private int bombPrice;
    [SerializeField]
    private int bombCount;
    [SerializeField]
    private int coinCounter;
    [SerializeField]
    private AudioClip buyBombSound;

    void Start()
    {
        bombCount = PlayerPrefs.GetInt("Bomb");
        coinCounter = PlayerPrefs.GetInt("Coin");
    }
    
    public void BombBuy()
    {
        if (coinCounter >= bombPrice)
        {
            bombCount++;
            PlaySound(buyBombSound);
            coinCounter -= bombPrice;
            PlayerPrefs.SetInt("Bomb", bombCount);
            PlayerPrefs.Save();
            PlayerPrefs.SetInt("Coin", coinCounter);
            PlayerPrefs.Save();
        }
    }

    private void Update()
    {
        bombCount = PlayerPrefs.GetInt("Bomb");
        coinCounter = PlayerPrefs.GetInt("Coin");
    }
    // Update is called once per frame
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
