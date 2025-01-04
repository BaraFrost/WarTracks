using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponController : MonoBehaviour
{

    Rigidbody2D rb;
    private Vector3 gunRotation;
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    private Vector2 rotationRange;
    [SerializeField]
    private Vector2 rotationRanger;
    [SerializeField]
    private Vector2 rotationRangeL;
    public bool T = false;
    public bool S = false;
    private double Y = 0.5;
    private bool i = false;
    public Joystick joystick;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private float rotZ;
    [SerializeField]
    private float rotX;
    [SerializeField]
    private float rotY;
    [SerializeField]
    private float absRotZ;
    [SerializeField]
    private float x;
    [SerializeField]
    private float y;

    public enum FireMode { Parabolic, Straight }
    public FireMode CurrentFireMode { get; private set; } = FireMode.Parabolic; // Режим стрельбы
    public float CurrentAngle { get; private set; }
    public void ToggleFireMode()
    {
        // Переключение режима
        CurrentFireMode = (CurrentFireMode == FireMode.Parabolic) ? FireMode.Straight : FireMode.Parabolic;
    }
    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
    }

   
    void Update()
    {

        rotZ = player.transform.rotation.z;

        

        Vector3 joystickDirection = Quaternion.LookRotation(Vector3.forward, Vector3.up * joystick.Horizontal + Vector3.left * joystick.Vertical).eulerAngles;
        CurrentAngle = Mathf.Clamp(joystickDirection.z < 180 ? joystickDirection.z : joystickDirection.z - 360, -180, 180);

        if (joystick.Horizontal > 0 && joystick.Vertical != 0)
        {



            if (rotZ < 0.1 && rotZ > -0.1)
            {

                Vector3 moveVector = Quaternion.LookRotation(Vector3.forward, Vector3.up * joystick.Horizontal + Vector3.left * joystick.Vertical).eulerAngles;
                moveVector.z = Mathf.Clamp(moveVector.z < 180 ? moveVector.z : moveVector.z - 360, rotationRange.x, rotationRange.y);
                transform.rotation = Quaternion.Euler(moveVector);

            }
            else if (rotZ >= 0.1)
            {

                Vector3 moveVector = Quaternion.LookRotation(Vector3.forward, Vector3.up * joystick.Horizontal + Vector3.left * joystick.Vertical).eulerAngles;
                moveVector.z = Mathf.Clamp(moveVector.z < 180 ? moveVector.z : moveVector.z - 360, rotationRanger.x, rotationRanger.y);
                transform.rotation = Quaternion.Euler(moveVector);

            }

            else if (rotZ <= -0.1)
            {

                Vector3 moveVector = Quaternion.LookRotation(Vector3.forward, Vector3.up * joystick.Horizontal + Vector3.left * joystick.Vertical).eulerAngles;
                moveVector.z = Mathf.Clamp(moveVector.z < 180 ? moveVector.z : moveVector.z - 360, rotationRangeL.x, rotationRangeL.y);
                transform.rotation = Quaternion.Euler(moveVector);

            }

        }



    }

}

  