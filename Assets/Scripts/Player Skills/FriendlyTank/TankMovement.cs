using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class TankMovement : MonoBehaviour
{
    [SerializeField]
    private EntitySpeed speed; // Скорость движения
    [SerializeField]
    private Rigidbody2D rb; // Rigidbody2D для движения
    [SerializeField]
    private Transform point; // Точка, к которой движется танк
    [SerializeField]
    private float maxTiltAngle; // Максимальный угол наклона танка
    [SerializeField]
    private float stabilizationTorque; // Скорость стабилизации наклона
    [SerializeField]
    private Transform _centerOfMass; // Центр массы танка
    [SerializeField]
    private float stoppingDistance; // Расстояние, при котором танк останавливается
    [SerializeField]
    private float _gravity; // Гравитация
    [SerializeField]
    private Collider2D collider1; // Первый коллайдер
    [SerializeField]
    private Collider2D collider2; // Второй коллайдер
    [SerializeField]
    private float distance;

    private Transform targetEnemy; // Ближайший враг
    private float movementX; // Направление движения

    private void Start()
    {
        rb.gravityScale = 0;
        rb.centerOfMass = _centerOfMass.localPosition;

        // Инициализация точки, если она не задана
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
            movementX = 1; // Движение к цели
        }
        else
        {
            movementX = 0; // Остановка
        }
    }

    private void FixedUpdate()
    {
        if (targetEnemy == null)
            return;

        Vector2 direction = (targetEnemy.position - transform.position).normalized;

        // Обновляем скорость танка
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

        // Если нашли ближайшего врага, обновляем цель
        if (closestEnemy != null)
        {
            targetEnemy = closestEnemy;
            point.position = closestEnemy.position; // Обновляем позицию точки
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
