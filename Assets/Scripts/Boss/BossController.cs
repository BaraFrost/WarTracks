using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
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
   
    [SerializeField]
    private float minStopingDistance;
    [SerializeField]
    private Collider2D collider1; // Первый коллайдер
    [SerializeField]
    private Collider2D collider2;
    private float movementX;
    [SerializeField]
    private Collider2D targetCollider;
    [SerializeField]
    private float _gravity;
    public float distance;

    [SerializeField]
    private Rigidbody2D rb;

    private void Awake()
    {
        rb.centerOfMass = _centerOfMass.localPosition;
        sprite = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        rb.gravityScale = 0;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // Логика при столкновении с игроком
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            transform.position = transform.position;
        }
    }

    void Update()
    {
        distance = Vector2.Distance(transform.position, player.position);
        movementX = 0;

        if (distance < stoppingDistance)
        {
            movementX = -1;
        }
        if (distance > stoppingDistance || distance < minStopingDistance)
        {
            
            movementX = 0;
        }
    }

    void FixedUpdate()
    {
        if (movementX == 0 && collider1.IsTouching(targetCollider) && collider2.IsTouching(targetCollider))
        {
            rb.velocity = Vector2.zero;
            rb.constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
            return;
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.None;
        }

        Vector2 movement = transform.right * movementX + Vector3.up * _gravity;
        rb.velocity = movement;
        LimitTankRotation();
    }

    private void LimitTankRotation()
    {
        float currentRotation = transform.eulerAngles.z;

        if (currentRotation > 180)
            currentRotation -= 360;

        if (Mathf.Abs(currentRotation) > maxTiltAngle)
        {
            float torqueDirection = -Mathf.Sign(currentRotation); // Противоположный момент силы
            rb.AddTorque(torqueDirection * stabilizationTorque);
        }

    }
    }
