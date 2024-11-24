using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyShoot : MonoBehaviour
{

    public GameObject player;
    public GameObject gun;

    [SerializeField]
    private float dist;
    [SerializeField]
    private Bullet bullet;
    [SerializeField]
    private Transform shotPosition;
    [SerializeField]
    private int bulletCount;
    [SerializeField]
    private float reloadTime;
    [SerializeField]
    private float repeatTime;
    [SerializeField]
    private float nextFireTime;
    [SerializeField]
    private float fireRate;
    [SerializeField]
    private int damageCount;
    /*[SerializeField]
    private Bullet damage;*/
    [SerializeField]
    private float minAngle;
    [SerializeField]
    private float maxAngle;
    [SerializeField]
    private float lifeTime;

    [SerializeField]
    private AudioClip shootSound;
    [SerializeField]
    private AudioSource audioSource;
    private void Start()
    {
       player = GameObject.FindGameObjectWithTag("Player");
        nextFireTime = fireRate;
        //Bullet.damage = damageCount;
        //Bullet.minAngle = minAngle;
        //Bullet.maxAngle = maxAngle;
        //Bullet.lifeTime = lifeTime;
        audioSource = GetComponent<AudioSource>();

    }
    void Update()
    {
        
        Vector3 difference = player.transform.position - shotPosition.transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        gun.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);

        /*RaycastHit2D hit = Physics2D.Raycast(new Vector2(shotPosition.position.x, shotPosition.position.y), new Vector2(difference.x,difference.y), dist);
        if (hit.collider != null && hit.collider.gameObject.TryGetComponent<PlayerController>(out var health))*/
        
        var currentDistance = Vector3.Distance(player.transform.position, transform.position);
         
        if(currentDistance<=dist)
        {
           
            for (int x = bulletCount; x > 0; x--)
            {
                nextFireTime -= Time.deltaTime;
                if (nextFireTime <= 0)
                { 
                    Instantiate(bullet, shotPosition.transform.position, transform.rotation);
                    audioSource.PlayOneShot(shootSound);

                    nextFireTime = fireRate;
                }
            }
        
           
        }

    }
   
}
