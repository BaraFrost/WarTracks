using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankShooting : MonoBehaviour
{
    [SerializeField]
    private GameObject gun;

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

    private Transform targetEnemy;

    private void Start()
    {
        nextFireTime = fireRate;
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        // Находим ближайшего врага
        FindClosestEnemy();

        if (targetEnemy == null)
            return;

        // Поворачиваем пушку в сторону ближайшего врага
        Vector3 difference = targetEnemy.position - shotPosition.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        gun.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);

        // Проверяем дистанцию и стреляем
        float currentDistance = Vector3.Distance(targetEnemy.position, transform.position);
        if (currentDistance <= dist)
        {
            for (int x = bulletCount; x > 0; x--)
            {
                nextFireTime -= Time.deltaTime;
                if (nextFireTime <= 0)
                {
                    Instantiate(bullet, shotPosition.position, Quaternion.Euler(0.0f, 0.0f, rotationZ));
                    audioSource.PlayOneShot(shootSound);
                    nextFireTime = fireRate;
                }
            }
        }
    }

    private void FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float closestDistance = Mathf.Infinity;
        Transform closestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy.transform;
            }
        }

        targetEnemy = closestEnemy;
    }
}
