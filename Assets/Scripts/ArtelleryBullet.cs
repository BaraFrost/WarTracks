using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtelleryBullet : MonoBehaviour
{
    [SerializeField]
    private float speed;
    Rigidbody2D rb;
    private SpriteRenderer sprite;
    public bool moveRight;
    [SerializeField]
    private int damage;
    public float lifeTime;
    public float minAngle;  // минимальный угол в градусах
    public float maxAngle;  // максимальный угол в градусах
    [SerializeField]
    private GameObject smoke;
    private EntityHealth entityHealth;

    [SerializeField]
    private AudioClip collisionSound;
    private Renderer objectRenderer;

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
        
        objectRenderer = GetComponent<Renderer>();

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
            HandleCollision();
        }
        else if (collision.gameObject.TryGetComponent<EntityHealth>(out var health))
        {
            health.value -= damage;
            HandleCollision();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullet")
        {
            HandleCollision();
        }
    }

    private void HandleCollision()
    {
        if (objectRenderer.isVisible) // Проверяем, видим ли объект
        {
            PlaySound(collisionSound); // Воспроизводим звук только если объект видим
        }

        Destroy(gameObject); // Уничтожаем объект
        Instantiate(smoke, transform.position, transform.rotation); // Создаём эффект дыма

        
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip == null) return;

        // Создаём временный объект для звука
        GameObject tempSoundObject = new GameObject("TempAudio");
        AudioSource tempAudioSource = tempSoundObject.AddComponent<AudioSource>();

        // Настраиваем параметры звука
        tempAudioSource.clip = clip;
        tempAudioSource.volume = 0.1f; // Устанавливаем громкость
        tempAudioSource.spatialBlend = 0; // Устанавливаем 2D-звук
        tempAudioSource.Play();

        // Уничтожаем временный объект после завершения воспроизведения
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
        // Обновляем траекторию
        UpdateTrajectory();
    }
   
    void RotateTowardsMovementDirection(Vector2 movement)
    {
        // Вычисляем угол в радианах от вектора движения
        float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;

        // Поворачиваем объект по оси Z (в 2D пространстве)
        rb.rotation = angle;
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
