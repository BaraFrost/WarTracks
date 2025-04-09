using UnityEngine;
using UnityEngine.UI; // Для работы с кнопками
using YG; // Подключение YandexGame SDK
using PlayerPrefs = RedefineYG.PlayerPrefs;

public class ShowAdForCoins : MonoBehaviour
{
    public string rewardID = "AddCoin"; // ID награды
    public int rewardAmount = 25; // Количество монет за просмотр рекламы
    private int coinValue;

    
    // Метод для вызова видео рекламы
    public void ShowRewardedAd()
    {

        YG2.RewardedAdvShow(rewardID, () =>
        {
            OnRewarded();
        });

        Debug.Log("Запущена реклама с ID: " + rewardID);
    }

    private void OnRewarded()
    {
        coinValue = PlayerPrefs.GetInt("Coin", 0);

        coinValue += rewardAmount;
        PlayerPrefs.SetInt("Coin", coinValue);
        PlayerPrefs.Save();
        Debug.Log("Начислено " + rewardAmount + " монет. Всего монет: " + coinValue);
    }
}