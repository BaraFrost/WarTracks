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
    void Start()
    {
        spawnRange = Random.Range(1, maxRange);
        
    }
    void Update()
    {
       
        if (value <= 0f)
        {
           if(gameObject.tag=="Player")
           {
                var activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
                PlayerPrefs.SetInt("SavedLevel", activeSceneIndex);
                SceneManager.LoadScene(7);
           }
           else
           {
                
                if(spawnRange>mediumRange && isHeeling==true  )
                {   
                   // Debug.Log("ELSE");
                    Instantiate(heeling, heelPosition.transform.position, transform.rotation);
                }
                earn.OnCoinEarn();
                Destroy(gameObject);
                
                if(spawnDead==true)
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
}
