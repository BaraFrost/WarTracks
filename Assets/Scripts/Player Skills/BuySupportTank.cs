using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;
using PlayerPrefs = RedefineYG.PlayerPrefs;

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

    

    public void STBuy()
    {
        if (coinCounter >= sTPrice)
        {
            sTCount++;
            PlaySound(buySTSound);
            coinCounter -= sTPrice;
            PlayerPrefs.SetInt("ST", sTCount);
            PlayerPrefs.SetInt("Coin", coinCounter);
            PlayerPrefs.Save();

            YG2.MetricaSend("coin_balance", new Dictionary<string, string>
        {
            { "ST", sTCount.ToString() }
        });
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
