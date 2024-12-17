using UnityEngine;

public class LeftButtonHandler : MonoBehaviour
{
    public GameObject objectToMove; // Hareket ettirilecek nesne (Poligono1)
    public float initialXPosition = -27.217f; // Soldaki butonlar i�in ba�lang�� X koordinat�

    void Start()
    {
        // Nesnenin ba�lang�� pozisyonunu ayarlama
        if (objectToMove != null)
        {
            objectToMove.transform.position = new Vector3(initialXPosition, objectToMove.transform.position.y, objectToMove.transform.position.z);
        }
    }

    public void OnLeftButtonClick(int xOffset)
    {
        MoveObject(xOffset);  // Nesneyi hareket ettir
    }

    void MoveObject(int xOffset)
    {
        if (objectToMove != null)
        {
            // Nesnenin mevcut pozisyonunu al�yoruz
            Vector3 newPosition = objectToMove.transform.position;
            newPosition.x = initialXPosition + xOffset; // X koordinat�na offset ekle
            objectToMove.transform.position = newPosition; // Yeni pozisyona ta��
            Debug.Log("Nesne yeni pozisyona ta��nd�: " + newPosition);
        }
        else
        {
            Debug.LogWarning("Ta��nacak nesne atanmad�!");
        }
    }
}
