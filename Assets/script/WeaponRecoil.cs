using UnityEngine;

public class WeaponRecoil : MonoBehaviour
{
    [Header("Recoil Settings")]
    public float recoilAmount = 5f; // Silah�n yukar� d�nece�i a�� (derece)
    public float recoilSpeed = 10f; // Rotasyon h�z� (d�n�� h�z�)
    public float returnSpeed = 5f; // Geri d�n�� h�z�
    public float returnOffset = 1f; // Geri d�n��te Z eksenindeki sapma miktar�

    [Header("Bullet Settings")]
    public GameObject bulletPrefab; // Mermi prefab'�
    public Transform firePoint; // Merminin ��k�� noktas�
    public float bulletSpread = 2f; // Mermi sapma miktar� (derece)

    private Quaternion originalRotation; // Silah�n ba�lang�� rotasyonu
    private Quaternion targetRotation; // Hedef rotasyon
    private Quaternion returnRotation; // Geri d�n�� rotasyonu
    private bool isRecoiling = false;

    void Start()
    {
        // Silah�n ba�lang�� rotasyonunu kaydet
        originalRotation = transform.localRotation;
        returnRotation = originalRotation; // �lk ba�ta ayn� olsun
    }

    void Update()
    {
        if (isRecoiling)
        {
            // Recoil hareketi s�ras�nda hedef rotasyona yakla�
            transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime * recoilSpeed);

            // E�er hedef rotasyona ula��ld�ysa geri d�n��e ba�la
            if (Quaternion.Angle(transform.localRotation, targetRotation) < 0.1f)
            {
                isRecoiling = false;
            }
        }
        else
        {
            // Recoil hareketi bittiyse hafif offset eklenmi� pozisyona d�n
            transform.localRotation = Quaternion.Lerp(transform.localRotation, returnRotation, Time.deltaTime * returnSpeed);
        }
    }

    public void ApplyRecoil()
    {
        // Yukar� do�ru hedef rotasyonu ayarla
        targetRotation = Quaternion.Euler(originalRotation.eulerAngles.x - recoilAmount, originalRotation.eulerAngles.y, originalRotation.eulerAngles.z);

        // Geri d�n��te Z eksenine hafif bir fark olu�tur
        float zOffset = Random.Range(-returnOffset, returnOffset); // Z ekseninde sapma
        returnRotation = Quaternion.Euler(originalRotation.eulerAngles.x, originalRotation.eulerAngles.y, originalRotation.eulerAngles.z + zOffset);

        isRecoiling = true;
    }

    public void Fire()
    {
        if (bulletPrefab == null || firePoint == null)
        {
            Debug.LogWarning("Bullet prefab veya fire point eksik!");
            return;
        }

        // Rastgele bir sapma a��s� hesapla
        float spreadX = Random.Range(-bulletSpread, bulletSpread);
        float spreadY = Random.Range(-bulletSpread, bulletSpread);

        // Sapma a��s�n� uygula
        Quaternion spreadRotation = Quaternion.Euler(spreadX, spreadY, 0);
        Quaternion finalRotation = firePoint.rotation * spreadRotation;

        // Mermiyi olu�tur ve y�nlendir
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, finalRotation);

        // Mermiye kuvvet uygula (�rne�in Rigidbody ile)
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        if (bulletRb != null)
        {
            bulletRb.AddForce(firePoint.forward * 1000f); // H�z ve kuvvet
        }
    }
}
