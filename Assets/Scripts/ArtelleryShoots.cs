using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtelleryShoots : MonoBehaviour
{
    [SerializeField]
    private GameObject gun;

    [SerializeField]
    private int bulletCount;

    [SerializeField]
    private float nextFireTime;

    [SerializeField]
    private float fireRate;

    [SerializeField]
    private float minAngle;

    [SerializeField]
    private float maxAngle;

    [SerializeField]
    private ArtelleryBullet bullet;

    [SerializeField]
    private Transform shotPosition;

    void Update()
    {
        
        gun.transform.rotation = Quaternion.Euler(0.0f, 180, 45);
        for (int x = bulletCount; x > 0; x--)
        {
            nextFireTime -= Time.deltaTime;
            if (nextFireTime <= 0)
            {
                Instantiate(bullet, shotPosition.transform.position, transform.rotation);
                nextFireTime = fireRate;
            }
        }
    }
}
