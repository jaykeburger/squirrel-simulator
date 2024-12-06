using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingBook : BaseWeapon
{
    private Coroutine shootingCoroutine;
    private bool availableForShooting = false;
    public float fireRate;
    public override int Damage => 5;
    public List<GameObject> bulletPrefabs;

    public override void StartShooting()
    {
        if (!gameObject.activeSelf)
        {
            Debug.Log($"Cannot start shooting. {gameObject.name} is inactive.");
            return;
        }
        if (GlobalValues.bookCount > 0)
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
            ShootwithRandomBooks();
            GlobalValues.bookCount -= 1;
            GlobalValues.bookCount = Mathf.Max(0, GlobalValues.bookCount);
            if (GlobalValues.bookCount == 0)
            {
                availableForShooting = false;
            }
            yield return new WaitForSeconds(1f/ fireRate);
        }
    }

    private void ShootwithRandomBooks()
    {
        GameObject randomBook = bulletPrefabs[Random.Range(0, bulletPrefabs.Count)];
        bulletPrefab = randomBook;
        Shoot();
    }
}
