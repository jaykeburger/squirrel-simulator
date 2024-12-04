using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Jobs;

public class FinalBossScript : MonoBehaviour
{
    public RedCircleDrawer redCircleDrawer;
    public RatsAttackSystem ratAttack;
    public EnemyShoot enemyShoot;
    // public EnemyShoot enemyShoot;
    public bool isBattleActive = true;
    public float playerHealth = 100;
    public float enemyHealth = 100;
    public int attackChoice;
    public static bool activeRats = false;
    public GameObject winPanel;
    public GameObject losePanel;
    // Start is called before the first frame update
    void Awake()
    {
        if (winPanel != null) winPanel.SetActive(false);
        if (losePanel != null) losePanel.SetActive(false);
    }
    void Start()
    {
        StartCoroutine(FinalBattles());
    }

    IEnumerator FinalBattles()
    {
        while (isBattleActive)
        {
            if (GlobalValues.currentHealth <= 0 || TakeDamage.health <= 0)
            {
                string message;
                if (GlobalValues.currentHealth <= 0)
                {
                    message = "Lose";
                }
                else
                {
                    message = "Win";
                }
                endBattle(message);
                yield break;
            }

            attackChoice = Random.Range(0,3);
            if (attackChoice == 0)
            {
                redCircleDrawer.InitializeCircles();
            }
            else if (attackChoice == 1)
            {
                enemyShoot.StartShootingPlayer();
            }
            else
            {
                activeRats = true;
                int idx = Random.Range(0,2);
                if (idx == 0)
                {
                    RatsAttackSystem.first = false;
                }
                else
                {
                    RatsAttackSystem.first = true;
                }
            }
            yield return new WaitForSeconds(10f);
            activeRats = false;
        }
    }

    public void endBattle(string message)
    {
        if (message == "Win")
        {
            winPanel.SetActive(true);
        }
        if (message == "Lose")
        {
            losePanel.SetActive(true);
        }
        Debug.Log("END BATTLES");
        PauseScript.GameIsPause = true;
        Time.timeScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        // while player's health >= 0

    }
}
