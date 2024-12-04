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
    public float value=100f;
    public Image Bar;
    [SerializeField]
    private float maxValue;
    [SerializeField]
    private TMP_Text health;
    [SerializeField]
    private GameObject dead;
    [SerializeField]
    private int spawnRange;
    [SerializeField]
    private int maxRange=10;
    [SerializeField]
    private int mediumRange=5;
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
    }
    void Update()
    {
       
        if (value <= 0f)
        {
            if (gameObject.tag == "Player" && !isTransitioning)
            {
                isTransitioning = true;
                PlaySoundOnDestroy();
                // ���������� ������
                var activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
                PlayerPrefs.SetInt("SavedLevel", activeSceneIndex);

                // �������� ������� ����
                Instantiate(smoke, transform.position, Quaternion.identity);
                
                // ������ �������� �� ��������� �����
                StartCoroutine(LoadSceneAfterDelay(0.5f)); // 0.5 ������� ��������
            }
            else
            {

                if (spawnRange > mediumRange && isHeeling == true)
                {
                    // Debug.Log("ELSE");
                    Instantiate(heeling, heelPosition.transform.position, transform.rotation);
                }

                PlaySoundOnDestroy();

                earn.OnCoinEarn();
                Destroy(gameObject);

                if (spawnDead == true)
                {
                    Instantiate(dead, transform.position, transform.rotation);


                }
                if (isDeadracer == true)
                {
                    Instantiate(smoke, transform.position, transform.rotation);
                    Instantiate(smoke, transform.position, transform.rotation);


                }
                Instantiate(smoke, transform.position, transform.rotation);
            }
        }
        if(value>=maxValue)
        {
            value = maxValue;
        } 
        if(Bar == null || health == null )
        {
            return;
        }
      Bar.fillAmount = value/maxValue;
      health.text = value.ToString();

    }

    private void PlaySoundOnDestroy()
    {
        // ������ ��������� ������ ��� �����
        GameObject tempSoundObject = new GameObject("TempSound");
        AudioSource tempAudioSource = tempSoundObject.AddComponent<AudioSource>();

        tempAudioSource.clip = shootSound;
        tempAudioSource.volume = audioSource.volume; // ������������� �� �� ���������, ��� � � ��������� �������
        tempAudioSource.pitch = audioSource.pitch;   // ������������� ��� �� ���
        tempAudioSource.Play();

        // ������� ��������� ������ ����� ���������� �����
        Destroy(tempSoundObject, shootSound.length);
    }

        private System.Collections.IEnumerator LoadSceneAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            SceneManager.LoadScene(8);
        }
    }
