using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class MetricsManager : MonoBehaviour
{
    public static void SendEvent(string eventName)
    {
        YG2.MetricaSend(eventName);
    }

    public static void SendEvent(string eventName, Dictionary<string, string> eventData)
    {
        YG2.MetricaSend(eventName, eventData);
    }

    public static void SendEvent(string eventName, Dictionary<string, object> eventData)
    {
        YG2.MetricaSend(eventName, eventData);
    }

    public static void SendEvent(string eventName, string param1, string param2)
    {
        YG2.MetricaSend(eventName, param1, param2);
    }

    public static void SendEvent(string eventName, string nestedParam, Dictionary<string, object> subNestedData)
    {
        YG2.MetricaSend(eventName, nestedParam, subNestedData);
    }
}
