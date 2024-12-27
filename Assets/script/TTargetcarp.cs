using UnityEngine;
using TMPro; // TextMeshPro kullan�m� i�in gerekli

public class TargetScoring : MonoBehaviour
{
    public int totalScore = 0; // Toplam puan
    public TextMeshProUGUI scoreText; // Skoru g�stermek i�in kullan�lan UI bile�eni
    private bool specificZoneHit = false; // Belirli bir b�lgeye isabet edildi�ini izler

    void Start()
    {
        Debug.Log("Game Started. Initial Score: " + totalScore);
        UpdateScoreText(); // Ba�lang��ta skor metnini g�ncelle
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision detected with: " + other.name);

        // �arp�lan nesnenin TargetZone bile�enini al
        TargetZone zone = other.GetComponent<TargetZone>();
        if (zone != null)
        {
            Debug.Log("Target zone points: " + zone.points);

            // E�er �arp�lan b�lge 7 puanl�ysa ve ba�ka bir spesifik b�lgeye �arp�lmad�ysa
            if (zone.points == 7 && !specificZoneHit)
            {
                totalScore += zone.points;
                Debug.Log("Hit 7-point zone. Total Score: " + totalScore);
            }
            else if (zone.points != 7)
            {
                specificZoneHit = true; // Spesifik bir b�lgeye �arp�ld�
                totalScore += zone.points;
                Debug.Log("Hit specific zone worth " + zone.points + " points. Total Score: " + totalScore);
            }

            UpdateScoreText(); // Skor metnini g�ncelle
        }
        else
        {
            Debug.LogWarning("The object hit does not have a TargetZone component.");
        }
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("Exited collision with: " + other.name);

        // �arp��ma sona erdi�inde bayra�� s�f�rla
        TargetZone zone = other.GetComponent<TargetZone>();
        if (zone != null && zone.points != 7)
        {
            specificZoneHit = false;
            Debug.Log("specificZoneHit flag reset.");
        }
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + totalScore;
            Debug.Log("Score updated to: " + totalScore);
        }
        else
        {
            Debug.LogError("ScoreText is not assigned in the inspector.");
        }
    }
}
