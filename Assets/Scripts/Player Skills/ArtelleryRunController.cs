using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtelleryRunController : MonoBehaviour
{
    [SerializeField]
    private EntitySpeed speed; // �������� �����
    [SerializeField]
    private Rigidbody2D rb; // Rigidbody �����
    [SerializeField]
    private Transform _centerOfMass; // ����� �����
    [SerializeField]
    private float maxTiltAngle; // ������������ ���� �������
    [SerializeField]
    private float stabilizationTorque; // ������������ �������
    [SerializeField]
    private float stoppingDistance = 5f; // ���������� ���������
    [SerializeField]
    private float avoidDistance = 2f; // ����������, �� ������� ���� �������� ���������
    [SerializeField]
    private float _gravity; // �������������� ���� "����������"

    private Transform player; // ������ �� ������
    private float distance; // ������� ���������� �� ������

    private void Start()
    {
       
        rb.centerOfMass = _centerOfMass.localPosition;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        distance = Vector2.Distance(transform.position, player.position);

        if (distance < avoidDistance)
        {
            MoveAwayFromPlayer();
        }
        else if (distance > stoppingDistance)
        {
            StopMovement();
        }
    }

    private void FixedUpdate()
    {
        LimitTankRotation();
    }

    private void MoveAwayFromPlayer()
    {
        Vector2 directionAwayFromPlayer = (transform.position - player.position).normalized;
        rb.velocity = directionAwayFromPlayer * speed.value;
    }

    private void StopMovement()
    {
        rb.velocity = Vector2.zero;
    }

    private void LimitTankRotation()
    {
        float currentRotation = transform.eulerAngles.z;

        if (currentRotation > 180)
            currentRotation -= 360;

        if (Mathf.Abs(currentRotation) > maxTiltAngle)
        {
            float torqueDirection = -Mathf.Sign(currentRotation);
            rb.AddTorque(torqueDirection * stabilizationTorque);
        }
    }
}
