using UnityEditor;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public BaseWeapon[] weapons; //Arrays of weapon
    public int currentWeapon = 0;
    public static WeaponManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else 
        {
            Instance = this;

        }
    }

    public void SwitchWeapon(int weaponIdx)
    {
        currentWeapon = weaponIdx;
    }

    public void StartShooting()
    {
        if (weapons[currentWeapon] != null)
        {
            weapons[currentWeapon].StartShooting();
        }
        else
        {
            Debug.Log("weapon is null");
        }
    }

    public void StopShooting()
    {
        if (weapons[currentWeapon] != null)
        {
            weapons[currentWeapon].StopShooting();
        }
    }
}
