using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankSpawner : MonoBehaviour
{
    public GameObject friendlyTankPrefab; // ������ �����
    [SerializeField]
    private int dRCount; // ���������� ��������� ������ ��� ������
    private int dRPrice = 1; // ��������� ������ �����

    private Transform spawnPoint; // ����� ��� ������

    [SerializeField]
    private bool dR;
    
    private float reloadTimer;
    public Image reloadDR;
    [SerializeField]
    private float reloadTime = 2;
    private void Start()
    {
        reloadTimer = reloadTime;
        // ��������� ���������� ��������� ������ �� PlayerPrefs
        dRCount = PlayerPrefs.GetInt("DR");

        // ���� ������ � ����� dRSpawn
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
            // ��������� ������ � ��������� fillAmount �����������
            reloadTimer -= Time.deltaTime;
            reloadDR.fillAmount = reloadTimer / reloadTime;

            // ���� ����������� ���������, ���������� ���������
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
        // ���������, ���������� �� ������ ��� ������ � ������ �� ����� ������
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
