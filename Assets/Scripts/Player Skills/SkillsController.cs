using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsController : MonoBehaviour
{
    [SerializeField]
    private GameObject objectPrefab; // ������ �������, ������� ����� ��������
    [SerializeField]
    private RectTransform canvasTransform; // ������
    [SerializeField]
    private float minY = -200f; // ����������� �������� �� ��� Y
    [SerializeField]
    private float maxY = 200f; // ������������ �������� �� ��� Y
    [SerializeField]
    private int spawnCount = 5; // ���������� �������� ��� ������

    // ������, ������������ �������� ����� ����������� �����
    [SerializeField]
    private RectTransform referenceObject;

    

    public void TriggerArtillery()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            // ��������� ��������� ������� �� ��� Y � �������� ���������
            float randomY = Random.Range(minY, maxY);

            // ������� ������ � ����������� ��� � �������
            GameObject newObject = Instantiate(objectPrefab, canvasTransform);
            RectTransform newObjectRect = newObject.GetComponent<RectTransform>();

            // ������������� ��������� ������� ������������ ������������� �������
            newObjectRect.anchoredPosition = new Vector2(referenceObject.anchoredPosition.x, randomY);
        }
    }
}
