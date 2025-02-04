using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS : MonoBehaviour
{
    public static FPS Instance { get; private set; }

    private int frameCount = 0;
    private float deltaTime = 0f;
    private float fps = 0f;

    private void Awake()
    {
        // ���������� ���������
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // ������ ����������� ����� �������
        }
        else
        {
            Destroy(gameObject); // ���������� ����������� ������
        }
    }

    private void Update()
    {
        frameCount++;
        deltaTime += Time.unscaledDeltaTime;

        if (deltaTime >= 1f) // ��������� FPS ��� � �������
        {
            fps = frameCount / deltaTime;
            frameCount = 0;
            deltaTime = 0f;
        }
    }

    private void OnGUI()
    {
        // ��������� ����� ������
        GUIStyle style = new GUIStyle();
        style.fontSize = 24;
        style.normal.textColor = Color.white;

        // ������� ������ �� ������
        Rect rect = new Rect(10, 10, 200, 50);
        GUI.Label(rect, $"FPS: {fps:0.}", style);
    }
}
