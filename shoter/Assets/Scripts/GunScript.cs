using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    //Gun Stats
    public int damage;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;
    int bulletsleft, bulletsShot;

    //bools
    bool shooting, readyToShoot, reloading;

    //Reference
    public Camera fpsCam;
    public Transform attackPoint;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;

    //Aiming
    public Vector3 normalLocalPosition;
    public Vector3 aimingLocalPosition;
    public float aimSmoothing = 10;
    public float recoil;
    public GameObject bulletindicatorfornow;

    private void Start()
    {
        bulletsleft = magazineSize;
        readyToShoot = true;
    }

    private void Update()
    {
        MyInput();
        DetermineAim();
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
            DetermineRecoil();
        }
    }


    private void Shoot()
    {
        readyToShoot = false;

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
                rayHit.collider.GetComponent<TargetEnemy>().TakeDamage(damage);
            }
        }

        //Graphics
        Instantiate(bulletindicatorfornow, rayHit.point, Quaternion.Euler(0, 180, 0));

        bulletsleft--;
        bulletsShot--;

        Invoke("ResetShot", timeBetweenShooting);

        if (bulletsShot > 0 && bulletsleft > 0)
        {
            Invoke("Shoot", timeBetweenShots);
        }
    }

    private void ResetShot()
    {
        readyToShoot = true;
    }

    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }

    private void ReloadFinished()
    {
        bulletsleft = magazineSize;
        reloading = false;
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
        transform.localPosition -= Vector3.forward * recoil;
    }
}
