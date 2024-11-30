using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
public class WeaponManager : MonoBehaviour
{
    public BaseWeapon[] weapons; //Arrays of weapon
    public int currentWeapon = 0; // 0-SlingShot, 1-AcornGunMachine
    public static WeaponManager Instance { get; private set; }
    public Image weaponIcon; // UI Image to show current weapon
    public Sprite[] weaponIcons; // Array of sprites for weapon icons
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
        UpdateWeaponIcon();
    }
     private void UpdateWeaponIcon()
    {
        if (weaponIcon != null && weaponIcons.Length > currentWeapon)
        {
            weaponIcon.sprite = weaponIcons[currentWeapon]; // Change the icon
        }
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
