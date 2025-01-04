using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtelleryBullet : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private Rigidbody2D rb;
    [SerializeField]
    private int damage;
    public float lifeTime;
    public float minAngle;  // ����������� ���� � ��������
    public float maxAngle;  // ������������ ���� � ��������
    [SerializeField]
    private GameObject smoke;

    [SerializeField]
    private AudioClip collisionSound;

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
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            HandleCollision();
        }
        else if (collision.gameObject.tag == "PlayerBullet")
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
        Instantiate(smoke, transform.position, transform.rotation);
        Destroy(gameObject);
        
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

    public void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }

        RotateTowardsMovementDirection(rb.velocity);
    }

    void RotateTowardsMovementDirection(Vector2 movement)
    {
        // ��������� ���� � �������� �� ������� ��������
        float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;

        // ������������ ������ �� ��� Z (� 2D ������������)
        rb.rotation = angle;
    }
}
