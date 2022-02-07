using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [Header("Gun Settings")]
    public float fireRate = 0.1f;
    public int clipSize = 30;
    public int reservedAmmoCapacity = 270;

    public Camera cam;
    public float range = 100f;
    public float damage = 10f;

    //variables that change troughout code
    bool canShoot;
    int currentAmmoInClip;
    int ammoInReserve;

    // muzzle flash



    //Aiming
    public Vector3 normalLocalPosition;
    public Vector3 aimingLocalPosition;

    public float aimSmoothing = 10;




    //Weapon Recoil
    public bool randomizeRecoil;
    public Vector2 randomRecoilConstraints;
    //you only need to assign this if randomizerecoil is off;
    public Vector2 recoilpattern;


    private void Start()
    {
        currentAmmoInClip = clipSize;
        ammoInReserve = reservedAmmoCapacity;
        canShoot = true;
    }
    private void Update()
    {
        DetermineAim();
        if (Input.GetMouseButton(0) && canShoot && currentAmmoInClip > 0)
        {
            canShoot = false;
            currentAmmoInClip--;
            StartCoroutine(ShootGun());

        }
        else if (Input.GetKeyDown(KeyCode.R) && currentAmmoInClip < clipSize && ammoInReserve > 0)
        {
            int ammountNeeded = clipSize - currentAmmoInClip;
            if (ammountNeeded >= ammoInReserve)
            {
                currentAmmoInClip += ammoInReserve;
                ammoInReserve -= ammountNeeded;
            }
            else
            {
                currentAmmoInClip = clipSize;
                ammoInReserve -= ammountNeeded;
            }
        }
    }
    void DetermineAim()
    {
        Vector3 target = normalLocalPosition;
        if (Input.GetMouseButton(1)) target = aimingLocalPosition;

        Vector3 desiredPosition = Vector3.Lerp(transform.localPosition, target, Time.deltaTime * aimSmoothing);

        transform.localPosition = desiredPosition;
    }

    void DetermineRecoil()
    {
        transform.localPosition -= Vector3.forward * 0.1f;
    }

    IEnumerator ShootGun()
    {
        DetermineRecoil();

        RaycastForEnemy();

        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }

    void RaycastForEnemy()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            TargetEnemy target = hit.transform.GetComponent<TargetEnemy>();
            if (target != null)
            {
                target.GetComponent<Rigidbody>().AddForce(transform.parent.transform.forward * 300);
                target.TakeDamage(damage);
            }

        }
    }
}
