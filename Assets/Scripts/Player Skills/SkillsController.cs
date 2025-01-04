using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsController : MonoBehaviour
{
    [SerializeField]
    private GameObject bomb; // Префаб бомбы
    [SerializeField]
    private GameObject heel; // Префаб бомбы
    
    [SerializeField]
    private List<GameObject> artellerySpawn = new List<GameObject>(); // Список объектов
    [SerializeField]
    private List<GameObject> heelSpawn = new List<GameObject>(); // Список объектов
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
        // Проверяем нажатие клавиши (например, пробел)
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
        // Проходим по каждому объекту в списке
        foreach (var obj in artellerySpawn)
        {
            if (obj != null) // Проверяем, что объект существует
            {
                // Спавним бомбу на позиции объекта
                Instantiate(bomb, obj.transform.position, Quaternion.identity);
            }
        }
    }

    private void SpawnHeel()
    {
        // Проходим по каждому объекту в списке
        foreach (var obj in heelSpawn)
        {
            if (obj != null) // Проверяем, что объект существует
            {
                // Спавним бомбу на позиции объекта
                Instantiate(heel, obj.transform.position, Quaternion.identity);
            }
        }
    }


}
