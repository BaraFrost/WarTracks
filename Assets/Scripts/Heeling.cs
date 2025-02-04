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

    [SerializeField]
    private AudioClip shootSound;
    [SerializeField]
    private AudioSource audioSource;
    private void Start()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>(); // Автоматически находим Rigidbody, если он не задан
        }
        audioSource = GetComponent<AudioSource>();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<EntityHealth>(out var health))
        {
            health.value += heelCount;

            // Создаём временный объект для звука
            GameObject tempSoundObject = new GameObject("TempAudio");
            AudioSource tempAudioSource = tempSoundObject.AddComponent<AudioSource>();

            // Настраиваем звук
            tempAudioSource.clip = shootSound;
            tempAudioSource.volume = 0.05f; // Установите нужную громкость (например, 50%)
            tempAudioSource.pitch = 1.0f;  // Установите нужный тон
            tempAudioSource.spatialBlend = 0; // Для 2D-звука (если требуется)
            tempAudioSource.Play();

            // Уничтожаем временный объект после завершения воспроизведения звука
            Destroy(tempSoundObject, shootSound.length);

            // Уничтожаем основной объект
            Destroy(gameObject);
        }
        else if ((collision.gameObject.CompareTag("Wall"))|| (collision.gameObject.CompareTag("Player")))
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