using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtilleryProjectile : MonoBehaviour
{
    [Header("Характеристики снаряда")]
    public int artillery = 0; // Переменная для управления (может быть полезна при настройке)
    public float speed; // Скорость снаряда
    public float minAngle; // Минимальный угол траектории
    public float maxAngle; // Максимальный угол траектории
    public GameObject impactEffectPrefab; // Префаб объекта при столкновении

    private Rigidbody2D rb;

    public void Initialize(float projectileSpeed, float minTrajectoryAngle, float maxTrajectoryAngle, GameObject impactEffect)
    {
        speed = projectileSpeed;
        minAngle = minTrajectoryAngle;
        maxAngle = maxTrajectoryAngle;
        impactEffectPrefab = impactEffect;

        SetInitialVelocity();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            Debug.LogError("Rigidbody2D не найден. Добавьте Rigidbody2D к объекту снаряда.");
        }

        // Если параметры не были установлены вручную, установить их по умолчанию
        if (rb != null && speed > 0 && minAngle < maxAngle)
        {
            SetInitialVelocity();
        }
        else
        {
            Debug.LogError("Параметры снаряда не настроены. Проверьте значения скорости и углов.");
        }
    }

    private void SetInitialVelocity()
    {
        // Устанавливаем случайный угол траектории
        float randomAngle = Random.Range(minAngle, maxAngle);
        Vector3 direction = Quaternion.Euler(0, 0, randomAngle) * Vector3.right;

        if (rb != null)
        {
            rb.velocity = direction * speed;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Проверяем теги
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Enemy"))
        {
            // Создаем объект на месте столкновения
            if (impactEffectPrefab != null)
            {
                Instantiate(impactEffectPrefab, transform.position, Quaternion.identity);
            }

            // Уничтожаем снаряд
            Destroy(gameObject);
        }
    }
}
