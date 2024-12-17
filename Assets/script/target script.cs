using System.Collections.Generic;
using UnityEngine;

public class TargetSystem : MonoBehaviour
{
    [Header("Daire Ayarlar�")]
    public Transform targetCenter; // Hedefin merkezi
    public float innerCircleRadius = 3f; // K���k dairenin yar��ap�
    public float outerCircleRadius = 6f; // B�y�k dairenin yar��ap�

    [Header("Capsule Ayarlar�")]
    public int capsuleCount = 25; // Kaps�l say�s�
    public float capsuleRadius = 10f; // Kaps�llerin yerle�tirilece�i yar��ap

    [Header("Puan Ayarlar�")]
    public int innerCircleScore = 9; // K���k daire puan�
    public int outerCircleScore = 8; // B�y�k daire puan�
    public int maxCapsuleScore = 10; // Kaps�ller i�in maksimum puan

    [Header("�statistikler")]
    public int totalShots = 0; // Toplam at�� say�s�
    public int successfulHits = 0; // Ba�ar�l� isabet say�s�

    private List<CapsuleCollider> capsules = new List<CapsuleCollider>(); // Kaps�llerin collider listesi

    void Start()
    {
        // Daireleri ve kaps�lleri kur
        SetupCapsules();
    }

    private void SetupCapsules()
    {
        for (int i = 0; i < capsuleCount; i++)
        {
            // Kaps�l� dairesel bir d�zene yerle�tir
            float angle = i * (360f / capsuleCount); // E�it a��larla yerle�tir
            Vector3 position = new Vector3(
                Mathf.Cos(angle * Mathf.Deg2Rad) * capsuleRadius,
                0,
                Mathf.Sin(angle * Mathf.Deg2Rad) * capsuleRadius
            );

            // Kaps�l collider olu�tur ve ekle
            GameObject capsuleObject = new GameObject($"Capsule {i + 1}");
            capsuleObject.transform.SetParent(transform);
            capsuleObject.transform.localPosition = position;
            capsuleObject.transform.LookAt(targetCenter.position);

            CapsuleCollider capsuleCollider = capsuleObject.AddComponent<CapsuleCollider>();
            capsuleCollider.direction = 1; // Y ekseni boyunca hizala
            capsuleCollider.height = 2f;
            capsuleCollider.radius = 0.5f;

            // Kaps�l�n puan�n� ayarla
            int capsuleScore = Mathf.Max(1, maxCapsuleScore - i); // Kaps�l puanlar�n� azaltarak ata
            CapsuleScore capsuleScoreComponent = capsuleObject.AddComponent<CapsuleScore>();
            capsuleScoreComponent.score = capsuleScore;

            capsules.Add(capsuleCollider);
        }

        Debug.Log("Kaps�ller ba�ar�yla olu�turuldu.");
    }

    private void OnCollisionEnter(Collision collision)
    {
        // At�� kontrol�
        totalShots++;

        // �arp��ma noktas�
        Vector3 hitPoint = collision.GetContact(0).point;
        float distanceFromCenter = Vector3.Distance(hitPoint, targetCenter.position);

        // Puan hesaplama
        if (distanceFromCenter <= innerCircleRadius)
        {
            successfulHits++;
            Debug.Log($"�� daire isabet! Puan: {innerCircleScore}");
        }
        else if (distanceFromCenter <= outerCircleRadius)
        {
            successfulHits++;
            Debug.Log($"D�� daire isabet! Puan: {outerCircleScore}");
        }
        else
        {
            foreach (CapsuleCollider capsule in capsules)
            {
                if (capsule.bounds.Contains(hitPoint))
                {
                    successfulHits++;
                    int capsuleScore = capsule.GetComponent<CapsuleScore>().score;
                    Debug.Log($"Kaps�l isabet! Puan: {capsuleScore}");
                    break;
                }
            }
        }

        Debug.Log($"Ba�ar� oran�: {(float)successfulHits / totalShots:P2}");
    }
}

// Kaps�l�n puan�n� tutan yard�mc� s�n�f
public class CapsuleScore : MonoBehaviour
{
    public int score;
}
