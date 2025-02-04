using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayerPrefs = RedefineYG.PlayerPrefs;

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


    private int artPrice = 1;

    private int heelPrice = 1;

    [SerializeField]
    private bool artel;
    
    private float reloadTimer;
    public Image reloadBomb;
    
    private float reloadTime=4;


    [SerializeField]
    private bool heeling;
    
    private float heelingReloadTimer;
    public Image reloadHeel;
   
    private float heelingReloadTime=2;
    private void Start()
    {
        artBomb = PlayerPrefs.GetInt("Bomb");
        heelCount = PlayerPrefs.GetInt("Heel");
        reloadTimer = reloadTime;
        heelingReloadTimer = heelingReloadTime;
    }

    private void Update()
    {
        if (artel)
        {
            // Уменьшаем таймер и обновляем fillAmount изображения
            reloadTimer -= Time.deltaTime;
            reloadBomb.fillAmount = reloadTimer / reloadTime;

            // Если перезарядка завершена, сбрасываем состояние
            if (reloadTimer <= 0)
            {
                artel = false;
                reloadTimer = reloadTime;
                reloadBomb.fillAmount = reloadTimer / reloadTime;
            }
        }

        if (heeling)
        {
            // Уменьшаем таймер и обновляем fillAmount изображения
            heelingReloadTimer -= Time.deltaTime;
            reloadHeel.fillAmount = heelingReloadTimer / heelingReloadTime;

            // Если перезарядка завершена, сбрасываем состояние
            if (heelingReloadTimer <= 0)
            {
                heeling= false;
                heelingReloadTimer = heelingReloadTime;
                reloadHeel.fillAmount = heelingReloadTimer / heelingReloadTime;
            }
        }
    }

    public void PlayerArtellery()
    {
        // Проверяем нажатие клавиши (например, пробел)
        if (artBomb > 0 && !artel)
        {
            SpawnBombs();
            artBomb -= artPrice;
            PlayerPrefs.SetInt("Bomb", artBomb);
            PlayerPrefs.Save();
            artel = true;
        }
    }
    public void PlayerHeel()
    {
        if (heelCount > 0 && !heeling)
        {
            SpawnHeel();
            heelCount -= heelPrice;
            PlayerPrefs.SetInt("Heel", heelCount);
            PlayerPrefs.Save();
            heeling = true;
        }
    }



    private void SpawnBombs()
    {
        foreach (var obj in artellerySpawn)
        {
            if (obj != null) // Проверяем, что объект существует
            {
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

                Instantiate(heel, obj.transform.position, Quaternion.identity);
            }
        }
    }


}
