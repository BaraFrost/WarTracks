using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtilleryProjectile : MonoBehaviour
{
    [Header("�������������� �������")]
    public int artillery = 0; // ���������� ��� ���������� (����� ���� ������� ��� ���������)
    public float speed; // �������� �������
    public float minAngle; // ����������� ���� ����������
    public float maxAngle; // ������������ ���� ����������
    public GameObject impactEffectPrefab; // ������ ������� ��� ������������

    private Rigidbody2D rb;

    public void Initialize(float projectileSpeed, float minTrajectoryAngle, float maxTrajectoryAngle, GameObject impactEffect)
    {
        speed = projectileSpeed;
        minAngle = minTrajectoryAngle;
        maxAngle = maxTrajectoryAngle;
        impactEffectPrefab = impactEffect;

        SetInitialVelocity();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            Debug.LogError("Rigidbody2D �� ������. �������� Rigidbody2D � ������� �������.");
        }

        // ���� ��������� �� ���� ����������� �������, ���������� �� �� ���������
        if (rb != null && speed > 0 && minAngle < maxAngle)
        {
            SetInitialVelocity();
        }
        else
        {
            Debug.LogError("��������� ������� �� ���������. ��������� �������� �������� � �����.");
        }
    }

    private void SetInitialVelocity()
    {
        // ������������� ��������� ���� ����������
        float randomAngle = Random.Range(minAngle, maxAngle);
        Vector3 direction = Quaternion.Euler(0, 0, randomAngle) * Vector3.right;

        if (rb != null)
        {
            rb.velocity = direction * speed;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // ��������� ����
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Enemy"))
        {
            // ������� ������ �� ����� ������������
            if (impactEffectPrefab != null)
            {
                Instantiate(impactEffectPrefab, transform.position, Quaternion.identity);
            }

            // ���������� ������
            Destroy(gameObject);
        }
    }
}
