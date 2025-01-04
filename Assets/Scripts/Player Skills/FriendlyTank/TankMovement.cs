using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class TankMovement : MonoBehaviour
{
    [SerializeField]
    private EntitySpeed speed; // �������� ��������
    [SerializeField]
    private Rigidbody2D rb; // Rigidbody2D ��� ��������
    [SerializeField]
    private Transform point; // �����, � ������� �������� ����
    [SerializeField]
    private float maxTiltAngle; // ������������ ���� ������� �����
    [SerializeField]
    private float stabilizationTorque; // �������� ������������ �������
    [SerializeField]
    private Transform _centerOfMass; // ����� ����� �����
    [SerializeField]
    private float stoppingDistance; // ����������, ��� ������� ���� ���������������
    [SerializeField]
    private float _gravity; // ����������
    [SerializeField]
    private Collider2D collider1; // ������ ���������
    [SerializeField]
    private Collider2D collider2; // ������ ���������
    [SerializeField]
    private float distance;

    private Transform targetEnemy; // ��������� ����
    private float movementX; // ����������� ��������

    private void Start()
    {
        rb.gravityScale = 0;
        rb.centerOfMass = _centerOfMass.localPosition;

        // ������������� �����, ���� ��� �� ������
        if (point == null)
        {
            GameObject tempPoint = new GameObject("Point");
            point = tempPoint.transform;
            point.position = transform.position;
        }
    }

    private void Update()
    {
        FindClosestEnemy();

        if (targetEnemy == null)
        {
            movementX = 0;
            return;
        }

        distance = Vector2.Distance(transform.position, targetEnemy.position);

        if (distance > stoppingDistance)
        {
            movementX = 1; // �������� � ����
        }
        else
        {
            movementX = 0; // ���������
        }
    }

    private void FixedUpdate()
    {
        if (targetEnemy == null)
            return;

        Vector2 direction = (targetEnemy.position - transform.position).normalized;

        // ��������� �������� �����
        Vector2 movement = direction * movementX * speed.value + Vector2.up * _gravity;
        rb.velocity = movement;
        LimitTankRotation();
    }

    private void FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float closestDistance = Mathf.Infinity;
        Transform closestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy.transform;
            }
        }

        // ���� ����� ���������� �����, ��������� ����
        if (closestEnemy != null)
        {
            targetEnemy = closestEnemy;
            point.position = closestEnemy.position; // ��������� ������� �����
        }
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
