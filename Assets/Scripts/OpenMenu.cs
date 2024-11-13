using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OpenMenu : MonoBehaviour
{
    [SerializeField]
    private CoinCount value;
    [SerializeField]
    private NextLevel thLevel;
    [SerializeField]
    private TMP_Text level;
    

    public void Start()
    {
       
    }
    public void Update()
    {

    }
    // Start is called before the first frame update
    public void NewGame()
    {
        Application.LoadLevel(8);
        PlayerPrefs.DeleteKey("TankID"); 
        PlayerPrefs.DeleteKey("Coin");
        PlayerPrefs.DeleteKey("12ShadowOff"); 
        PlayerPrefs.DeleteKey("12TankBue");
        PlayerPrefs.DeleteKey("22ShadowOff");
        PlayerPrefs.DeleteKey("22TankBue");
        PlayerPrefs.DeleteKey("13ShadowOff");
        PlayerPrefs.DeleteKey("13TankBue");
        PlayerPrefs.DeleteKey("23ShadowOff");
        PlayerPrefs.DeleteKey("23TankBue");
    }
    public void OnMenuButtonDown()
    {
        Application.LoadLevel(0);
       
    }
    public void OnFirstButtonDown()
    {
        Application.LoadLevel(1);
        
    }
    public void OnAngarButtonDown()
    {
        Application.LoadLevel(6);
        
    }
    public void OnPlayButtonDown() 
    {
        var activeSceneIndex = PlayerPrefs.GetInt("SavedLevel");
        Application.LoadLevel(activeSceneIndex);
        value.CoinValue();
        Debug.Log(activeSceneIndex);
    }
}
