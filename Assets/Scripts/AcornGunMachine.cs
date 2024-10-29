using System.Collections;
using UnityEngine;

public class AcornGunMachine : BaseWeapon
{
    private Coroutine shootingCoroutine;
    public float fireRate;

    public override int Damage => 10;

    public override void StartShooting()
    {
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
