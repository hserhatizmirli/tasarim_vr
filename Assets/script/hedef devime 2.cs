using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TargetXRInteraction : MonoBehaviour
{
    public Transform targetObject; // Hedef nesne
    public Vector3 targetPosition = new Vector3(0, 0, 0); // Yeni konum
    public Vector3 targetRotation = new Vector3(0, 0, 0); // Yeni rotasyon (Euler a��lar�)
    private bool isClicked = false; // Hedefin daha �nce t�klan�p t�klanmad���n� kontrol eder

    private XRGrabInteractable grabInteractable; // XR Grab Interactable bile�eni

    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>(); // XR Grab Interactable bile�enini al

        if (grabInteractable != null)
        {
            // XR Interactable i�in Select Enter event'ini ba�la
            grabInteractable.onSelectEntered.AddListener(OnSelectEntered);
        }
        else
        {
            Debug.LogError("XRGrabInteractable bile�eni eksik!");
        }
    }

    void OnSelectEntered(XRBaseInteractor interactor)
    {
        // E�er hedef nesneye t�klanm��sa ve daha �nce ta��nmam��sa
        if (!isClicked)
        {
            MoveTarget(); // Hedefi ta��
            isClicked = true; // Hedef ta��nd�, tekrar ta��namaz
        }
    }

    void MoveTarget()
    {
        // Konum de�i�ikli�i
        if (targetObject != null)
        {
            targetObject.position = targetPosition; // Hedefin yeni konumunu belirler

            // Rotasyon de�i�ikli�i
            targetObject.rotation = Quaternion.Euler(targetRotation); // Hedefin yeni rotas�n� belirler

            Debug.Log("Hedef ta��nd�!"); // Debug log mesaj�
        }
        else
        {
            Debug.LogError("Hedef nesne atanmad�!"); // E�er hedef nesne atanmad�ysa hata mesaj�
        }
    }
}
