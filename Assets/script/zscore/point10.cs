using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class point10 : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject); // Mermiyi yok et
        DeductPoints(10);  // Art�r�lan puan miktar�n� belirleyebilirsin.
    }
    void DeductPoints(int DamageAmount)
    {
        GlobalScore.CurrentScore += 10;
        Debug.Log("Score Updated: " + GlobalScore.CurrentScore + "Eklenen De�er: " + 10);
    }
}