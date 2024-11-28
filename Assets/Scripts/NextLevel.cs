using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    
    [SerializeField]
    private int nextSceneNumber;//номер следующей сцены
    public int enter=0;//узнаем вошли мы в зону или нет
    [SerializeField]
    private int thisLevel;//указываем какой по счёту данный уровень
    public static int tLevel;//переменная для ссылки 
    
    public static int sceneIndex;
    public bool o;

    [SerializeField]
    private AngarController buy;
     
    public void Start()
    {
        var activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("SavedLevel", activeSceneIndex);
        PlayerPrefs.Save();
        thisLevel = activeSceneIndex;
        nextSceneNumber = activeSceneIndex + 1;//получаем номер следующей сцены
    }
 
    public void OnTriggerStay2D(Collider2D collision)//функция для перехода игрока на следующий уровень
    {
        if (collision.gameObject.tag == "Player")
        {
            if (buy.goBuy == 0)
            {
                SceneManager.LoadScene(nextSceneNumber);
            }
            else if(buy.goBuy == 1)
            {
                SceneManager.LoadScene(nextSceneNumber);
                  enter = 1;
            }
            if (enter==1)
            {
                SceneManager.LoadScene(6);
            }

        }

    }
}
