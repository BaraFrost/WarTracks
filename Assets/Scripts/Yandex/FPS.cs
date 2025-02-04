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
        // Реализация синглтона
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Объект сохраняется между сценами
        }
        else
        {
            Destroy(gameObject); // Уничтожить дублирующий объект
        }
    }

    private void Update()
    {
        frameCount++;
        deltaTime += Time.unscaledDeltaTime;

        if (deltaTime >= 1f) // Обновляем FPS раз в секунду
        {
            fps = frameCount / deltaTime;
            frameCount = 0;
            deltaTime = 0f;
        }
    }

    private void OnGUI()
    {
        // Настройки стиля текста
        GUIStyle style = new GUIStyle();
        style.fontSize = 24;
        style.normal.textColor = Color.white;

        // Позиция текста на экране
        Rect rect = new Rect(10, 10, 200, 50);
        GUI.Label(rect, $"FPS: {fps:0.}", style);
    }
}
