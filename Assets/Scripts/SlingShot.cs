using System.Collections;
using UnityEditor;
using UnityEngine;

public class SlingShot : BaseWeapon
{
    private Coroutine shootingCoroutine;
    private bool availableForShooting = false;
    public float fireRate;

    public override int Damage => 20;

    public override void StartShooting()
    {
        if (!gameObject.activeSelf)
        {
            Debug.Log($"Cannot start shooting. {gameObject.name} is inactive.");
            return;
        }
        if (GlobalValues.rockCount > 0)
        {
            availableForShooting = true;
            shootingCoroutine = StartCoroutine(ShootRoutine());
        } 
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
        while (true && availableForShooting)
        {
            Shoot();
            GlobalValues.rockCount -= 1;
            GlobalValues.rockCount = Mathf.Max(0, GlobalValues.rockCount);
            if (GlobalValues.rockCount == 0)
            {
                availableForShooting = false;
            }
            yield return new WaitForSeconds(1f/ fireRate);
        }
    }
}
