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

    public void Start()
    {
       
    }
    public void Update()
    {

    }
    // Start is called before the first frame update
    public void NewGame()
    {
        PlayerPrefs.DeleteAll();

        SceneManager.LoadScene(9);
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
        SceneManager.LoadScene(1);
        
    }
    public void OnAngarButtonDown()
    {
        SceneManager.LoadScene(7);
        
    }
    public void OnPlayButtonDown() 
    {
        nextLevel = PlayerPrefs.GetInt("NextLevel");
        if (nextLevel == 1)
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
