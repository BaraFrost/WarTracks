using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using YG;
using PlayerPrefs = RedefineYG.PlayerPrefs;

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

   
    
    public void BombBuy()
    {
        if (coinCounter >= bombPrice)
        {
            bombCount++;
            PlaySound(buyBombSound);
            coinCounter -= bombPrice;
            PlayerPrefs.SetInt("Bomb", bombCount);
            PlayerPrefs.SetInt("Coin", coinCounter);
            PlayerPrefs.Save();

            YG2.MetricaSend("coin_balance", new Dictionary<string, string>
        {
            { "bombs", bombCount.ToString() }
        });
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

        // ������ ��������� ������ ��� �����
        GameObject tempSoundObject = new GameObject("TempAudio");
        AudioSource tempAudioSource = tempSoundObject.AddComponent<AudioSource>();

        // ����������� ��������� �����
        tempAudioSource.clip = clip;
        tempAudioSource.volume = 0.35f; // ��������� ���������
        tempAudioSource.spatialBlend = 0; // 2D ����
        tempAudioSource.Play();

        // ���������� ������ ����� ��������������� �����
        Destroy(tempSoundObject, clip.length);
    }
}
