using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heeling : MonoBehaviour
{
    [SerializeField]
    private float heelCount; // ���������� �������
   
    [SerializeField]
    private Rigidbody2D rb; // Rigidbody �������

    private bool isStopped = false; // ���� ��������� �������

    private void Start()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>(); // ������������� ������� Rigidbody, ���� �� �� �����
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<EntityHealth>(out var health))
        {
            // ������� ��� ������������ � ��������, ������� ����� ��������� EntityHealth
            health.value += heelCount;
            Destroy(gameObject); // ������� ������
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            // ��������� ������� ��� ������������ �� ������
            StopMovement();
        }
    }

    private void StopMovement()
    {
        if (!isStopped)
        {
            isStopped = true;

            // ������������� ���������� ��������
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;

            // ��������� ������� ������
            rb.bodyType = RigidbodyType2D.Kinematic;
        }
    }
}