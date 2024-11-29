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
    public float minAngle;  // ����������� ���� � ��������
    public float maxAngle;  // ������������ ���� � ��������
    [SerializeField]
    private GameObject smoke;
    private EntityHealth entityHealth;

    [SerializeField]
    private AudioClip collisionSound;
    private Renderer objectRenderer;

    [SerializeField]
    private LineRenderer lineRenderer; // ����� ��� ����������� ����������
    [SerializeField]
    private int maxPoints = 50; // ������������ ���������� �����
    [SerializeField]
    private float pointSpacing = 0.1f; // �������� ����� �������

    private List<Vector3> trajectoryPoints = new List<Vector3>();
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        objectRenderer = GetComponent<Renderer>();

        if (lineRenderer == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
        }

        // ����������� LineRenderer
        lineRenderer.positionCount = 0;
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.yellow;
        lineRenderer.endColor = Color.red;

        // ����� ��������� �������� �������
        float randomAngle = UnityEngine.Random.Range(minAngle, maxAngle);
        Vector3 direction = Quaternion.AngleAxis(randomAngle, Vector3.forward) * transform.right;
        rb.velocity = direction * speed;

        // ��������� ������ ����� ����������
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
        if (objectRenderer.isVisible) // ���������, ����� �� ������
        {
            PlaySound(collisionSound); // ������������� ���� ������ ���� ������ �����
        }

        Destroy(gameObject); // ���������� ������
        Instantiate(smoke, transform.position, transform.rotation); // ������ ������ ����

        
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip == null) return;

        // ������ ��������� ������ ��� �����
        GameObject tempSoundObject = new GameObject("TempAudio");
        AudioSource tempAudioSource = tempSoundObject.AddComponent<AudioSource>();

        // ����������� ��������� �����
        tempAudioSource.clip = clip;
        tempAudioSource.volume = 0.1f; // ������������� ���������
        tempAudioSource.spatialBlend = 0; // ������������� 2D-����
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
        // ��������� ����������
        UpdateTrajectory();
    }
   
    void RotateTowardsMovementDirection(Vector2 movement)
    {
        // ��������� ���� � �������� �� ������� ��������
        float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;

        // ������������ ������ �� ��� Z (� 2D ������������)
        rb.rotation = angle;
    }

    private void UpdateTrajectory()
    {
        // ��������� ������� ������� � ������ �����, ���� ��� ���������� ������ �� ����������
        if (trajectoryPoints.Count == 0 || Vector3.Distance(trajectoryPoints[trajectoryPoints.Count - 1], transform.position) >= pointSpacing)
        {
            trajectoryPoints.Add(transform.position);

            // ������������ ���������� �����
            if (trajectoryPoints.Count > maxPoints)
            {
                trajectoryPoints.RemoveAt(0);
            }

            // ��������� LineRenderer
            lineRenderer.positionCount = trajectoryPoints.Count;
            lineRenderer.SetPositions(trajectoryPoints.ToArray());
        }
    }
}
