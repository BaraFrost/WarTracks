using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArtelleryBullet : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D rb;
    [SerializeField] private float damage;

    [Header("Время жизни снаряда")]
    [SerializeField] private float lifeTimeParabolic = 3f; // Время жизни в параболическом режиме
    [SerializeField] private float lifeTimeStraight = 5f; // Время жизни в прямом режиме

    private float lifeTime; // Текущее время жизни снаряда

    [SerializeField] private float minAngle;
    [SerializeField] private float maxAngle;
    [SerializeField] private GameObject smoke;
    [SerializeField] private AudioClip wallSound;
    [SerializeField] private AudioClip healthSound;

    [SerializeField] private WeaponController weaponController;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Находим WeaponController
        weaponController = FindObjectOfType<WeaponController>();

        // Устанавливаем начальное время жизни в зависимости от режима стрельбы
        SetLifeTime();

        // Устанавливаем начальную траекторию
        SetInitialVelocity();
    }

    void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }

        // Проверяем текущий режим стрельбы
        if (weaponController != null && weaponController.CurrentFireMode == WeaponController.FireMode.Straight)
        {
            rb.velocity = transform.right * speed;
        }

        RotateTowardsMovementDirection(rb.velocity);
    }

    private void SetLifeTime()
    {
        // Устанавливаем время жизни в зависимости от текущего режима стрельбы
        if (weaponController != null)
        {
            lifeTime = weaponController.CurrentFireMode == WeaponController.FireMode.Parabolic
                ? lifeTimeParabolic
                : lifeTimeStraight;
        }
        else
        {
            lifeTime = lifeTimeParabolic; // Значение по умолчанию
        }
    }

    private void SetInitialVelocity()
    {
        if (weaponController != null && weaponController.CurrentFireMode == WeaponController.FireMode.Parabolic)
        {
            // Устанавливаем параболическую траекторию
            float randomAngle = UnityEngine.Random.Range(minAngle, maxAngle);
            Vector3 direction = Quaternion.AngleAxis(randomAngle, Vector3.forward) * transform.right;
            rb.velocity = direction * speed;
        }
        else
        {
            // Прямая траектория
            rb.velocity = transform.right * speed;
        }
    }

    private void RotateTowardsMovementDirection(Vector2 movement)
    {
        if (movement.sqrMagnitude > 0.01f)
        {
            float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
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
            health.value -= damage;
            PlaySound(healthSound);
            Destroy(gameObject);
            Instantiate(smoke, transform.position, transform.rotation);
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip == null) return;

        GameObject tempSoundObject = new GameObject("TempAudio");
        AudioSource tempAudioSource = tempSoundObject.AddComponent<AudioSource>();
        tempAudioSource.clip = clip;
        tempAudioSource.volume = 0.35f;
        tempAudioSource.spatialBlend = 0;
        tempAudioSource.Play();
        Destroy(tempSoundObject, clip.length);
    }
}
