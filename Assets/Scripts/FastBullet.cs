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
    public float minAngle;  // ����������� ���� � ��������
    public float maxAngle;
    [SerializeField]
    private GameObject smoke;

    [SerializeField]
    private AudioClip wallSound;
    [SerializeField]
    private AudioClip healthSound;

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

        if(lineRenderer == null)
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

        float randomAngle = UnityEngine.Random.Range(minAngle, maxAngle);
        //rb.velocity = (moveRight ? transform.right : Quaternion.AngleAxis(180, Vector3.forward) * transform.right) * speed;
        Vector3 direction = Quaternion.AngleAxis(randomAngle, Vector3.forward) * transform.right;
        rb.velocity = direction * speed;

        trajectoryPoints.Add(transform.position);
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, transform.position);

    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")// || collision.gameObject.tag == "PlayerBullet"
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

        // ��������� ����������
        UpdateTrajectory();
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip == null) return;

        // ������ ��������� ������ ��� �����
        GameObject tempSoundObject = new GameObject("TempAudio");
        AudioSource tempAudioSource = tempSoundObject.AddComponent<AudioSource>();

        // ����������� ��������� �����
        tempAudioSource.clip = clip;
        tempAudioSource.volume = 0.35f; // ��������� ���������
        tempAudioSource.spatialBlend = 0; // 2D ����
        tempAudioSource.Play();

        // ���������� ������ ����� ��������������� �����
        Destroy(tempSoundObject, clip.length);
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
