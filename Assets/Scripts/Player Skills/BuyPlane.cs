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
