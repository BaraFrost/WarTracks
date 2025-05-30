using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerPrefs = RedefineYG.PlayerPrefs;

public class LoadTank : MonoBehaviour
{
    [SerializeField]
    private int tank;
    [SerializeField]
    private GameObject tank1;
    [SerializeField]
    private GameObject tank2;
    [SerializeField]
    private GameObject tank12;
    [SerializeField]
    private GameObject tank22;
    [SerializeField]
    private GameObject tank13;
    [SerializeField]
    private GameObject tank23;
    [SerializeField]
    private GameObject tank23Button;
   
    private void Awake()
    {
        tank = PlayerPrefs.GetInt("TankID");
        //Debug.Log(tank);
        if (tank ==1)
        {
            tank1.SetActive(true);
        }
        else if(tank ==2)
        {
            tank2.SetActive(true);
        }
        else if (tank == 12)
        {
            tank12.SetActive(true);
        }
        else if (tank == 22)
        {
            tank22.SetActive(true);
        }
        else if (tank == 13)
        {
            tank13.SetActive(true);
        }
        else if (tank == 23)
        {
            tank23.SetActive(true);

            tank23Button.SetActive(true);
           // tank23Artellery.SetActive(true);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
