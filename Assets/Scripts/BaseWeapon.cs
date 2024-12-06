using UnityEditor;
using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform barrelTransform;
    public Transform bulletParent;
    public abstract int Damage { get; }
    public float bulletHitMissDistance;
    protected Transform cameraTransform;

    protected virtual void Start()
    {
        cameraTransform = Camera.main.transform;
    }

    public abstract void StartShooting();
    public abstract void StopShooting();

    protected void Shoot()
    {
        Debug.Log("Shooting");
        RaycastHit hit;
        GameObject bullet = GameObject.Instantiate(bulletPrefab, barrelTransform.position, Quaternion.identity);
        if (bulletParent != null && bulletParent.gameObject.activeSelf)
        {
            bullet.transform.SetParent(bulletParent);
        }
        else
        {
            Debug.Log("Bullet parent is null");
        }
        bullet.SetActive(true);
        Rigidbody rb = bullet.AddComponent<Rigidbody>();
        rb.useGravity = false;
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
        bulletController.SetDamage(Damage); // Set bullet damage
        // Debug.Log($"{weaponName} fired! Damage: {damage}");
    }
}
