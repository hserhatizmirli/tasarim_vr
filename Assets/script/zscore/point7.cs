using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class point7 : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject); // Mermiyi yok et
        DeductPoints(7);  // Art�r�lan puan miktar�n� belirleyebilirsin
    }
    void DeductPoints(int DamageAmount)
    {
        GlobalScore.CurrentScore += 7;
        Debug.Log("Score Updated: " + GlobalScore.CurrentScore + "Eklenen De�er: " + 7);
    }
}