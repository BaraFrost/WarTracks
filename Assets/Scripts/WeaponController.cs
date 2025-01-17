using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponController : MonoBehaviour
{
    Rigidbody2D rb;
    private Vector3 gunRotation;
    [SerializeField]
    private Vector2 rotationRange;    // Диапазон углов для прямого положения
    [SerializeField]
    private Vector2 rotationRanger;  // Диапазон углов для верхнего положения
    [SerializeField]
    private Vector2 rotationRangeL;  // Диапазон углов для нижнего положения
    public Joystick joystick;
    [SerializeField]
    private GameObject player;

    private float rotZ; // Текущий угол поворота танка
    private float lastTurretAngle; // Сохранённый угол орудия

    public enum FireMode { Parabolic, Straight }
    public FireMode CurrentFireMode { get; private set; } = FireMode.Parabolic;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Установка начального угла пушки
        lastTurretAngle = 0f;
        transform.rotation = Quaternion.Euler(0, 0, lastTurretAngle);
    }

    void Update()
    {
        // Получение текущего угла игрока по оси Z
        rotZ = player.transform.rotation.eulerAngles.z;
        if (rotZ > 180) rotZ -= 360; // Преобразование угла в диапазон [-180, 180]

        Vector3 moveVector;

        // Определение диапазона поворота башни в зависимости от угла поворота танка
        if (rotZ > 20) // Верхний диапазон
        {
            moveVector = GetClampedRotation(rotationRanger);
        }
        else if (rotZ < -20) // Нижний диапазон
        {
            moveVector = GetClampedRotation(rotationRangeL);
        }
        else // Прямой диапазон
        {
            moveVector = GetClampedRotation(rotationRange);
        }

        // Обновляем поворот только если джойстик используется
        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            lastTurretAngle = moveVector.z;
        }

        transform.rotation = Quaternion.Euler(0, 0, lastTurretAngle);
    }

    // Метод для получения ограниченного поворота с учётом джойстика
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

  