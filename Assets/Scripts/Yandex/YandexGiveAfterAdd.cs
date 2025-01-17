using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YandexGiveAfterAdd : MonoBehaviour
{
    // ID �������
    private const int AdID = 1;

    // ������� �����-�������
    public delegate void VideoEvent();
    public static event VideoEvent OpenVideoEvent;
    public static event VideoEvent CloseVideoEvent;
    public static event VideoEvent ErrorVideoEvent;
    public static event System.Action<int> RewardVideoEvent;
    private BuyTank buyTank;
    void Start()
    {
        // ������������� �� �������
        CloseVideoEvent += OnAdClosed;
        RewardVideoEvent += OnAdRewarded;
        ErrorVideoEvent += OnAdError;
    }

    void OnDestroy()
    {
        // ������������ �� �������
        CloseVideoEvent -= OnAdClosed;
        RewardVideoEvent -= OnAdRewarded;
        ErrorVideoEvent -= OnAdError;
    }

    // ����� �������
    public void ShowAd()
    {
        Debug.Log("���������� �������");
        RewVideoShow(AdID); // ��������� ������� � �������� ID
    }

    // ����� ������ �������
    private void RewVideoShow(int id)
    {
        // ����� ���������� ��������� ������ �������
        Debug.Log($"������� �������� � ID: {id}");
        OpenVideoEvent?.Invoke();
        // �������� ���������� ������� (������� � �������� ����������)
        Invoke(nameof(SimulateAdClose), 3f); // �������� ������� ����� 3 �������
    }

    // ��������� ���������� ������� (������ ��� ������)
    private void SimulateAdClose()
    {
        Debug.Log("������� �������");
        CloseVideoEvent?.Invoke();
        RewardVideoEvent?.Invoke(AdID);
    }

    // ����� ������ �������
    private void OnAdRewarded(int id)
    {
        if (id == AdID)
        {
            buyTank.Give25(); // �������� ����� ��������������
        }
    }

    // ����� ��� �������� �������
    private void OnAdClosed()
    {
        Debug.Log("������� �������, ��������� ���������.");
    }

    // ����� ��������� ������
    private void OnAdError()
    {
        Debug.Log("������ ������ �������!");
    }

    // ��� ����� ��������������
    
}
