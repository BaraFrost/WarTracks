using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyShoot : MonoBehaviour
{

    [SerializeField]
    private GameObject gun;

    [SerializeField]
    private float dist; // Дистанция стрельбы
    [SerializeField]
    private Bullet bullet;
    [SerializeField]
    private Transform shotPosition;
    [SerializeField]
    private int bulletCount;
    [SerializeField]
    private float fireRate; // Скорострельность
    [SerializeField]
    private AudioClip shootSound;
    [SerializeField]
    private AudioSource audioSource;

    private float nextFireTime;
    private Transform target;

    private void Start()
    {
        nextFireTime = fireRate;
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        FindClosestTarget(); // Постоянно ищем ближайшую цель

        if (target == null)
            return;

        // Поворачиваем пушку в сторону цели
        Vector3 difference = target.position - shotPosition.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        gun.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);

        // Проверяем расстояние и стреляем
        float currentDistance = Vector3.Distance(target.position, transform.position);
        if (currentDistance <= dist)
        {
            nextFireTime -= Time.deltaTime;
            if (nextFireTime <= 0)
            {
                Shoot();
                nextFireTime = fireRate;
            }
        }
    }

    private void Shoot()
    {
        Instantiate(bullet, shotPosition.position, gun.transform.rotation);
        if (shootSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(shootSound);
        }
    }

    private void FindClosestTarget()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        GameObject[] friendlies = GameObject.FindGameObjectsWithTag("Friendly");
        GameObject[] allTargets = CombineArrays(players, friendlies);

        float closestDistance = Mathf.Infinity;
        Transform closestTarget = null;

        foreach (GameObject obj in allTargets)
        {
            if (obj == null || !obj.activeInHierarchy)
                continue;

            float distance = Vector3.Distance(transform.position, obj.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestTarget = obj.transform;
            }
        }

        target = closestTarget;
    }

    private GameObject[] CombineArrays(GameObject[] array1, GameObject[] array2)
    {
        GameObject[] combined = new GameObject[array1.Length + array2.Length];
        array1.CopyTo(combined, 0);
        array2.CopyTo(combined, array1.Length);
        return combined;
    }

}
