using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class EnemyController : MonoBehaviour
{
    /*[SerializeField]
    private float speed;*/
    [SerializeField]
    private EntitySpeed speed;
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
    //bool chill = false;
    bool angry = false;
    bool goBack = false;
    [SerializeField]
    private float minStopingDistance;
    [SerializeField]
    private Collider2D collider1;
    [SerializeField]// Первый коллайдер
    private Collider2D collider2;
    private float movementX;
    [SerializeField]
    private Collider2D targetCollider;
    [SerializeField]
    private float _gravity;
    public float distance;
    private void Awake()
    {
        playerPosition = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        rb.gravityScale = 0;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb.centerOfMass = _centerOfMass.localPosition;
        sprite = GetComponent<SpriteRenderer>();
        Rigidbody2D rb2 = GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {

            
        }
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            transform.position = transform.position;
        }
    }
    // Update is called once per frame
    void Update()
    {
        distance =(float) Vector2.Distance(transform.position, player.position);

        movementX = 0;
        if (Vector2.Distance(transform.position, player.position) < stoppingDistance)
        {
            angry = true;
            movementX = -1;
        }
        if (Vector2.Distance(transform.position, player.position) > stoppingDistance || Vector2.Distance(transform.position, player.position) < minStopingDistance)// в последствии сделай логику такой что когда игрок едет на врага то он уезжаеть там надо две переменных типо расстояние там первое будет как мин расстояние остановки и максимальное 
        {
            goBack = true;
            angry = false;
            movementX = 0;//здесь стояла 1
        }
        
       /* else if (angry == true)
        {
            Angry();

        }
        else if (goBack == true)
        {
            GoBack();
        }*/


    }
    void FixedUpdate()
    {
        if (movementX == 0 && collider1.IsTouching(targetCollider) && collider2.IsTouching(targetCollider))
        {
            // Выполняем действие, если оба коллайдера пересекаются с targetCollider
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
        // Получаем текущий угол наклона танка
        float currentRotation = transform.eulerAngles.z;

        // Приводим угол к диапазону [-180, 180] для удобства сравнения
        if (currentRotation > 180)
            currentRotation -= 360;

        // Если наклон танка превышает максимальный угол наклона, возвращаем его
        if (Mathf.Abs(currentRotation) > maxTiltAngle)
        {
            // Вычисляем направление и величину силы для стабилизации
                  float torqueDirection = -Mathf.Sign(currentRotation); // Противоположный момент силы
            rb.AddTorque(torqueDirection * stabilizationTorque);
        }
    }
}
