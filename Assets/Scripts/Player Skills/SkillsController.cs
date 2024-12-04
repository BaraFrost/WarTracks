using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsController : MonoBehaviour
{
    [SerializeField]
    private GameObject objectPrefab; // Префаб объекта, который нужно спавнить
    [SerializeField]
    private RectTransform canvasTransform; // Канвас
    [SerializeField]
    private float minY = -200f; // Минимальное значение по оси Y
    [SerializeField]
    private float maxY = 200f; // Максимальное значение по оси Y
    [SerializeField]
    private int spawnCount = 5; // Количество объектов для спавна

    // Объект, относительно которого будет происходить спаун
    [SerializeField]
    private RectTransform referenceObject;

    

    public void TriggerArtillery()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            // Генерация случайной позиции по оси Y в заданном диапазоне
            float randomY = Random.Range(minY, maxY);

            // Создаем объект и прикрепляем его к канвасу
            GameObject newObject = Instantiate(objectPrefab, canvasTransform);
            RectTransform newObjectRect = newObject.GetComponent<RectTransform>();

            // Устанавливаем локальную позицию относительно родительского объекта
            newObjectRect.anchoredPosition = new Vector2(referenceObject.anchoredPosition.x, randomY);
        }
    }
}
