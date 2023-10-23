using System.Security.Cryptography;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed = 1f; // Скорость пули
    private Vector3 direction; // Направление движения пули
    private int baseDamage = 10;
    private int damageMultiplier;
    private GameManager GameManager;
    private Planet planet;
    public void SetPlanetController(Planet _planet)
    {
        planet = _planet;
    }

    // Добавьте метод для установки скорости и направления пули
    public void SetSpeedAndDirection(float bulletSpeed, Vector3 bulletDirection)
    {
        speed = bulletSpeed;

        direction = transform.up; // Нормализуем направление, чтобы сделать его единичным вектором
        GameManager = FindObjectOfType<GameManager>();
        damageMultiplier = GameManager.damageUpgradeLevel;
    }


    public void SetBulletDamage(int damage)
    {
        baseDamage = damage;
        Debug.Log(damage);
    }
    void Update()
    {
        // Двигаем пулю в указанном направлении с указанной скоростью
        transform.position += direction * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Проверяем, является ли объект, в который попала пуля, противником
        Enemy enemy = other.GetComponent<Enemy>();
        Planet planet = other.GetComponent<Planet>();
        PlayerController player= other.GetComponent<PlayerController>();
        Debug.Log(other);


        // Если это противник, наносим ему урон
        if (enemy != null)
        {
            enemy.TakeDamage(baseDamage + damageMultiplier * 10);
        }

        if (planet!= null)
        {
            planet.TakeDamage(baseDamage);
        }

        if(player!= null)
        {
            player.TakeDamage(baseDamage);
        }

        // Уничтожаем пулю после попадания
        Destroy(gameObject);
    }
}
