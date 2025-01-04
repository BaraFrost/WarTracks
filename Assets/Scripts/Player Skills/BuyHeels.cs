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
