using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Jobs;
using UnityEngine.SceneManagement;

public class FinalBossScript : MonoBehaviour
{
    public RedCircleDrawer redCircleDrawer;
    public RatsAttackSystem ratAttack;
    public EnemyShoot enemyShoot;
    public static bool isBattleActive = false;
    public int attackChoice;
    public static bool activeRats = false;
    public GameObject losePanel;
    public static bool isDead = false;
    // Start is called before the first frame update
    void Awake()
    {
        if (losePanel != null) losePanel.SetActive(false);
    }
    void Start()
    {
        StartCoroutine(WaitForCondition());
    }

    IEnumerator WaitForCondition()
    {
        while (!isBattleActive)
        {
            yield return null; // Wait until next frame
        }

        Debug.Log("From boss fight: " + isBattleActive);
        StartCoroutine(FinalBattles());
    }

    // Update is called once per frame
    void Update()
    {
        if (GlobalValues.currentHealth <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            GlobalValues.currentHealth = GlobalValues.maxHealth;
            isBattleActive = false;
        }
    }

    IEnumerator FinalBattles()
    {
        while (isBattleActive)
        {
            if (GlobalValues.currentHealth <= 0 || isDead)
            {
                Debug.Log("is trigger");
                Time.timeScale = 0f;
                if (GlobalValues.currentHealth <= 0)
                {
                    losePanel.SetActive(true);
                    yield return new WaitForSeconds(0.5f);

                }
                else
                {
                    Cursor.lockState = CursorLockMode.None;
                    SceneManager.LoadScene("Comic-4");
                }
                yield break;
            }

            attackChoice = Random.Range(0,3);
            if (attackChoice == 0)
            {
                Debug.Log(attackChoice);
                redCircleDrawer.InitializeCircles();
            }
            else if (attackChoice == 1)
            {
                Debug.Log(attackChoice);
                enemyShoot.StartShootingPlayer();
            }
            else
            {
                Debug.Log(attackChoice);
                activeRats = true;
                RatsAttackSystem.first = true;
            }
            yield return new WaitForSeconds(10f);
            // activeRats = false;
        }
    }
}
