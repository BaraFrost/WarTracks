using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField]
    private Transform shotPosition;
    [SerializeField]
    private Bullet bullet;
    [SerializeField]
    private int bulletCount;

    [SerializeField]
    private float reloadTime;
    [SerializeField]
    private float repeatTime;
    
    private Bullet damage;
    private Bullet minAngle;
    private Bullet maxAngle;
    private Bullet lifeTime;

    public Image reload;

    [SerializeField]
    private float dmg;
    [SerializeField]
    private int mnAn;
    [SerializeField]
    private int mxAn;
    [SerializeField]
    private int lT;
    [SerializeField]
    private float nextFireTime;
    [SerializeField]
    private float fireRate;
    [SerializeField]
    private Joystick joystick;
    [SerializeField]
    private float joystikShootRadius;
    [SerializeField]
    private bool drob=false;
    [SerializeField]
    private float reloadTimer;
    [SerializeField]
    private bool shoot;

    [SerializeField]
    private AudioClip shootSound;
    [SerializeField]
    private AudioSource audioSource;
    void Start()
    {
        StartCoroutine(Shoot());
        damage = GetComponent<Bullet>();
        minAngle =GetComponent<Bullet>();
        maxAngle = GetComponent<Bullet>();
        lifeTime = GetComponent<Bullet>();
        //dmg = Bullet.damage;
        nextFireTime = fireRate;
        reloadTimer = reloadTime;
        reload.fillAmount = reloadTimer / reloadTime;

        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (shoot)
        {
            // Уменьшаем таймер и обновляем fillAmount изображения
            reloadTimer -= Time.deltaTime;
            reload.fillAmount = reloadTimer / reloadTime;

            // Если перезарядка завершена, сбрасываем состояние
            if (reloadTimer <= 0)
            {
                shoot = false;
                reloadTimer = reloadTime;
                reload.fillAmount = reloadTimer / reloadTime;
            }
        }
    }

    public void StartReload()
    {
        if (!shoot)
        {
            shoot = true;
            reloadTimer = reloadTime;
        }
    }
    public void OnFireDown()
    {
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

    public void OnFireUp()
    {
        nextFireTime -= Time.deltaTime;
    }
    private IEnumerator Shoot()
    {
        while (true)
        { 
            

            if (Input.GetKeyDown(KeyCode.Space)||joystick.Direction.magnitude>joystikShootRadius && joystick.Horizontal>0)
            {

                for (int x = bulletCount; x > 0; x--)
                {
                    if (drob == true)
                    {
                    Instantiate(bullet, shotPosition.transform.position, transform.rotation);
                        audioSource.PlayOneShot(shootSound);
                        Instantiate(bullet, shotPosition.transform.position, transform.rotation);
                        audioSource.PlayOneShot(shootSound);
                        Instantiate(bullet, shotPosition.transform.position, transform.rotation);
                        audioSource.PlayOneShot(shootSound);
                        Instantiate(bullet, shotPosition.transform.position, transform.rotation);
                        audioSource.PlayOneShot(shootSound);

                    }
                    Instantiate(bullet, shotPosition.transform.position, transform.rotation);
                    audioSource.PlayOneShot(shootSound);
                    yield return new WaitForSeconds(repeatTime);
                    shoot = true;
                    reloadTimer = reloadTime;
                }
                yield return new WaitForSeconds(reloadTime);
            }
            yield return null;

           


        }
    }
}
