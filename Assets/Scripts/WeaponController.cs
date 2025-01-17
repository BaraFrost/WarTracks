using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponController : MonoBehaviour
{
    Rigidbody2D rb;
    private Vector3 gunRotation;
    [SerializeField]
    private Vector2 rotationRange;    // �������� ����� ��� ������� ���������
    [SerializeField]
    private Vector2 rotationRanger;  // �������� ����� ��� �������� ���������
    [SerializeField]
    private Vector2 rotationRangeL;  // �������� ����� ��� ������� ���������
    public Joystick joystick;
    [SerializeField]
    private GameObject player;

    private float rotZ; // ������� ���� �������� �����
    private float lastTurretAngle; // ���������� ���� ������

    public enum FireMode { Parabolic, Straight }
    public FireMode CurrentFireMode { get; private set; } = FireMode.Parabolic;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // ��������� ���������� ���� �����
        lastTurretAngle = 0f;
        transform.rotation = Quaternion.Euler(0, 0, lastTurretAngle);
    }

    void Update()
    {
        // ��������� �������� ���� ������ �� ��� Z
        rotZ = player.transform.rotation.eulerAngles.z;
        if (rotZ > 180) rotZ -= 360; // �������������� ���� � �������� [-180, 180]

        Vector3 moveVector;

        // ����������� ��������� �������� ����� � ����������� �� ���� �������� �����
        if (rotZ > 20) // ������� ��������
        {
            moveVector = GetClampedRotation(rotationRanger);
        }
        else if (rotZ < -20) // ������ ��������
        {
            moveVector = GetClampedRotation(rotationRangeL);
        }
        else // ������ ��������
        {
            moveVector = GetClampedRotation(rotationRange);
        }

        // ��������� ������� ������ ���� �������� ������������
        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            lastTurretAngle = moveVector.z;
        }

        transform.rotation = Quaternion.Euler(0, 0, lastTurretAngle);
    }

    // ����� ��� ��������� ������������� �������� � ������ ���������
    private Vector3 GetClampedRotation(Vector2 range)
    {
        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            Vector3 joystickDirection = Quaternion.LookRotation(Vector3.forward, Vector3.up * joystick.Horizontal + Vector3.left * joystick.Vertical).eulerAngles;
            float joystickAngle = Mathf.Clamp(joystickDirection.z < 180 ? joystickDirection.z : joystickDirection.z - 360, range.x, range.y);
            return new Vector3(0, 0, joystickAngle);
        }

        return new Vector3(0, 0, lastTurretAngle);
    }
}

  