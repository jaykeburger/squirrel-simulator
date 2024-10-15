using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private Transform cameraTransform;
    public string weaponName;
    public GameObject bulletPrefab;    // Bullet object to spawn when shooting
    public Transform barrelTransform;  // Barrel position from which bullets fire
    private Transform bulletParent;
    private bool isShooting = false;
    private Coroutine shootingCoroutine;
    [SerializeField]
    private float fireRate;            // Fire rate (seconds between shots)
    [SerializeField]
    public int damage;
    [SerializeField]
    private float bulletHitMissDistance = 25.0f;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
    }

    // Start shooting
    public void StartShooting()
    {
        if (!PauseScript.GameIsPause)
        {
            shootingCoroutine = StartCoroutine(ShootRoutine());
        }
    }

    // Stop shooting
    public void StopShooting()
    {
        if (isShooting)
        {
            StopCoroutine(shootingCoroutine);
            isShooting = false;
        }
    }

    // Shooting routine for automatic weapons
    private IEnumerator ShootRoutine()
    {
        isShooting = true;
        while (isShooting)
        {
            Shoot();
            yield return new WaitForSeconds(1f / fireRate); // Fire bullets according to fireRate
        }
    }

    // Fire a bullet
    private void Shoot()
    {
        RaycastHit hit;
        GameObject bullet = GameObject.Instantiate(bulletPrefab, barrelTransform.position, Quaternion.identity, bulletParent);
        BulletController bulletController = bullet.GetComponent<BulletController>();
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, Mathf.Infinity))
        {
            bulletController.target = hit.point;
            bulletController.hit = true;
        }
        else
        {
            bulletController.target = cameraTransform.position + cameraTransform.forward * bulletHitMissDistance;
            bulletController.hit = false;
        }
        bulletController.SetDamage(damage); // Set bullet damage
        Debug.Log($"{weaponName} fired! Damage: {damage}");
    }
}
