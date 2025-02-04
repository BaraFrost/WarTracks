using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerPrefs = RedefineYG.PlayerPrefs;

public class SpawnPlane : MonoBehaviour
{
    [Header("��������� �������")]
    [SerializeField] private string itemKey = "Plane"; // ���� ��� ���������� � PlayerPrefs
    [SerializeField] private GameObject planePrefab; // ������ �������
    [SerializeField] private Transform spawnPoint; // ����� ���������
    [SerializeField] private int planePrice = 1; // ���� �� �������
    [SerializeField] private float initialDelay = 1; // �������� ����� ������� ������

    private int sTCount; // ���������� �������� ��� ������
    private bool hasSpawned; // ����, ����� ����� ��������� ������ ���� ���

    void Start()
    {
        // ��������� ���������� ��������
        sTCount = PlayerPrefs.GetInt(itemKey, 0);

        // �������� ����� � ���������, ���� ���� ��� ��������
        if (sTCount > 0)
        {
            Invoke(nameof(SpawnItem), initialDelay);
        }
    }

    private void SpawnItem()
    {
        if (hasSpawned || sTCount <= 0) return;

        // ������� ������
        Instantiate(planePrefab, spawnPoint.position, spawnPoint.rotation);

        // ��������� ����������
        sTCount -= planePrice;
        if (sTCount < 0)
        {
            sTCount = 0;
        }

        // ��������� ����������� ���������� � PlayerPrefs
        PlayerPrefs.SetInt(itemKey, sTCount);
        PlayerPrefs.Save();

        // ��������, ��� ������ ��� ������
        hasSpawned = true;
    }
}
