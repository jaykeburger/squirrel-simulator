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

    }
}
