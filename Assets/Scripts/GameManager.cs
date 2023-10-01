using System;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI planetHPText;
    public TextMeshProUGUI playerHPText;
    public TextMeshProUGUI playerScoreText;
    public PlanetController planet;
    public PlayerController player;
    private int score = 0;
    public int damageUpgradeLevel;
    public int healthPointsUpgradeLevel = 1;
    public int speedUpgradeLevel = 0;
    public int fireRateLevel = 1;
    public bool isHaveUpgrade = false;
    public int playerLVL;


    public GameObject UpgradePanel;
    public GameOverScreen GameOverScreen;
    public int upgradePoints = 0;
    private int pointsNeeded = 2; // ���������� ����� ��� ��������� 1 ������
    public UpgradePanel upgradePanel;
    public EnemySpawner EnemySpawner;
   

    public string GunMode = "default"; 
    

    // Start is called before the first frame update
    void Start()
    {
        planet = FindObjectOfType<PlanetController>();
        player = FindObjectOfType<PlayerController>();
        playerLVL = 1;
        UpdatePlanetHPText();
        UpdatePlayerHPText();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetCurrentHealth() <= 0)
        {
            GameOver();
        }
    }

    public void UpdatePlanetHPText()
    {
        planetHPText.text = "Planet HP: " + GetPlanetHealth();
    }

    public void UpdatePlayerHPText()
    {
        playerHPText.text = "Player HP: " + GetPlayerHealth();
    }

    public void UpdateScore(int scoreAmount)
    {
        score += scoreAmount;
        playerScoreText.text = "Score: " + score;

        // ����������� ������� ������ �� ������ ��������� ���� score
        if (score >= playerLVL * pointsNeeded) 
        {
            ++playerLVL; // ����������� ������� ������
            ++upgradePoints;
            Debug.Log("playerLVL" + playerLVL);
            Debug.Log("upgradePoints" + upgradePoints);
            Debug.Log("score" + score);
        }

        // ���������� ������ ���������, ���� ���� ��������� ������ ��� ���������
        if (playerLVL > 1 && upgradePoints > 0) // �����������, ��� ����� ����� �������� �������, ������� � 2
        {
            upgradePanel.ShowUpgradePanel();
        }

        if(score == 5)
        {
            EnemySpawner.SpawnBoss();
        }
    }

    public void UpgradeDamageLevel()
    {
        damageUpgradeLevel++;
    }

    public void UpgradeSpeedLevel()
    {
        speedUpgradeLevel++;
        player.UpdateSpeed();
    }

    public void UpgradeFireRateLevel()
    {
        fireRateLevel++;
        Debug.Log("UpgradeFireRateLevel" + fireRateLevel);
        player.UpdateFireRate();
    }


    public void UpgradeHPLevel()
    {
        healthPointsUpgradeLevel++;
        player.UpdateMaxHealth();
        Debug.Log("current hp lvl: " + healthPointsUpgradeLevel);
    }

    public void UpdradeGunMode(string mode)
    {
        GunMode = mode;
    }

    public string GetCurrentGunMode()
    {
        return GunMode;
    }

    public int GetPlanetHealth()
    {
        if (planet != null)
        {
            return planet.GetCurrentHealth();
        }
        else
        {
            Debug.LogError("������ �� ������� �� ����������� � GameManager.");
            return 0;
        }
    }

    public int GetPlayerHealth()
    {
        if (player != null)
        {
            return player.GetCurrentHealth();
        }
        else
        {
            Debug.LogError("������ �� Player �� ����������� � GameManager.");
            return 0;
        }
    }

    public int GetCurrentScore()
    {
        return score;
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
    }

    public void GameOver()
    {
        GameOverScreen.Setup(score);
        Time.timeScale = 0;
    }
}
