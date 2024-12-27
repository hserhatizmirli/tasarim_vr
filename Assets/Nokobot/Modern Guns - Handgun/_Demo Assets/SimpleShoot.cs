using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit; 

[AddComponentMenu("Nokobot/Modern Guns/Simple Shoot")]
public class SimpleShoot : MonoBehaviour
{
    [Header("Prefab Refrences")]
    public GameObject bulletPrefab;
    public GameObject casingPrefab;
    public GameObject muzzleFlashPrefab;

    [Header("Location Refrences")]
    [SerializeField] private Animator gunAnimator;
    [SerializeField] private Transform barrelLocation;
    [SerializeField] private Transform casingExitLocation;
    [SerializeField] private GameObject _bulletHolePrefab; //Bullet hole


    [Header("Settings")]
    [Tooltip("Specify time to destory the casing object")] [SerializeField] private float destroyTimer = 2f;
    [Tooltip("Bullet Speed")] [SerializeField] private float shotPower = 500f;
    [Tooltip("Casing Ejection Speed")] [SerializeField] private float ejectPower = 150f;

    public AudioSource source;
    public AudioClip fireSound;
    public AudioClip reload;
    public AudioClip noAmmo;

    public Magazine magazine;
    public XRBaseInteractor soketInteractor;
    public TMP_Text resultText;

    private bool hasSlide = true;
    private int fireSoundCount = 0; // fireSound'un çalınma sayısı
    public int totalScore = 0; // Skor değişkeni

    public void AddMagazine(XRBaseInteractable interactable)
    {
        magazine=interactable.GetComponent<Magazine>();
        source.PlayOneShot(reload);
        hasSlide = false;
    }

    public void RemoveMagazine(XRBaseInteractable interactable) 
    {
        magazine = null;
        source.PlayOneShot(reload);
    }

    public void Slide()
    {
        hasSlide = true;
        source.PlayOneShot(reload);
    }

    void Start()
    {
        if (barrelLocation == null)
            barrelLocation = transform;

        if (gunAnimator == null)
            gunAnimator = GetComponentInChildren<Animator>();

        soketInteractor.onSelectEntered.AddListener(AddMagazine);
        soketInteractor.onSelectExited.AddListener(RemoveMagazine);
    }

    public void PullTheTrigger ()
    {

        if (!hasSlide)
        {
            // Slider çekilmediği durumda oyuncuya geri bildirim ver
            source.PlayOneShot(noAmmo);
            Debug.Log("Slider çekilmedi. Önce slider'ı çekin!");
            return;
        }


        if (magazine && magazine.numberOfBullet > 0)
        {
            gunAnimator.SetTrigger("Fire");
        }
        else
        {
            source.PlayOneShot(noAmmo);
        }
    }

    void FireWeapon()
    {
        fireSoundCount++; // fireSound sayısını artır

        if (fireSoundCount >= 10)
        {
            // Skoru GlobalScore scriptinden al
            totalScore = GlobalScore.CurrentScore;

            if (totalScore > 80)
            {
                resultText.text = "Kazandınız!";
            }
            else
            {
                resultText.text = "Kaybettiniz!";
            }

            // Oyun işlemlerini durdurmak için
            Application.Quit(); // Oyunu kapatır


            // Oyun işlemlerini durdurmak için herhangi bir ek işlem ekleyebilirsiniz
            return;
        }

        source.PlayOneShot(fireSound); // Silah ateş ettiğinde ses çalınır.
    }


    //This function creates the bullet behavior
    void Shoot()
    {
        //Returns true if we click the mouse button0
        RaycastHit hitInfo; //Contains raycast hit informations
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo))
        {//Returns true if the ray touches something
            GameObject obj = Instantiate(_bulletHolePrefab, hitInfo.point + hitInfo.normal * 0.1f, Quaternion.LookRotation(hitInfo.normal));
            //Instantiating the bullet hole object

            //Changing the bullet hole's position a bit so it will fit better
        }

        GetComponent<WeaponRecoil>().ApplyRecoil();

        magazine.numberOfBullet--;
        source.PlayOneShot(fireSound);

        if (muzzleFlashPrefab)
        {
            //Create the muzzle flash
            GameObject tempFlash;
            tempFlash = Instantiate(muzzleFlashPrefab, barrelLocation.position, barrelLocation.rotation);

            //Destroy the muzzle flash effect
            Destroy(tempFlash, destroyTimer);
        }

        //cancels if there's no bullet prefeb
        if (!bulletPrefab)
        { return; }

        // Create a bullet and add force on it in direction of the barrel
        Instantiate(bulletPrefab, barrelLocation.position, barrelLocation.rotation).GetComponent<Rigidbody>().AddForce(barrelLocation.forward * shotPower);

    }

    //This function creates a casing at the ejection slot
    void CasingRelease()
    {
        //Cancels function if ejection slot hasn't been set or there's no casing
        if (!casingExitLocation || !casingPrefab)
        { return; }

        //Create the casing
        GameObject tempCasing;
        tempCasing = Instantiate(casingPrefab, casingExitLocation.position, casingExitLocation.rotation) as GameObject;
        //Add force on casing to push it out
        tempCasing.GetComponent<Rigidbody>().AddExplosionForce(Random.Range(ejectPower * 0.7f, ejectPower), (casingExitLocation.position - casingExitLocation.right * 0.3f - casingExitLocation.up * 0.6f), 1f);
        //Add torque to make casing spin in random direction
        tempCasing.GetComponent<Rigidbody>().AddTorque(new Vector3(0, Random.Range(100f, 500f), Random.Range(100f, 1000f)), ForceMode.Impulse);

        //Destroy casing after X seconds
        Destroy(tempCasing, destroyTimer);
    }

    private void OnTriggerEnter(Collider other)
    {
      
        // Mermiyi yok et  
        Destroy(gameObject);
    }


}
