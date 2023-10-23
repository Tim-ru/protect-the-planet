using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject bossPrefab;

    private float minSpawnTime = 3;
    private float maxSpawnTime = 5;
    private float timeUntilSpawn;
    private float spawnOffset = 5f; // ���������� �� ����� ������ �� ������
    public Planet planet;

    void Awake()
    {
        SetTimeUntilSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        timeUntilSpawn -= Time.deltaTime;
        if (timeUntilSpawn <= 0)
        {
            Vector2 spawnPos = GenerateSpawnPos();
            Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
            SetTimeUntilSpawn();
        }
    }

    private void SetTimeUntilSpawn()
    {
        timeUntilSpawn = Random.Range(minSpawnTime, maxSpawnTime);
    }

    private Vector2 GenerateSpawnPos()
    {
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        // ���������� ��������� ���������� ������ ��� ������� ��������� ������
        Vector2 spawnPosition = Vector2.zero;
        int randomSide = Random.Range(0, 4);

        switch (randomSide)
        {
            case 0: // ���� ������
                spawnPosition = new Vector2(Random.Range(0f, screenWidth), screenHeight + spawnOffset);
                break;
            case 1: // ��� ������
                spawnPosition = new Vector2(Random.Range(0f, screenWidth), -spawnOffset);
                break;
            case 2: // ���� ������
                spawnPosition = new Vector2(-spawnOffset, Random.Range(0f, screenHeight));
                break;
            case 3: // ����� ������
                spawnPosition = new Vector2(screenWidth + spawnOffset, Random.Range(0f, screenHeight));
                break;
        }

        Vector2 worldSpawnPosition = Camera.main.ScreenToWorldPoint(spawnPosition);

        return worldSpawnPosition;
    }

    public void SpawnBoss()
    {
        Vector2 spawnPos = GenerateSpawnPos();
        // ��� ���, ������� ������� ���������� �����
        GameObject boss = Instantiate(bossPrefab, spawnPos, Quaternion.identity);

        // �������� ��������� Boss �� ���������� �������
        Boss bossComponent = boss.GetComponent<Boss>();

        // ���������� ������ �� �������
        bossComponent.planet = planet; // ��� planetTransform - ������ �� ���� �������
    }
}
