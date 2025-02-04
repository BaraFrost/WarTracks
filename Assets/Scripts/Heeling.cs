using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heeling : MonoBehaviour
{
    [SerializeField]
    private float heelCount; // ���������� �������
   
    [SerializeField]
    private Rigidbody2D rb; // Rigidbody �������

    private bool isStopped = false; // ���� ��������� �������

    [SerializeField]
    private AudioClip shootSound;
    [SerializeField]
    private AudioSource audioSource;
    private void Start()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>(); // ������������� ������� Rigidbody, ���� �� �� �����
        }
        audioSource = GetComponent<AudioSource>();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<EntityHealth>(out var health))
        {
            health.value += heelCount;

            // ������ ��������� ������ ��� �����
            GameObject tempSoundObject = new GameObject("TempAudio");
            AudioSource tempAudioSource = tempSoundObject.AddComponent<AudioSource>();

            // ����������� ����
            tempAudioSource.clip = shootSound;
            tempAudioSource.volume = 0.05f; // ���������� ������ ��������� (��������, 50%)
            tempAudioSource.pitch = 1.0f;  // ���������� ������ ���
            tempAudioSource.spatialBlend = 0; // ��� 2D-����� (���� ���������)
            tempAudioSource.Play();

            // ���������� ��������� ������ ����� ���������� ��������������� �����
            Destroy(tempSoundObject, shootSound.length);

            // ���������� �������� ������
            Destroy(gameObject);
        }
        else if ((collision.gameObject.CompareTag("Wall"))|| (collision.gameObject.CompareTag("Player")))
        {
            // ��������� ������� ��� ������������ �� ������
            StopMovement();
        }
    }

    private void StopMovement()
    {
        if (!isStopped)
        {
            isStopped = true;

            // ������������� ���������� ��������
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;

            // ��������� ������� ������
            rb.bodyType = RigidbodyType2D.Kinematic;
        }
    }
}