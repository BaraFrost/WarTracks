using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArtelleryBullet : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    public bool moveRight;
    [SerializeField]
    private float damage;
    public float lifeTime;
    public float minAngle; // минимальный угол в градусах
    public float maxAngle; // максимальный угол в градусах
    [SerializeField]
    private GameObject smoke;
    private EntityHealth entityHealth;

    [SerializeField]
    private AudioClip wallSound;
    [SerializeField]
    private AudioClip healthSound;

    [SerializeField]
    private LineRenderer lineRenderer; // Линия для отображения траектории
    [SerializeField]
    private int maxPoints = 50; // Максимальное количество точек
    [SerializeField]
    private float pointSpacing = 0.1f; // Интервал между точками

    private List<Vector3> trajectoryPoints = new List<Vector3>();

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (lineRenderer == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
        }

        // Настраиваем LineRenderer
        lineRenderer.positionCount = 0;
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.yellow;
        lineRenderer.endColor = Color.red;

        // Задаём начальную скорость снаряда
        float randomAngle = UnityEngine.Random.Range(minAngle, maxAngle);
        Vector3 direction = Quaternion.AngleAxis(randomAngle, Vector3.forward) * transform.right;
        rb.velocity = direction * speed;

        // Добавляем первую точку траектории
        trajectoryPoints.Add(transform.position);
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, transform.position);
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

        // Обновляем траекторию
        UpdateTrajectory();

        // Поворачиваем снаряд в направлении движения
        RotateTowardsMovementDirection(rb.velocity);
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

    void RotateTowardsMovementDirection(Vector2 movement)
    {
        if (movement.sqrMagnitude > 0.01f) // Проверка, чтобы угол рассчитывался только при наличии движения
        {
            float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
            rb.rotation = angle; // Устанавливаем поворот Rigidbody2D
        }
    }

    private void UpdateTrajectory()
    {
        // Добавляем текущую позицию в список точек, если она достаточно далека от предыдущей
        if (trajectoryPoints.Count == 0 || Vector3.Distance(trajectoryPoints[trajectoryPoints.Count - 1], transform.position) >= pointSpacing)
        {
            trajectoryPoints.Add(transform.position);

            // Ограничиваем количество точек
            if (trajectoryPoints.Count > maxPoints)
            {
                trajectoryPoints.RemoveAt(0);
            }

            // Обновляем LineRenderer
            lineRenderer.positionCount = trajectoryPoints.Count;
            lineRenderer.SetPositions(trajectoryPoints.ToArray());
        }
    }
}
