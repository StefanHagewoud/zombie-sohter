using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.Audio;

public class GunScript : MonoBehaviour
{
    PhotonView pv;
    Animator anim;

    [Header("Gun Stats")]
    public int damage;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;
    public int bulletsleft;
    int bulletsShot;
    public int ammoTotal;
    int shotsFired;

    public TMP_Text ammoText;

    [Header("Bools")]
    bool shooting, readyToShoot, reloading;
    public bool pistol, AR, shotgun;

    [Header("Reference")]
    public Camera fpsCam;
    public Transform attackPoint;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;

    [Header("Aiming")]
    public Vector3 normalLocalPosition;
    public Vector3 aimingLocalPosition;
    public float aimSmoothing = 10;
    public float recoil;
    public GameObject bulletindicatorfornow;
    public float aimFieldOfView;

    [Header("Audio")]
    AudioSource audio;
    public AudioClip pistolShot;
    public AudioClip ARShot;
    public AudioClip shotgunShot;
    private void Awake()
    {
        pv = GetComponent<PhotonView>();
    }

    private void Start()
    {
        bulletsleft = magazineSize;
        readyToShoot = true;
        fpsCam = Camera.main;
        audio = GetComponent<AudioSource>();
        ammoText = GameObject.Find("AmmoCounter").GetComponent<TMP_Text>();
        ammoText.text = $"{bulletsleft}/{ammoTotal}";
    }

    private void Update()
    {
        MyInput();
        DetermineAim();
        //ammoText.text = $"{bulletsleft}/{ammoTotal}";
    }

    private void MyInput()
    {
        if (allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        if (Input.GetKeyDown(KeyCode.R) && bulletsleft < magazineSize && !reloading) Reload();

        //Shoot
        if (readyToShoot && shooting && !reloading && bulletsleft > 0)
        {
            bulletsShot = bulletsPerTap;
            Shoot();
            readyToShoot = false;

            PlayAudio();
            DetermineRecoil();
        }
    }


    private void Shoot()
    {
        GameObject.Find("Character").GetComponent<Animator>().SetBool("Shoot", true);
        //Spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);
        float z = Random.Range(-spread, spread);

        //calculate Direction with Spread
        Vector3 direction = fpsCam.transform.forward + new Vector3(x, y, z);

        //Raycast
        if (Physics.Raycast(fpsCam.transform.position, direction, out rayHit, range))
        {
            Debug.Log(rayHit.collider.name);

            if (rayHit.collider.CompareTag("Enemy"))
            {
                rayHit.collider.GetComponent<Health>().GetHit(damage);
            }
        }

        //Graphics
        Destroy(Instantiate(bulletindicatorfornow, rayHit.point, Quaternion.Euler(0, 180, 0)), 2);

        bulletsleft--;
        bulletsShot--;
        shotsFired++;
        ammoText.text = $"{bulletsleft}/{ammoTotal}";


        Invoke("ResetShot", timeBetweenShooting);

        if (bulletsShot > 0 && bulletsleft > 0)
        {
            Invoke("Shoot", timeBetweenShots);
        }
    }

    private void ResetShot()
    {
        readyToShoot = true;
        GameObject.Find("Character").GetComponent<Animator>().SetBool("Shoot", false);

    }

    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
        ammoText.text = $"{bulletsleft}/{ammoTotal}";
        GameObject.Find("Character").GetComponent<Animator>().SetBool("Reload", true);

    }

    private void ReloadFinished()
    {
        if (ammoTotal <= 0 && bulletsleft <= 0)
        {
            magazineSize = 0;
            ammoTotal = 0;
            shotsFired = 0;
        }

        ammoTotal -= shotsFired;
        bulletsleft = magazineSize;
        reloading = false;
        shotsFired -= shotsFired;
        ammoText.text = $"{bulletsleft}/{ammoTotal}";
        GameObject.Find("Character").GetComponent<Animator>().SetBool("Reload", false);
    }

    void DetermineAim()
    {
        Vector3 target = normalLocalPosition;
        if (Input.GetMouseButton(1))
        {
            target = aimingLocalPosition;
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, aimFieldOfView, aimSmoothing * Time.deltaTime);
        }
        else
        {
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 60, aimSmoothing * Time.deltaTime);
        }

        Vector3 desiredPosition = Vector3.Lerp(transform.localPosition, target, Time.deltaTime * aimSmoothing);

        transform.localPosition = desiredPosition;
        
    }

    void DetermineRecoil()
    {
        transform.localPosition -= Vector3.forward * recoil;
    }

    public void PlayAudio()
    {
        if (pistol)
            audio.PlayOneShot(pistolShot);
        if (shotgun)
            audio.PlayOneShot(shotgunShot);
        if (AR)
            audio.PlayOneShot(ARShot);
    }
}
