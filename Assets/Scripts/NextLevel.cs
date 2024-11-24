using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    
    [SerializeField]
    private int nextSceneNumber;//номер следующей сцены
    [SerializeField]
    private bool enter;//узнаем вошли мы в зону или нет
    [SerializeField]
    private int thisLevel;//указываем какой по счёту данный уровень
    public static int tLevel;//переменная для ссылки 
    
    public static int sceneIndex;

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

            SceneManager.LoadScene(nextSceneNumber);
          

        }

    }
}
