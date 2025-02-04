using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayerPrefs = RedefineYG.PlayerPrefs;

public class SpawnSupportTank : MonoBehaviour
{
    [Header("Настройки объекта")]
    [SerializeField] private string itemKey = "ST"; // Ключ для сохранения в PlayerPrefs
    [SerializeField] private GameObject friendlyTankPrefab; // Префаб объекта
    [SerializeField] private Transform spawnPoint; // Точка появления
    [SerializeField] private int sTPrice = 1; // Цена за единицу
    [SerializeField] private float initialDelay = 1; // Задержка перед началом спавна

    private int sTCount; // Количество объектов для спавна
    private bool hasSpawned; // Флаг, чтобы спавн произошел только один раз

    void Start()
    {
        // Загружаем количество объектов
        sTCount = PlayerPrefs.GetInt(itemKey, 0);

        // Начинаем спавн с задержкой, если есть что спавнить
        if (sTCount > 0)
        {
            Invoke(nameof(SpawnItem), initialDelay);
        }
    }

    private void SpawnItem()
    {
        if (hasSpawned || sTCount <= 0) return;

        // Создаем объект
        Instantiate(friendlyTankPrefab, spawnPoint.position, spawnPoint.rotation);

        // Уменьшаем количество
        sTCount -= sTPrice;
        if (sTCount < 0)
        {
            sTCount = 0;
        }

        // Сохраняем обновленное количество в PlayerPrefs
        PlayerPrefs.SetInt(itemKey, sTCount);
        PlayerPrefs.Save();

        // Отмечаем, что объект был создан
        hasSpawned = true;
    }
}
