using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEngine;

public class DokController : MonoBehaviour
{
    [SerializeField]
    private Transform gun; // Оружие, из которого выходит луч
    [SerializeField]
    private Transform dokObj; // Объект, который будет менять размер
    [SerializeField]
    private float maxDistance = 10f; // Максимальная длина луча
    public LayerMask enemy; // Слой врагов
    [SerializeField]
    private float distancew; // Текущее расстояние до цели
    [SerializeField]
    private Vector2 startDokObj; // Начальная позиция объекта

    [SerializeField] private float minDistancew = 1f; // Минимальное значение расстояния
    [SerializeField] private float maxDistancew = 10f; // Максимальное значение расстояния
    [SerializeField] private float minScale = 0.5f; // Минимальный размер объекта
    [SerializeField] private float maxScale = 2f; // Максимальный размер объекта
    [SerializeField]
    private bool nePopal = true;
    void Start()
    {
        startDokObj = dokObj.position; // Сохраняем начальную позицию объекта
    }

    void Update()
    {
        // Определяем направление луча
        Vector2 direction = transform.right;

        // Выпускаем луч
        RaycastHit2D hit = Physics2D.Raycast(gun.position, direction, maxDistance, enemy);
        if (hit.collider != null)
        {
            nePopal= false;
            Vector2 intersectionPoint = hit.point;
            dokObj.position = intersectionPoint; // Устанавливаем объект в точку пересечения
            distancew = hit.distance; // Получаем расстояние до цели
        }
        else
        {
            nePopal= true;
            // Если луч не попал, устанавливаем объект на максимальное расстояние
            dokObj.position = (Vector2)transform.position + direction * maxDistance;
        }

        // Обновляем размер объекта
        UpdateScale();

        // Рисуем луч для отладки
        Debug.DrawRay(transform.position, direction * maxDistance, Color.red);
    }

    // Метод для обновления размера объекта
    private void UpdateScale()
    {
        // Ограничиваем значение distancew в диапазоне [minDistancew, maxDistancew]
        float clampedDistance = Mathf.Clamp(distancew, minDistancew, maxDistancew);

        // Нормализуем расстояние в диапазоне [0, 1]
        float normalizedDistance = Mathf.InverseLerp(minDistancew, maxDistancew, clampedDistance);

        // Вычисляем новый размер с помощью линейной интерполяции
        float newScale = Mathf.Lerp(minScale, maxScale, normalizedDistance);

        // Устанавливаем размер объекта по осям X и Y (Z остается фиксированной)
        dokObj.localScale = new Vector3(dokObj.localScale.x, newScale, dokObj.localScale.z);
        if(nePopal==true)
        {
            dokObj.localScale = new Vector3(dokObj.localScale.x, maxScale, dokObj.localScale.z);
        }
    }
}