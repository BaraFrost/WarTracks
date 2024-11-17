using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heeling : MonoBehaviour
{
    [SerializeField]
    private float heelCount; // Количество лечения
   
    [SerializeField]
    private Rigidbody2D rb; // Rigidbody объекта

    private bool isStopped = false; // Флаг остановки объекта

    private void Start()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>(); // Автоматически находим Rigidbody, если он не задан
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<EntityHealth>(out var health))
        {
            // Лечение при столкновении с объектом, который имеет компонент EntityHealth
            health.value += heelCount;
            Destroy(gameObject); // Удаляем объект
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            // Остановка объекта при столкновении со стеной
            StopMovement();
        }
    }

    private void StopMovement()
    {
        if (!isStopped)
        {
            isStopped = true;

            // Останавливаем физическое движение
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;

            // Отключаем влияние физики
            rb.bodyType = RigidbodyType2D.Kinematic;
        }
    }
}