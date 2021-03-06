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
    public ParticleSystem muzzleFlash;

    public TMP_Text ammoText;

    [Header("Bools")]
    bool shooting, readyToShoot, reloading;
    public bool holdingWeapon;
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
    public GameObject hitEffect;
    public float aimFieldOfView;

    [Header("Audio")]
    AudioSource audioS;
    public AudioClip pistolShot;
    public AudioClip ARShot;
    public AudioClip shotgunShot;
    public AudioClip pistolReload;
    public AudioClip shotgunReload;
    public AudioClip ARReload;
    private void Awake()
    {
        pv = GetComponent<PhotonView>();
    }

    public void Start()
    {
        bulletsleft = magazineSize;
        readyToShoot = true;
        audioS = GetComponent<AudioSource>();
        ammoText = GameObject.Find("AmmoCounter").GetComponent<TMP_Text>();
        ammoText.text = $"{bulletsleft}/{ammoTotal}";

        if (pistol)
            gameObject.GetComponentInParent<PlayerMovement>().ChangeWeaponPrefab(1);
        if (AR)
            gameObject.GetComponentInParent<PlayerMovement>().ChangeWeaponPrefab(2);
        if (shotgun)
            gameObject.GetComponentInParent<PlayerMovement>().ChangeWeaponPrefab(3);

        if (!pistol && !shotgun && !AR)
        {
            gameObject.GetComponentInParent<PlayerMovement>().currenWeapon = 0;
        }
    }

    private void Update()
    {
        MyInput();
        DetermineAim();
        ammoText.text = $"{bulletsleft}/{ammoTotal}";
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
        muzzleFlash.Play();
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

            if (rayHit.collider.CompareTag("Player"))
            {
                rayHit.collider.GetComponent<Health>().GetHit(damage);
            }
        }

        //Graphics
        Destroy(Instantiate(hitEffect, rayHit.point, Quaternion.LookRotation(rayHit.normal)), 1);
        //Instantiate(impactArrow, hit.point, Quaternion.LookRotation(hit.normal));

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
        PlayReloadAudio();


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

        if (ammoTotal <= 0)
        {
            ammoTotal = 0;
            shotsFired = 0;
            magazineSize = 0;
        }

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
            audioS.PlayOneShot(pistolShot);
        if (shotgun)
            audioS.PlayOneShot(shotgunShot);
        if (AR)
            audioS.PlayOneShot(ARShot);
    }
    public void PlayReloadAudio()
    {
        if (pistol)
            audioS.PlayOneShot(pistolReload);
        if (shotgun)
            audioS.PlayOneShot(shotgunReload);
        if (AR)
            audioS.PlayOneShot(ARReload);
    }
}
