using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtelleryDot : MonoBehaviour
{
    [Header("��������� ��������")]
    public ArtilleryProjectile artillery; // ������ �� ������ � ���������������� �������
    [SerializeField] private GameObject projectilePrefab; // ������ �������
    [SerializeField] private Transform spawnPoint; // ����� ������ �������
    [SerializeField] private float spawnInterval = 1f; // �������� ����� ��������

    private bool isSpawning = false;

    void Update()
    {
        // ��������� �������� ���������� `artillery`
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

            // �������� ��������� � ������
            ArtilleryProjectile projectileScript = projectile.GetComponent<ArtilleryProjectile>();
            if (projectileScript != null)
            {
                projectileScript.Initialize(artillery.speed, artillery.minAngle, artillery.maxAngle, artillery.impactEffectPrefab);
            }
        }
    }
}
