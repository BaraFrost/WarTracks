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
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame


   /* public void OnUpButtonDown()
    {
        T = true;
        // gunRotation.z += rotationSpeed * Time.deltaTime;

    }

    public void OnDownButtonDown()
    {
        S = true;
        // gunRotation.z -= rotationSpeed * Time.deltaTime;

    }

    public void OnButtonsUp()
    {
        T = false;
        S = false;
        gunRotation.z = gunRotation.z;
    }
   */
    void Update()
    {

        rotZ = player.transform.rotation.z;//посмотри и исправь всё тут только 69 71 72 79 родные остальное ты дописал вчера не работает как надо нужно чтобы при изменении угла менялся рэёндж и прицел не дергался в зависимости на что ты наехал и какой угол у танка может это можно и по простому сделать тупо выставив пушку

       
        // absRotZ = Mathf.Abs(rotZ);
        //rotX=rotationRange.x;
        // rotY=rotationRange.y;
        if (joystick.Horizontal > 0 && joystick.Vertical != 0)
        {

            // moveVector.z = Mathf.Clamp(moveVector.z < 180 ? moveVector.z : moveVector.z - 360, rotationRange.x, rotationRange.y);


            if (rotZ< 0.1 && rotZ>-0.1)
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

        /*if (T == true)
        {
            gunRotation.z += rotationSpeed * Time.deltaTime;
            gunRotation.z = Mathf.Clamp(gunRotation.z, rotationRange.x, rotationRange.y);
            transform.localRotation = Quaternion.Euler(gunRotation);
        }
        if (S == true)
        {
            gunRotation.z -= rotationSpeed * Time.deltaTime;
            gunRotation.z = Mathf.Clamp(gunRotation.z, rotationRange.x, rotationRange.y);
            transform.localRotation = Quaternion.Euler(gunRotation);
        }
    }
    // gunRotation.z = Mathf.Clamp(gunRotation.z, rotationRange.x, rotationRange.y);
    // transform.localRotation = Quaternion.Euler(gunRotation);

    // Vector3 gunRotations = new Vector3(gunRotation.z,rotationRange.x,rotationRange.y);
    // transform.localRotation = Quaternion.Euler(gunRotations);
}*/

