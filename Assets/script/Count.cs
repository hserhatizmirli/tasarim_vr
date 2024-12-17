using UnityEngine;
using TMPro; // TextMeshPro k�t�phanesi

public class CounterScript : MonoBehaviour
{
    public TMP_Text counterText; // Saya� metni
    public TMP_Text congratsText; // "Tebrikler!" metni
    private int counter = 0; // Sayac� ba�lat
    private int targetCount = 12; // Hedef say�s�

    void Start()
    {
        UpdateCounter(); // Sayac� g�ncelle
        congratsText.gameObject.SetActive(false); // Tebrikler Text'ini gizle
    }

    public void IncrementCounter()
    {
        counter++; // Saya� art�r
        UpdateCounter(); // Saya� metnini g�ncelle

        if (counter >= targetCount) // Hedefe ula��ld� m�?
        {
            ShowCongratsMessage(); // Tebrikler mesaj�n� g�ster
        }
    }

    void UpdateCounter()
    {
        counterText.text = "Hedef: " + counter + "/" + targetCount; // Saya� metnini ayarla
    }

    void ShowCongratsMessage()
    {
        congratsText.gameObject.SetActive(true); // Tebrikler Text'ini g�r�n�r yap
        counterText.gameObject.SetActive(false); // Saya� Text'ini gizle
    }
}
