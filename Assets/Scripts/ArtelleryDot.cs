using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtelleryDot : MonoBehaviour
{
    [Header("Настройки спавнера")]
    public ArtilleryProjectile artillery; // Ссылка на скрипт с характеристиками снаряда
    [SerializeField] private GameObject projectilePrefab; // Префаб снаряда
    [SerializeField] private Transform spawnPoint; // Точка спавна снаряда
    [SerializeField] private float spawnInterval = 1f; // Интервал между спавнами

    private bool isSpawning = false;

    void Update()
    {
        // Проверяем значение переменной `artillery`
        if (artillery.artillery == 1 && !isSpawning)
        {
            StartSpawning();
        }
        else if (artillery.artillery == 0 && isSpawning)
        {
            StopSpawning();
        }
    }

    private void StartSpawning()
    {
        isSpawning = true;
        InvokeRepeating(nameof(SpawnProjectile), 0f, spawnInterval);
    }

    private void StopSpawning()
    {
        isSpawning = false;
        CancelInvoke(nameof(SpawnProjectile));
    }

    private void SpawnProjectile()
    {
        if (projectilePrefab != null && spawnPoint != null)
        {
            GameObject projectile = Instantiate(projectilePrefab, spawnPoint.position, spawnPoint.rotation);

            // Передаем настройки в снаряд
            ArtilleryProjectile projectileScript = projectile.GetComponent<ArtilleryProjectile>();
            if (projectileScript != null)
            {
                projectileScript.Initialize(artillery.speed, artillery.minAngle, artillery.maxAngle, artillery.impactEffectPrefab);
            }
        }
    }
}
