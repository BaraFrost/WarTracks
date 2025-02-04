using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayerPrefs = RedefineYG.PlayerPrefs;

public class SpawnSupportTank : MonoBehaviour
{
    [Header("��������� �������")]
    [SerializeField] private string itemKey = "ST"; // ���� ��� ���������� � PlayerPrefs
    [SerializeField] private GameObject friendlyTankPrefab; // ������ �������
    [SerializeField] private Transform spawnPoint; // ����� ���������
    [SerializeField] private int sTPrice = 1; // ���� �� �������
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
        Instantiate(friendlyTankPrefab, spawnPoint.position, spawnPoint.rotation);

        // ��������� ����������
        sTCount -= sTPrice;
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
