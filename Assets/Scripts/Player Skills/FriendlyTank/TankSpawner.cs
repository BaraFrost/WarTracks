using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankSpawner : MonoBehaviour
{
    public GameObject friendlyTankPrefab; // Префаб танка
    [SerializeField]
    private int dRCount; // Количество доступных танков для спавна
    private int dRPrice = 1; // Стоимость спавна танка

    private Transform spawnPoint; // Точка для спавна

    [SerializeField]
    private bool dR;
    
    private float reloadTimer;
    public Image reloadDR;
    [SerializeField]
    private float reloadTime = 2;
    private void Start()
    {
        reloadTimer = reloadTime;
        // Загружаем количество доступных танков из PlayerPrefs
        dRCount = PlayerPrefs.GetInt("DR");

        // Ищем объект с тегом dRSpawn
        GameObject spawnObject = GameObject.FindGameObjectWithTag("dRSpawn");
        if (spawnObject != null)
        {
            spawnPoint = spawnObject.transform;
        }
    }

    private void Update()
    {
        if(dR)
        {
            // Уменьшаем таймер и обновляем fillAmount изображения
            reloadTimer -= Time.deltaTime;
            reloadDR.fillAmount = reloadTimer / reloadTime;

            // Если перезарядка завершена, сбрасываем состояние
            if (reloadTimer <= 0)
            {
                dR = false;
                reloadTimer = reloadTime;
                reloadDR.fillAmount = reloadTimer / reloadTime;
            }
        }
    }
    public void SpawnTank()
    {
        // Проверяем, достаточно ли танков для спавна и задана ли точка спавна
        if (dRCount > 0 && spawnPoint != null && !dR)
        {
            Instantiate(friendlyTankPrefab, spawnPoint.position, spawnPoint.rotation);
            dRCount -= dRPrice;
            PlayerPrefs.SetInt("DR", dRCount);
            PlayerPrefs.Save();
            dR = true;
        }
    }
}
