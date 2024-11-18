using System.Collections;
using UnityEngine;

public class AcornGunMachine : BaseWeapon
{
    private Coroutine shootingCoroutine;
    private bool availableForShooting = false;
    public float fireRate;

    public override int Damage => 10;

    public override void StartShooting()
    {
        if (!gameObject.activeSelf)
        {
            Debug.Log($"Cannot start shooting. {gameObject.name} is inactive.");
            return;
        }
        if (GlobalValues.acornCount > 0)
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
            GlobalValues.acornCount -= 1;
            GlobalValues.acornCount = Mathf.Max(0, GlobalValues.acornCount);
            if (GlobalValues.acornCount == 0)
            {
                availableForShooting = false;
            }
            yield return new WaitForSeconds(1f/ fireRate);
        }

    }
}
