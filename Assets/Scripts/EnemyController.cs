using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private EntitySpeed speed;
    private int speedValue;
    private float positionOfPatrol;
    [SerializeField]
    private Transform point;
    bool moveingRight;
    private SpriteRenderer playerPosition;
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private float maxTiltAngle; // Максимальный угол наклона танка в градусах
    [SerializeField]
    private float stabilizationTorque; // Скорость стабилизации наклона танка
    [SerializeField]
    private Transform _centerOfMass;
    [SerializeField]
    private Transform enemyPosition;
    Transform player;
    [SerializeField]
    private float stoppingDistance;
    private SpriteRenderer sprite;
    bool angry = false;
    bool goBack = false;
    [SerializeField]
    private float minStopingDistance;
    [SerializeField]
    private Collider2D collider1;
    [SerializeField] // Первый коллайдер
    private Collider2D collider2;
    private float movementX;
    private Collider2D targetCollider; // Убрали атрибут SerializeField, так как теперь он будет находиться автоматически
    [SerializeField]
    private float _gravity;
    public float distance;

    private void Awake()
    {
        playerPosition = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        speedValue = speed.value;
        rb.gravityScale = 0;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb.centerOfMass = _centerOfMass.localPosition;
        sprite = GetComponent<SpriteRenderer>();

        // Поиск объекта с тегом "Wall" и получение его Collider2D
        GameObject wallObject = GameObject.FindGameObjectWithTag("Wall");

        targetCollider = wallObject.GetComponent<Collider2D>();


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            speed.value = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //transform.position = transform.position;
            speed.value = speedValue;
        }
    }

    void Update()
    {
        distance = Vector2.Distance(transform.position, player.position);
        movementX = 0;

        if (distance < stoppingDistance)
        {
            angry = true;
            movementX = -1;
        }
        else if (distance > stoppingDistance || distance < minStopingDistance)
        {
            goBack = true;
            angry = false;
            movementX = 0; // здесь стояла 1
        }
    }

    void FixedUpdate()
    {
        if (movementX == 0 && collider1.IsTouching(targetCollider) && collider2.IsTouching(targetCollider))
        {
            // Если оба коллайдера пересекаются с targetCollider
            rb.velocity = Vector2.zero;
            rb.constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
            return;
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.None;
        }

        Vector2 movement = transform.right * movementX * speed.value + Vector3.up * _gravity;
        rb.velocity = movement;
        LimitTankRotation();
    }

    void Angry()
    {
        gameObject.TryGetComponent<EntitySpeed>(out var speed);
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed.value * Time.deltaTime);
    }

    void GoBack()
    {
        gameObject.TryGetComponent<EntitySpeed>(out var speed);
        transform.position = Vector2.MoveTowards(transform.position, point.position, speed.value * Time.deltaTime);
    }

    private void LimitTankRotation()
    {
        float currentRotation = transform.eulerAngles.z;

        // Приводим угол к диапазону [-180, 180]
        if (currentRotation > 180)
            currentRotation -= 360;

        // Если наклон превышает максимальный угол, стабилизируем
        if (Mathf.Abs(currentRotation) > maxTiltAngle)
        {
            float torqueDirection = -Mathf.Sign(currentRotation); // Противоположный момент силы
            rb.AddTorque(torqueDirection * stabilizationTorque);
        }
    }
}
