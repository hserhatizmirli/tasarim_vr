using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class point9 : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject); // Mermiyi yok et
        DeductPoints(9);  // Art�r�lan puan miktar�n� belirleyebilirsin.
    }
    void DeductPoints(int DamageAmount)
    {
        GlobalScore.CurrentScore += 9;
        Debug.Log("Score Updated: " + GlobalScore.CurrentScore + "Eklenen De�er: " + 9);
    }
}