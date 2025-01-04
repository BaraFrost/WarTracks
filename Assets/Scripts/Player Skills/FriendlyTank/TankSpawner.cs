using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankSpawner : MonoBehaviour
{
    public GameObject friendlyTankPrefab; // ������ �����
    [SerializeField]
    private int dRCount; // ���������� ��������� ������ ��� ������
    private int dRPrice = 1; // ��������� ������ �����

    private Transform spawnPoint; // ����� ��� ������

    private void Start()
    {
        // ��������� ���������� ��������� ������ �� PlayerPrefs
        dRCount = PlayerPrefs.GetInt("DR");

        // ���� ������ � ����� dRSpawn
        GameObject spawnObject = GameObject.FindGameObjectWithTag("dRSpawn");
        if (spawnObject != null)
        {
            spawnPoint = spawnObject.transform;
        }
    }

    public void SpawnTank()
    {
        // ���������, ���������� �� ������ ��� ������ � ������ �� ����� ������
        if (dRCount > 0 && spawnPoint != null)
        {
            Instantiate(friendlyTankPrefab, spawnPoint.position, spawnPoint.rotation);
            dRCount -= dRPrice;
            PlayerPrefs.SetInt("DR", dRCount);
            PlayerPrefs.Save();
        }
    }
}
