using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Net.NetworkInformation;
using UnityEditor.PackageManager;
using UnityEngine;

public static class GlobalValues
{
    // Values for player's health
    public static float currentHealth = 100f;
    public static float maxHealth = 100f;

    // Values for player's stamina
    public static float currentStamina = 100f;
    public static float maxStamina = 100f;

    // Values for bullet count
    public static int acornCount = 15;
    public static int rockCount = 5;
    public static int bookCount = 0;

    // Values for inventory
    public static Dictionary<int, string> GlobalInventory = new();

    //Bool for wobble effect
    public static bool wobbleEffectActive = false;

    //Bool values for trash bin generate system
    public static bool binIsChose = false;
    public static int binIsChoseID = -1;
}
