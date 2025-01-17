using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportTankController : MonoBehaviour
{
    [Header("Настройки персонажа")]
    [SerializeField] private EntitySpeed speed; // Скорость движения
    [SerializeField] private Rigidbody2D rb; // Rigidbody2D для движения
    [SerializeField] private Transform point; // Точка, к которой движется танк
    [SerializeField] private float maxTiltAngle; // Максимальный угол наклона танка
    [SerializeField] private float stabilizationTorque; // Скорость стабилизации наклона
    [SerializeField] private Transform _centerOfMass; // Центр массы танка
    [SerializeField] private float stoppingDistance; // Расстояние, при котором танк останавливается
    [SerializeField] private float _gravity; // Гравитация
    [SerializeField] private Collider2D collider1; // Первый коллайдер
    [SerializeField] private Collider2D collider2; // Второй коллайдер
    [SerializeField] private float distance; // Дистанция до врага (сериализована для просмотра в инспекторе)

    private Transform targetEnemy; // Ближайший враг
    private float movementX; // Направление движения

    private void Start()
    {
        rb.gravityScale = 0;
        rb.centerOfMass = _centerOfMass.localPosition;

        // Присваиваем скорость от объекта с тэгом Player
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            speed = player.GetComponent<EntitySpeed>();
        }
        else
        {
            Debug.LogError("Объект с тэгом Player не найден!");
        }

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
            movementX = 0; // Нет врага — останавливаемся
            return;
        }

        // Обновление расстояния до врага
        distance = Vector2.Distance(transform.position, targetEnemy.position);

        if (distance > stoppingDistance)
        {
            movementX = 1; // Движение к врагу
        }
        else
        {
            movementX = 0; // Остановка
        }
    }

    private void FixedUpdate()
    {
        if (targetEnemy == null || speed == null) return;

        Vector2 direction = (targetEnemy.position - transform.position).normalized;

        // Движение танка с учетом гравитации
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
            float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < closestDistance)
            {
                closestDistance = distanceToEnemy;
                closestEnemy = enemy.transform;
            }
        }

        // Если нашли ближайшего врага, обновляем цель
        if (closestEnemy != null)
        {
            targetEnemy = closestEnemy;
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
