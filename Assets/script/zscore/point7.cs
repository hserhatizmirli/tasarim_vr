using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class point7 : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject); // Mermiyi yok et
        GlobalScore.CurrentScore += 7;
        DeductPoints(7);  // Art�r�lan puan miktar�n� belirleyebilirsin
    }
    void DeductPoints(int DamageAmount)
    {

        Debug.Log("Score Updated: " + GlobalScore.CurrentScore + "Eklenen De�er: " + 7);
    }
}