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
