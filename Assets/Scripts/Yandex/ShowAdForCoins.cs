using UnityEngine;
using UnityEngine.UI; // ��� ������ � ��������
using YG; // ����������� YandexGame SDK
using PlayerPrefs = RedefineYG.PlayerPrefs;

public class ShowAdForCoins : MonoBehaviour
{
    public string rewardID = "AddCoin"; // ID �������
    public int rewardAmount = 25; // ���������� ����� �� �������� �������
    private int coinValue;

    void Start()
    {
        // ��������� ���������� ����� �� PlayerPrefs
        coinValue = PlayerPrefs.GetInt("Coin", 0);
    }

    // ����� ��� ������ ����� �������
    public void ShowRewardedAd()
    {
        YG2.RewardedAdvShow(rewardID, () =>
        {
            OnRewarded();
        });

        Debug.Log("�������� ������� � ID: " + rewardID);
    }

    private void OnRewarded()
    {
        coinValue += rewardAmount;
        PlayerPrefs.SetInt("Coin", coinValue);
        PlayerPrefs.Save();
        Debug.Log("��������� " + rewardAmount + " �����. ����� �����: " + coinValue);
    }
}