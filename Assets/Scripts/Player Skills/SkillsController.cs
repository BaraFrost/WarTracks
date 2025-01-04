using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsController : MonoBehaviour
{
    [SerializeField]
    private GameObject bomb; // ������ �����
    [SerializeField]
    private GameObject heel; // ������ �����
    
    [SerializeField]
    private List<GameObject> artellerySpawn = new List<GameObject>(); // ������ ��������
    [SerializeField]
    private List<GameObject> heelSpawn = new List<GameObject>(); // ������ ��������
    [SerializeField]
    private int artBomb;

    [SerializeField]
    private int heelCount;
    

    private int artPrice=1;
    
    private int heelPrice=1;

    private void Start()
    {
        artBomb = PlayerPrefs.GetInt("Bomb");
        heelCount = PlayerPrefs.GetInt("Heel");
    }
  
    public void PlayerArtellery()
    {
        // ��������� ������� ������� (��������, ������)
        if (artBomb > 0)
        {
            SpawnBombs();
            artBomb -= artPrice;
            PlayerPrefs.SetInt("Bomb", artBomb);
            PlayerPrefs.Save();
        }
    }
    public void PlayerHeel()
    {
        if (heelCount > 0)
        {
            SpawnHeel();
            heelCount -= heelPrice;
            PlayerPrefs.SetInt("Heel", heelCount);
            PlayerPrefs.Save();
        }
    }

    

    private void SpawnBombs()
    {
        // �������� �� ������� ������� � ������
        foreach (var obj in artellerySpawn)
        {
            if (obj != null) // ���������, ��� ������ ����������
            {
                // ������� ����� �� ������� �������
                Instantiate(bomb, obj.transform.position, Quaternion.identity);
            }
        }
    }

    private void SpawnHeel()
    {
        // �������� �� ������� ������� � ������
        foreach (var obj in heelSpawn)
        {
            if (obj != null) // ���������, ��� ������ ����������
            {
                // ������� ����� �� ������� �������
                Instantiate(heel, obj.transform.position, Quaternion.identity);
            }
        }
    }


}
