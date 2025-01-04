using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArtellery : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D rb;
    [SerializeField] private int damage;
    public float lifeTime;
    public float minAngle;  // ����������� ���� � ��������
    public float maxAngle;  // ������������ ���� � ��������
    [SerializeField] private GameObject smoke;
    [SerializeField] private AudioClip collisionSound;

    private TrailRenderer trailRenderer;
    private Renderer objectRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        trailRenderer = GetComponent<TrailRenderer>();
        objectRenderer = GetComponent<Renderer>();

        // ������� Trail Renderer ����� �������
        if (trailRenderer != null)
        {
            trailRenderer.Clear();
        }

        // ����� ��������� �������� �������
        float randomAngle = UnityEngine.Random.Range(minAngle, maxAngle);
        Vector3 direction = Quaternion.AngleAxis(randomAngle, Vector3.forward) * transform.right;
        rb.velocity = direction * speed;

        // ������������� ����������� ����������
        RotateTowardsMovementDirection(rb.velocity);
    }

    void Update()
    {
        // ��������� ����� ����� �������
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }

        // ������������ ������ � ����������� ��������
        RotateTowardsMovementDirection(rb.velocity);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            HandleCollision();
        }
        else if (collision.gameObject.TryGetComponent<EntityHealth>(out var health))
        {
            health.value -= damage;
            HandleCollision();
        }
    }

    private void HandleCollision()
    {
        if (objectRenderer.isVisible) // ���������, ����� �� ������
        {
            PlaySound(collisionSound);
        }

        Destroy(gameObject);
        Instantiate(smoke, transform.position, transform.rotation);
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip == null) return;

        // ������ ��������� ������ ��� �����
        GameObject tempSoundObject = new GameObject("TempAudio");
        AudioSource tempAudioSource = tempSoundObject.AddComponent<AudioSource>();

        // ����������� ��������� �����
        tempAudioSource.clip = clip;
        tempAudioSource.volume = 0.1f;
        tempAudioSource.spatialBlend = 0;
        tempAudioSource.Play();

        // ���������� ��������� ������ ����� ���������� ���������������
        Destroy(tempSoundObject, clip.length);
    }

    private void RotateTowardsMovementDirection(Vector2 movement)
    {
        // ���������, ���� �� ��������
        if (movement.sqrMagnitude > 0.01f)
        {
            // ��������� ���� � �������� �� ������� ��������
            float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;

            // ������������ ������ �� ��� Z (� 2D ������������)
            rb.rotation = angle;
        }
    }
}
