// using System.Collections;
// using System.Collections.Generic;
// using Unity.VisualScripting;
// using UnityEditor;
// using UnityEngine;
// using UnityEngine.SceneManagement;

// public class Weapon : MonoBehaviour
// {
//     private static Weapon instance;
//     private Transform cameraTransform;
//     public GameObject bulletPrefab;    // Bullet object to spawn when shooting
//     public Transform barrelTransform;  // Barrel position from which bullets fire
//     public Transform bulletParent;
//     private bool isShooting = false;
//     private Coroutine shootingCoroutine;
//     [SerializeField]
//     private float fireRate;            // Fire rate (seconds between shots)
//     [SerializeField]
//     public int damage;
//     [SerializeField]
//     private float bulletHitMissDistance = 25.0f;

//     private void Start()
//     {
//         // UpdateCameraTransform();
//         // SceneManager.sceneLoaded += OnScreenLoaded; // Register to screen load event to handle camera updates 
//         // cameraTransform = Camera.main.transform;
//         if (instance != null && instance != this)
//         {
//             Destroy(gameObject); // Destroy duplicate instance
//             return;
//         }
//         instance = this; // Set the singleton instance
        
//         cameraTransform = Camera.main?.transform;

//         if (cameraTransform == null)
//         {
//             Debug.LogError("Camera is still null. Ensure there's a MainCamera tagged in the scene.");
//         }

//         if (transform.parent != null)
//         {
//             transform.SetParent(null); // Detach from parent to make this a root object
//         }

//         // Optionally make sure the Weapon persists across scenes
//         DontDestroyOnLoad(gameObject);
//     }

//     private void OnDestroy()
//     {
//         SceneManager.sceneLoaded -= OnScreenLoaded;
//     }

//     private void UpdateCameraTransform()
//     {
//         cameraTransform = Camera.main?.transform;
//         if (cameraTransform == null)
//         {
//             Debug.Log("Camera is null");
//         }
//     }

//     // Event handler for when a scene is loaded
//     private void OnScreenLoaded(Scene scene, LoadSceneMode mode)
//     {
//         UpdateCameraTransform();
//     }

//     // Start shooting
//     public void StartShooting()
//     {
//         if (bulletPrefab == null) {
//             Debug.LogError("prefab is null.");
//         }
//         if (cameraTransform == null)
//         {
//             cameraTransform = Camera.main?.transform;
//             if (cameraTransform == null)
//             {
//                 Debug.LogError("Camera is still null. Please ensure there is a MainCamera tagged properly in the scene.");
//                 return;  // Exit early if there's no camera
//             }
//         }
//         if (!PauseScript.GameIsPause && SceneManager.GetActiveScene().name != "JimmyDorm")
//         {
//             // shootingCoroutine = StartCoroutine(ShootRoutine());
//             if (shootingCoroutine == null) // Check if it's not already running
//             {
//                 shootingCoroutine = StartCoroutine(ShootRoutine());
//                 Debug.Log("Shooting coroutine started.");
//             }
//             else
//             {
//                 Debug.LogWarning("Shooting coroutine is already running.");
//             }
//         }
//     }

//     // Stop shooting
//     public void StopShooting()
//     {
//         if (isShooting)
//         {
//             // StopCoroutine(shootingCoroutine);
//             // isShooting = false;
//             if (shootingCoroutine != null)
//             {
//                 StopCoroutine(shootingCoroutine);
//                 shootingCoroutine = null; // Reset it after stopping
//                 isShooting = false;
//                 Debug.Log("Shooting coroutine stopped.");
//             }
//             else
//             {
//                 Debug.LogWarning("No shooting coroutine to stop.");
//             }
//         }
//     }

//     // Shooting routine for automatic weapons
//     private IEnumerator ShootRoutine()
//     {
//         isShooting = true;
//         while (isShooting)
//         {
//             Shoot();
//             yield return new WaitForSeconds(1f / fireRate); // Fire bullets according to fireRate
//         }
//     }

//     // Fire a bullet
//     private void Shoot()
//     {
//         RaycastHit hit;
//         GameObject bullet = GameObject.Instantiate(bulletPrefab, barrelTransform.position, Quaternion.identity, bulletParent);
//         BulletController bulletController = bullet.GetComponent<BulletController>();
//         if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, Mathf.Infinity))
//         {
//             if (hit.collider.CompareTag("Enemy"))
//             {
//                 Transform enemyTransform = hit.collider.transform.parent;
//                 // Debug.Log($"Enemy Transform: {enemyTransform.name}");

//                 // Get the EnemyHealthBar component from the parent
//                 EnemyHealthBar enemyHealthBar = enemyTransform.GetComponent<EnemyHealthBar>();
//                 if (enemyHealthBar != null)
//                 {
//                     enemyHealthBar.takeDamge(); // Call the takeDamage function
//                 }
//                 else
//                 {
//                     Debug.LogError("EnemyHealthBar component not found on enemy-rat!");
//                 }
//             }
//             bulletController.target = hit.point;
//             bulletController.hit = true;
//         }
//         else
//         {
//             bulletController.target = cameraTransform.position + cameraTransform.forward * bulletHitMissDistance;
//             bulletController.hit = false;
//         }
//         bulletController.SetDamage(damage); // Set bullet damage
//         // Debug.Log($"{weaponName} fired! Damage: {damage}");
//     }
// }
