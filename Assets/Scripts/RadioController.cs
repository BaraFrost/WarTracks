using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RadioController : MonoBehaviour
{
    [SerializeField]
    private float distToSpawn;
    [SerializeField]
    private Transform player;
    [SerializeField]
    private GameObject spawnPoint;
    [SerializeField]
    private float timer;
    [SerializeField]
    private float distanc;
    [SerializeField]
    private GameObject derejabl;
    [SerializeField]
    private bool derejablSpawn=false;
    [SerializeField]
    private Image radioHealth;
    [SerializeField]
    private float startTimer;

    [SerializeField]
    private AudioClip instantiateDerejabl;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        startTimer = timer;
    }

    // Update is called once per frame
    void Update()
    {
        radioHealth.fillAmount = timer / startTimer;
        distanc = (float)Vector2.Distance(transform.position, player.position);
        if(distanc <= distToSpawn && derejablSpawn==false) 
        {
            timer-=Time.deltaTime;
            if(timer<=0)
            {
                PlaySound(instantiateDerejabl);
                Instantiate(derejabl, spawnPoint.transform.position, transform.rotation);
                timer = startTimer;

               
                //derejablSpawn = true;
            }
            
        }
      
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip == null) return;

        // Создаём временный объект для звука
        GameObject tempSoundObject = new GameObject("TempAudio");
        AudioSource tempAudioSource = tempSoundObject.AddComponent<AudioSource>();

        // Настраиваем параметры звука
        tempAudioSource.clip = clip;
        tempAudioSource.volume = 0.35f; // Настройте громкость
        tempAudioSource.spatialBlend = 0; // 2D звук
        tempAudioSource.Play();

        // Уничтожаем объект после воспроизведения звука
        Destroy(tempSoundObject, clip.length);
    }
}
