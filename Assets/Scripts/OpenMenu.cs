using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OpenMenu : MonoBehaviour
{
    //[SerializeField]
    //private CoinCount value;
    [SerializeField]
    private NextLevel thLevel;
    [SerializeField]
    private TMP_Text level;

    [SerializeField]
    private int nextLevel;

    private int enLevel;

    private int firstArt;
    private int firstHeel;
    private int firstDR;

    
    public void NewGame()
    {
        PlayerPrefs.DeleteAll();

        firstArt = 1;
        firstHeel = 3;
        firstDR = 2;

        PlayerPrefs.SetInt("Bomb", firstArt);
        PlayerPrefs.SetInt("DR", firstDR);
        PlayerPrefs.SetInt("Heel", firstHeel);
        PlayerPrefs.Save();

        SceneManager.LoadScene(17);
        /*PlayerPrefs.DeleteKey("Coin");
        PlayerPrefs.DeleteKey("12ShadowOff"); 
        PlayerPrefs.DeleteKey("12TankBue");
        PlayerPrefs.DeleteKey("22ShadowOff");
        PlayerPrefs.DeleteKey("22TankBue");
        PlayerPrefs.DeleteKey("13ShadowOff");
        PlayerPrefs.DeleteKey("13TankBue");
        PlayerPrefs.DeleteKey("23ShadowOff");
        PlayerPrefs.DeleteKey("23TankBue");*/
    }
    public void OnMenuButtonDown()
    {
        SceneManager.LoadScene(0);
       
    }
    public void OnFirstButtonDown()
    {

        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(1);

        
    }
    public void OnFirstLevelButtonDown()
    {
        SceneManager.LoadScene(1);

    }
    public void OnAngarButtonDown()
    {
        SceneManager.LoadScene(16);
        
    }
    public void OnPlayButtonDown() 
    {
        enLevel = PlayerPrefs.GetInt("EndLevel");
        if(enLevel==1)
        {
            enLevel = 0;
            PlayerPrefs.SetInt("EndLevel", enLevel);
            SceneManager.LoadScene("SavedLevel");
            PlayerPrefs.Save();
        }
        nextLevel = PlayerPrefs.GetInt("NextLevel");
        if (nextLevel == 1)                                      //тут потом посмотришь нужно исправить, часть скрипта не нужна
        {
            var nextSceneIndex = PlayerPrefs.GetInt("SavedLevel") + 1;
            SceneManager.LoadScene(nextSceneIndex);
            nextLevel = 0;
            PlayerPrefs.SetInt("NextLevel", nextLevel);
            PlayerPrefs.Save();
        }
        else
        {
            var activeSceneIndex = PlayerPrefs.GetInt("SavedLevel");
            SceneManager.LoadScene(activeSceneIndex);
            
        }
        
    }
}
