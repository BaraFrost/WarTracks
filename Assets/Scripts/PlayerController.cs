using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    private float movementX;
    private SpriteRenderer sprite;

    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private Transform _centerOfMass;
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private bool flip = true;
    [SerializeField]
    private Sprite tank;
    [SerializeField]
    private Sprite fort1;
    [SerializeField]
    private float sceneWidth = 10;
    [SerializeField]
    private EntitySpeed speed;
    [SerializeField]
    private float _gravity;
    [SerializeField]
    private float maxTiltAngle=10f; // ������������ ���� ������� ����� � ��������
    [SerializeField]
    private float stabilizationTorque=2f; // �������� ������������ ������� �����
    [SerializeField]
    private float maxSpeed = 5f;
    [SerializeField]
    private Collider2D collider1;
    [SerializeField]// ������ ���������
    private Collider2D collider2;
    [SerializeField]
    private int startSpeed;
 

    // ������ ���������

    [SerializeField] 
    private Collider2D targetCollider; // ��������������� ��������� �������� �������
    
    void Start()
    {
      
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.centerOfMass = _centerOfMass.localPosition;
        sprite = GetComponent<SpriteRenderer>();
        startSpeed = speed.value;
    }

 

    void Update()
    {
        float unitsPerPixel = sceneWidth / Screen.width;
        float desiredHalfHeight = 3f * unitsPerPixel * Screen.height;
        float desiredDownHeight = 2f * unitsPerPixel * Screen.height;
        if (Input.GetKey("a"))
        {
            movementX = -1;
            if (flip == true)
            {
                flip = false;
            }
        }
        else if (Input.GetKey("d"))
        {
            movementX = 1;
            if (flip == false)
            {
                flip = true;
            }
        }
        
    }
    public void OnLeftButtonDown()
    {
        //Debug.Log("Left");
        movementX = -1;
        if (flip == true)
        {
            flip = false;
        }
    }
    public void OnRightButtonDown()
    {
        movementX = 1;
        if (flip == false)
        {
            flip = true;
        }
    }
    public void OnButtonsUp()
    {
        movementX = 0;

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag == "Enemy")
        {
            //stabilizationTorque = 100000;
            speed.value = 0;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Enemy")
        {
           // stabilizationTorque= 10;
            transform.position = transform.position;
            speed.value = startSpeed;
        }
        
    }
    void FixedUpdate()
    {
        if (movementX == 0 && collider1.IsTouching(targetCollider) && collider2.IsTouching(targetCollider))
        {
            // ��������� ��������, ���� ��� ���������� ������������ � targetCollider
            rb.velocity = Vector2.zero;
            rb.constraints = RigidbodyConstraints2D.FreezePosition| RigidbodyConstraints2D.FreezeRotation;
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
    private void LimitTankRotation()
    {
        // �������� ������� ���� ������� �����
        float currentRotation = transform.eulerAngles.z;

        // �������� ���� � ��������� [-180, 180] ��� �������� ���������
        if (currentRotation > 180)
            currentRotation -= 360;

        // ���� ������ ����� ��������� ������������ ���� �������, ���������� ���
        if (Mathf.Abs(currentRotation) > maxTiltAngle)
        {
            // ��������� ����������� � �������� ���� ��� ������������
            float torqueDirection = -Mathf.Sign(currentRotation); // ��������������� ������ ����
            rb.AddTorque(torqueDirection * stabilizationTorque);
        }
    }
}

