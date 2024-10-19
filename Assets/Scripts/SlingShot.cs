using System.Collections;
using UnityEditor;
using UnityEngine;

public class SlingShot : BaseWeapon
{
    private Coroutine shootingCoroutine;
    public float fireRate;

    public override int Damage => 20;

    public override void StartShooting()
    {
        if (!gameObject.activeSelf)
        {
            Debug.Log($"Cannot start shooting. {gameObject.name} is inactive.");
            return;
        }
        shootingCoroutine = StartCoroutine(ShootRoutine()); 
    }

    public override void StopShooting()
    {
        if (shootingCoroutine != null)
        {
            StopCoroutine(shootingCoroutine);
            shootingCoroutine = null;
        }
    }

    private IEnumerator ShootRoutine()
    {
        while (true)
        {
            Shoot();
            yield return new WaitForSeconds(1f/ fireRate);
        }
    }
}
