using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEngine;

public class DokController : MonoBehaviour
{
    [SerializeField]
    private Transform gun; // ������, �� �������� ������� ���
    [SerializeField]
    private Transform dokObj; // ������, ������� ����� ������ ������
    [SerializeField]
    private float maxDistance = 10f; // ������������ ����� ����
    public LayerMask enemy; // ���� ������
    [SerializeField]
    private float distancew; // ������� ���������� �� ����
    [SerializeField]
    private Vector2 startDokObj; // ��������� ������� �������

    [SerializeField] private float minDistancew = 1f; // ����������� �������� ����������
    [SerializeField] private float maxDistancew = 10f; // ������������ �������� ����������
    [SerializeField] private float minScale = 0.5f; // ����������� ������ �������
    [SerializeField] private float maxScale = 2f; // ������������ ������ �������
    [SerializeField]
    private bool nePopal = true;
    void Start()
    {
        startDokObj = dokObj.position; // ��������� ��������� ������� �������
    }

    void Update()
    {
        // ���������� ����������� ����
        Vector2 direction = transform.right;

        // ��������� ���
        RaycastHit2D hit = Physics2D.Raycast(gun.position, direction, maxDistance, enemy);
        if (hit.collider != null)
        {
            nePopal= false;
            Vector2 intersectionPoint = hit.point;
            dokObj.position = intersectionPoint; // ������������� ������ � ����� �����������
            distancew = hit.distance; // �������� ���������� �� ����
        }
        else
        {
            nePopal= true;
            // ���� ��� �� �����, ������������� ������ �� ������������ ����������
            dokObj.position = (Vector2)transform.position + direction * maxDistance;
        }

        // ��������� ������ �������
        UpdateScale();

        // ������ ��� ��� �������
        Debug.DrawRay(transform.position, direction * maxDistance, Color.red);
    }

    // ����� ��� ���������� ������� �������
    private void UpdateScale()
    {
        // ������������ �������� distancew � ��������� [minDistancew, maxDistancew]
        float clampedDistance = Mathf.Clamp(distancew, minDistancew, maxDistancew);

        // ����������� ���������� � ��������� [0, 1]
        float normalizedDistance = Mathf.InverseLerp(minDistancew, maxDistancew, clampedDistance);

        // ��������� ����� ������ � ������� �������� ������������
        float newScale = Mathf.Lerp(minScale, maxScale, normalizedDistance);

        // ������������� ������ ������� �� ���� X � Y (Z �������� �������������)
        dokObj.localScale = new Vector3(dokObj.localScale.x, newScale, dokObj.localScale.z);
        if(nePopal==true)
        {
            dokObj.localScale = new Vector3(dokObj.localScale.x, maxScale, dokObj.localScale.z);
        }
    }
}