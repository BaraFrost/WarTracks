using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseTank : MonoBehaviour
{
    [SerializeField]
    private BuyTank buyTank;
    [SerializeField]
    private int tank;
    public int tankNumber;
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
    public int tankID;
    [SerializeField]
    private bool tankViev = true;
    //[SerializeField]
    //private GameObject buttonPlay;
    [SerializeField]
    private bool newGame = false;
    [SerializeField]
    private Image gTank;
    [SerializeField]
    private Image bTank;
    [SerializeField]
    private int tChoose;
    void Start()
    {
        if(newGame==true)
        {
            PlayerPrefs.DeleteKey("TankID");
        }
        
        tankID=PlayerPrefs.GetInt("TankID");
        if (tankViev == true)
        {
            if (tankID == 1)
            {
                tank1.SetActive(true);
            }
            if (tankID == 2)
            {
                tank2.SetActive(true);
            }
            if (tankID == 12)
            {
                tank12.SetActive(true);
            }
            if (tankID == 22)
            {
                tank22.SetActive(true);
            }
            if (tankID == 13)
            {
                tank13.SetActive(true);
            }
            if (tankID == 23)
            {
                tank23.SetActive(true);
            }
        }
    }

    // Update is called once per frame
    
    public void OnTank1BottonDown()
    {
        tankNumber = 1;
        
    }
    public void OnTank2BottonDown()
    {
        tankNumber = 2;
        
       
    }
    public void OnTank12BottonDown()
    {
        tankNumber = 12;
        

    }
    public void OnTank22BottonDown()
    {
        tankNumber = 22;
        

    }
    public void OnTank13BottonDown()
    {
        tankNumber = 13;


    }
    public void OnTank23BottonDown()
    {
        tankNumber = 23;


    }
    public void OnDestroyButton()
    {
        PlayerPrefs.DeleteKey("TankID");
    }
    private void Update()
    {
        tank = tankNumber;


        if (tank == 1)
        {
            tank2.SetActive(false);
            tank22.SetActive(false);
            tank12.SetActive(false);
            tank23.SetActive(false);
            tank13.SetActive(false);
            PlayerPrefs.DeleteKey("TankID");
            PlayerPrefs.SetInt("TankID", tank);
            PlayerPrefs.Save();
            //Debug.Log(tank);
            if (tankViev == true)
            {
                tank1.SetActive(true);
            }
            tChoose = 1;
        }
        if (tank==2)//&& buyTank.tank22Bue == 1
        {
            tank1.SetActive(false);
            tank22.SetActive(false);
            tank12.SetActive(false);
            tank23.SetActive(false);
            tank13.SetActive(false);
            PlayerPrefs.DeleteKey("TankID");
            PlayerPrefs.SetInt("TankID", tank);
            PlayerPrefs.Save();
            //Debug.Log(tank);
            if (tankViev == true)
            {
                tank2.SetActive(true);
            }
            tChoose = 2;
        }
        if (tank == 12 && buyTank.tank12Bue == 1)
        {
            tank1.SetActive(false);
            tank2.SetActive(false);
            tank22.SetActive(false);
            tank23.SetActive(false);
            tank13.SetActive(false);
            PlayerPrefs.DeleteKey("TankID");
            PlayerPrefs.SetInt("TankID", tank);
            PlayerPrefs.Save();
            if (tankViev == true)
            {
                tank12.SetActive(true);
            }
            

        }
        if (tank == 22 && buyTank.tank22Bue == 1)
        {
            tank1.SetActive(false);
            tank2.SetActive(false);
            tank12.SetActive(false);
            tank23.SetActive(false);
            tank13.SetActive(false);
            PlayerPrefs.DeleteKey("TankID");
            PlayerPrefs.SetInt("TankID", tank);
            PlayerPrefs.Save();
            //Debug.Log(tank);
            if (tankViev == true)
            {
                tank22.SetActive(true);
            }
            

        }
        if (tank == 13 && buyTank.tank13Bue == 1)
        {
            tank1.SetActive(false);
            tank2.SetActive(false);
            tank12.SetActive(false);
            tank22.SetActive(false);
            tank23.SetActive(false);
            PlayerPrefs.DeleteKey("TankID");
            PlayerPrefs.SetInt("TankID", tank);
            PlayerPrefs.Save();
            if (tankViev == true)
            {
                tank13.SetActive(true);
            }
            

        }
        if (tank == 23 && buyTank.tank23Bue == 1)
        {
            tank1.SetActive(false);
            tank2.SetActive(false);
            tank12.SetActive(false);
            tank13.SetActive(false);
            tank22.SetActive(false);
            PlayerPrefs.DeleteKey("TankID");
            PlayerPrefs.SetInt("TankID", tank);
            PlayerPrefs.Save();
            //Debug.Log(tank);
            if (tankViev == true)
            {
                tank23.SetActive(true);
            }
           

        }
        if (newGame==true)
        {
            if(tChoose==2)
            {
                bTank.color= new Color (0.4339623f, 0.4339623f, 0.4339623f, 1f) ;
                gTank.color = new Color(1, 1, 1, 1);
            }
             else if (tChoose == 1)
            {
                gTank.color = new Color(0.4339623f, 0.4339623f, 0.4339623f, 1f);
                bTank.color = new Color(1, 1, 1, 1);
            }
        }
    }
}