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
                Instantiate(derejabl, spawnPoint.transform.position, transform.rotation);
                timer = startTimer;

               
                //derejablSpawn = true;
            }
            
        }
      
    }
}
