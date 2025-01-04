using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankSpawner : MonoBehaviour
{
    public GameObject friendlyTankPrefab; // Префаб танка
    [SerializeField]
    private int dRCount; // Количество доступных танков для спавна
    private int dRPrice = 1; // Стоимость спавна танка

    private Transform spawnPoint; // Точка для спавна

    private void Start()
    {
        // Загружаем количество доступных танков из PlayerPrefs
        dRCount = PlayerPrefs.GetInt("DR");

        // Ищем объект с тегом dRSpawn
        GameObject spawnObject = GameObject.FindGameObjectWithTag("dRSpawn");
        if (spawnObject != null)
        {
            spawnPoint = spawnObject.transform;
        }
    }

    public void SpawnTank()
    {
        // Проверяем, достаточно ли танков для спавна и задана ли точка спавна
        if (dRCount > 0 && spawnPoint != null)
        {
            Instantiate(friendlyTankPrefab, spawnPoint.position, spawnPoint.rotation);
            dRCount -= dRPrice;
            PlayerPrefs.SetInt("DR", dRCount);
            PlayerPrefs.Save();
        }
    }
}
