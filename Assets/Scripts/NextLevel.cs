using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    
    [SerializeField]
    private int nextSceneNumber;//����� ��������� �����
    [SerializeField]
    private bool enter;//������ ����� �� � ���� ��� ���
    [SerializeField]
    private int thisLevel;//��������� ����� �� ����� ������ �������
    public static int tLevel;//���������� ��� ������ 
    
    public static int sceneIndex;

    public void Start()
    {
        var activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("SavedLevel", activeSceneIndex);
        PlayerPrefs.Save();
        thisLevel = activeSceneIndex;
        nextSceneNumber = activeSceneIndex + 1;//�������� ����� ��������� �����
    }
 
    public void OnTriggerStay2D(Collider2D collision)//������� ��� �������� ������ �� ��������� �������
    {
        if (collision.gameObject.tag == "Player")
        {

            SceneManager.LoadScene(nextSceneNumber);
          

        }

    }
}
