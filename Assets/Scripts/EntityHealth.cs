using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EntityHealth : MonoBehaviour
{
    [SerializeField]
    private Transform heelPosition;
    [SerializeField]
    private GameObject smoke;
    [SerializeField]
    private GameObject heeling;
    public float value = 100f;
    public Image Bar;
    [SerializeField]
    private float maxValue = 100f;
    [SerializeField]
    private TMP_Text health;
    [SerializeField]
    private GameObject dead;
    [SerializeField]
    private int spawnRange;
    [SerializeField]
    private int maxRange = 10;
    [SerializeField]
    private int mediumRange = 5;
    [SerializeField]
    private CoinEarn earn;
    [SerializeField]
    private bool isHeeling = true;
    [SerializeField]
    private bool spawnDead = true;
    [SerializeField]
    private bool isDeadracer = false;
    [SerializeField]
    private AudioClip shootSound;
    [SerializeField]
    private AudioSource audioSource;

    private bool isTransitioning = false;

    void Start()
    {
        spawnRange = Random.Range(1, maxRange);
        audioSource = GetComponent<AudioSource>();
        UpdateHealthUI();
    }

    void Update()
    {
        if (value <= 0f)
        {
            HandleDeath();
            return;
        }

        // Ограничение максимального здоровья
        value = Mathf.Clamp(value, 0f, maxValue);

        // Обновление UI здоровья
        UpdateHealthUI();
    }

    private void HandleDeath()
    {
        if (isTransitioning) return;

        isTransitioning = true;

        if (gameObject.tag == "Player")
        {
            PlaySoundOnDestroy();

            // Сохранение уровня
            int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
            PlayerPrefs.SetInt("SavedLevel", activeSceneIndex);

            // Создание эффекта дыма
            Instantiate(smoke, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
            Time.timeScale = 0;
            // Переход на следующую сцену
            GameSceneLoader.Instance.LoadSceneAfterDelay(16, 0.3f);
        }
        else
        {
            if (spawnRange > mediumRange && isHeeling)
            {
                Instantiate(heeling, heelPosition.position, transform.rotation);
            }

            PlaySoundOnDestroy();

            if (earn != null)
            {
                earn.OnCoinEarn();
            }

            if (spawnDead)
            {
                Instantiate(dead, transform.position, transform.rotation);
            }

            if (isDeadracer)
            {
                Instantiate(smoke, transform.position, transform.rotation);
            }

            Instantiate(smoke, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
    }

    private void UpdateHealthUI()
    {
        if (Bar != null)
        {
            Bar.fillAmount = value / maxValue;
        }

        if (health != null)
        {
            health.text = Mathf.CeilToInt(value).ToString();
        }
    }

    private void PlaySoundOnDestroy()
    {
        if (shootSound == null || audioSource == null) return;

        GameObject tempSoundObject = new GameObject("TempSound");
        AudioSource tempAudioSource = tempSoundObject.AddComponent<AudioSource>();

        tempAudioSource.clip = shootSound;
        tempAudioSource.volume = audioSource.volume;
        tempAudioSource.pitch = audioSource.pitch;
        tempAudioSource.Play();

        Destroy(tempSoundObject, shootSound.length);
    }
}
