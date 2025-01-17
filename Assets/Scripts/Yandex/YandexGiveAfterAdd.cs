using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YandexGiveAfterAdd : MonoBehaviour
{
    // ID рекламы
    private const int AdID = 1;

    // События видео-рекламы
    public delegate void VideoEvent();
    public static event VideoEvent OpenVideoEvent;
    public static event VideoEvent CloseVideoEvent;
    public static event VideoEvent ErrorVideoEvent;
    public static event System.Action<int> RewardVideoEvent;
    private BuyTank buyTank;
    void Start()
    {
        // Подписываемся на события
        CloseVideoEvent += OnAdClosed;
        RewardVideoEvent += OnAdRewarded;
        ErrorVideoEvent += OnAdError;
    }

    void OnDestroy()
    {
        // Отписываемся от событий
        CloseVideoEvent -= OnAdClosed;
        RewardVideoEvent -= OnAdRewarded;
        ErrorVideoEvent -= OnAdError;
    }

    // Вызов рекламы
    public void ShowAd()
    {
        Debug.Log("Показываем рекламу");
        RewVideoShow(AdID); // Запускаем рекламу с заданным ID
    }

    // Метод вызова рекламы
    private void RewVideoShow(int id)
    {
        // Здесь вызывается платформа показа рекламы
        Debug.Log($"Реклама запущена с ID: {id}");
        OpenVideoEvent?.Invoke();
        // Эмуляция завершения рекламы (удалить в реальной интеграции)
        Invoke(nameof(SimulateAdClose), 3f); // Закрытие рекламы через 3 секунды
    }

    // Симуляция завершения рекламы (только для тестов)
    private void SimulateAdClose()
    {
        Debug.Log("Реклама закрыта");
        CloseVideoEvent?.Invoke();
        RewardVideoEvent?.Invoke(AdID);
    }

    // Метод выдачи награды
    private void OnAdRewarded(int id)
    {
        if (id == AdID)
        {
            buyTank.Give25(); // Вызываем метод вознаграждения
        }
    }

    // Метод при закрытии рекламы
    private void OnAdClosed()
    {
        Debug.Log("Реклама закрыта, обработка завершена.");
    }

    // Метод обработки ошибки
    private void OnAdError()
    {
        Debug.Log("Ошибка показа рекламы!");
    }

    // Ваш метод вознаграждения
    
}
