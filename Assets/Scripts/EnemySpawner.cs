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
    private float spawnOffset = 5f; // Расстояние от краев экрана до спавна
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

        // Генерируем случайные координаты спавна вне области видимости игрока
        Vector2 spawnPosition = Vector2.zero;
        int randomSide = Random.Range(0, 4);

        switch (randomSide)
        {
            case 0: // Верх экрана
                spawnPosition = new Vector2(Random.Range(0f, screenWidth), screenHeight + spawnOffset);
                break;
            case 1: // Низ экрана
                spawnPosition = new Vector2(Random.Range(0f, screenWidth), -spawnOffset);
                break;
            case 2: // Лево экрана
                spawnPosition = new Vector2(-spawnOffset, Random.Range(0f, screenHeight));
                break;
            case 3: // Право экрана
                spawnPosition = new Vector2(screenWidth + spawnOffset, Random.Range(0f, screenHeight));
                break;
        }

        Vector2 worldSpawnPosition = Camera.main.ScreenToWorldPoint(spawnPosition);

        return worldSpawnPosition;
    }

    public void SpawnBoss()
    {
        Vector2 spawnPos = GenerateSpawnPos();
        // Ваш код, который создает экземпляры босса
        GameObject boss = Instantiate(bossPrefab, spawnPos, Quaternion.identity);

        // Получите компонент Boss из созданного объекта
        Boss bossComponent = boss.GetComponent<Boss>();

        // Установите ссылку на планету
        bossComponent.planet = planet; // где planetTransform - ссылка на вашу планету
    }
}
