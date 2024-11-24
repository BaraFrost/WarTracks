using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    [SerializeField]
    private GameObject smoke;
    [SerializeField]
    private float damage;

    [SerializeField]
    private AudioClip wallSound;
    [SerializeField]
    private AudioClip healthSound;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullet")
        {
            Destroy(gameObject);
            Instantiate(smoke, transform.position, transform.rotation);
        }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            PlaySound(wallSound);
            Destroy(gameObject);
            Instantiate(smoke, transform.position, transform.rotation);
        }

        if (collision.gameObject.TryGetComponent<EntityHealth>(out var health))
        {

            health.value = health.value - damage;

            PlaySound(healthSound);
            Destroy(gameObject);
            Instantiate(smoke, transform.position, transform.rotation);
        }
    }
    private void PlaySound(AudioClip clip)
    {
        if (clip == null) return;

        // Создаём временный объект для звука
        GameObject tempSoundObject = new GameObject("TempAudio");
        AudioSource tempAudioSource = tempSoundObject.AddComponent<AudioSource>();

        // Настраиваем параметры звука
        tempAudioSource.clip = clip;
        tempAudioSource.volume = 0.35f; // Настройте громкость
        tempAudioSource.spatialBlend = 0; // 2D звук
        tempAudioSource.Play();

        // Уничтожаем объект после воспроизведения звука
        Destroy(tempSoundObject, clip.length);
    }
}
