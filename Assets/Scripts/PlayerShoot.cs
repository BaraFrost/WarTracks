using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Transform shotPosition; // Позиция стрельбы
    [SerializeField] private Bullet parabolicBullet; // Снаряд для параболической стрельбы
    [SerializeField] private Bullet straightBullet;  // Снаряд для прямой стрельбы
    [SerializeField] private int bulletCount;

    [SerializeField] private float reloadTime;
    [SerializeField] private float repeatTime;

    [SerializeField] private Image reload;

    [SerializeField] private float fireRate;
    [SerializeField] private Joystick joystick;
    [SerializeField] private float joystickShootRadius;
    [SerializeField] private AudioClip shootSound;
    [SerializeField] private AudioSource audioSource;

    private float reloadTimer;
    private bool shoot;
    [SerializeField] private bool drob; // Условие для стрельбы дробью

    // Перечисление для режима стрельбы
    public enum FireMode { Parabolic, Straight }
    public FireMode CurrentFireMode { get; private set; } = FireMode.Parabolic;

    private void Start()
    {
        StartCoroutine(ShootCoroutine());
        reloadTimer = reloadTime;
        reload.fillAmount = reloadTimer / reloadTime;
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (shoot)
        {
            reloadTimer -= Time.deltaTime;
            reload.fillAmount = reloadTimer / reloadTime;

            if (reloadTimer <= 0)
            {
                shoot = false;
                reloadTimer = reloadTime;
                reload.fillAmount = reloadTimer / reloadTime;
            }
        }
    }

    public void ToggleFireMode()
    {
        // Переключение режима
        CurrentFireMode = (CurrentFireMode == FireMode.Parabolic) ? FireMode.Straight : FireMode.Parabolic;
    }

    private IEnumerator ShootCoroutine()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Space) || (joystick.Direction.magnitude > joystickShootRadius && joystick.Horizontal > 0))
            {
                for (int i = 0; i < bulletCount; i++)
                {
                    ShootBullet();
                    audioSource.PlayOneShot(shootSound);
                    yield return new WaitForSeconds(repeatTime);
                }

                shoot = true;
                reloadTimer = reloadTime;
                yield return new WaitForSeconds(reloadTime);
            }
            yield return null;
        }
    }

    private void ShootBullet()
    {
        // Выбор типа снаряда на основе текущего режима стрельбы
        Bullet bulletToShoot = (CurrentFireMode == FireMode.Parabolic) ? parabolicBullet : straightBullet;

        if (bulletToShoot == null)
        {
            return;
        }

        // Создание основного снаряда
        Instantiate(bulletToShoot, shotPosition.position, shotPosition.rotation);

        // Проверка на условие "дробь" и создание дополнительных снарядов
        if (drob)
        {
            Instantiate(parabolicBullet, shotPosition.position, Quaternion.Euler(shotPosition.eulerAngles ));
            Instantiate(parabolicBullet, shotPosition.position, Quaternion.Euler(shotPosition.eulerAngles ));
            Instantiate(parabolicBullet, shotPosition.position, Quaternion.Euler(shotPosition.eulerAngles ));
            Instantiate(parabolicBullet, shotPosition.position, Quaternion.Euler(shotPosition.eulerAngles ));
        }
    }
}
