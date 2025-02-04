using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;
using PlayerPrefs = RedefineYG.PlayerPrefs;

public class NextLevel : MonoBehaviour
{
    [SerializeField] private int nextSceneNumber;
    public int enter = 0;
    [SerializeField] private int thisLevel;
    public static int tLevel;
    public static int sceneIndex;
    public bool o;

    [SerializeField] private AngarController buy;
    [SerializeField] private int level;

    public int endLevel;

    public void Start()
    {
        var activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("SavedLevel", activeSceneIndex);
        PlayerPrefs.Save();
        thisLevel = activeSceneIndex;
        nextSceneNumber = activeSceneIndex + 1;
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            endLevel = 1;
            PlayerPrefs.SetInt("EndLevel", endLevel);
            PlayerPrefs.SetInt("SavedLevel", nextSceneNumber);
            SceneManager.LoadScene(16);

            

          
        }
    }
}
