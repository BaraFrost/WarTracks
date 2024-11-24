using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastBullet : MonoBehaviour
{
    [SerializeField]
    private float speed;
    Rigidbody2D rb;
    private SpriteRenderer sprite;
    public bool moveRight;
    [SerializeField]
    private  int damage;
    public float lifeTime;
    public float minAngle;  // минимальный угол в градусах
    public float maxAngle;
    [SerializeField]
    private GameObject smoke;

    [SerializeField]
    private AudioClip wallSound;
    [SerializeField]
    private AudioClip healthSound;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        float randomAngle = UnityEngine.Random.Range(minAngle, maxAngle);
        //rb.velocity = (moveRight ? transform.right : Quaternion.AngleAxis(180, Vector3.forward) * transform.right) * speed;
        Vector3 direction = Quaternion.AngleAxis(randomAngle, Vector3.forward) * transform.right;
        rb.velocity = direction * speed;

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
    public void Update()
    {

        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Destroy(gameObject);
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
