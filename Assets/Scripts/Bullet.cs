using System.Security.Cryptography;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed = 1f;
    private Vector3 direction;
    private int baseDamage = 10;
    private int damageMultiplier;
    private GameManager GameManager;
    private Planet planet;

    private int penetrateCount = 1;
    private bool isSniperBullet = false; // Флаг, указывающий, является ли пуля снайперской

    public void SetPlanetController(Planet _planet)
    {
        planet = _planet;
    }

    public void SetBulletDamage(int amount)
    {
        baseDamage = amount;
    }

    public void SetSpeedAndDirection(float bulletSpeed, Vector3 bulletDirection)
    {
        speed = bulletSpeed;
        direction = bulletDirection.normalized;
        GameManager = FindObjectOfType<GameManager>();
        damageMultiplier = GameManager.damageUpgradeLevel;
    }

    public void SetPenetrateCount(int count)
    {
        penetrateCount = count;
        Debug.Log("count" + count);
    }

    public void SetSniperBullet(bool isSniper)
    {
        isSniperBullet = isSniper;
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        Planet planet = other.GetComponent<Planet>();
        PlayerController player = other.GetComponent<PlayerController>();


        if (enemy != null && penetrateCount >= 0)
        {
            enemy.TakeDamage(baseDamage + damageMultiplier * 10);
            penetrateCount--;
        }

        if (planet != null && !isSniperBullet) // Добавьте проверку на тип пули
        {
            planet.TakeDamage(baseDamage);
        }

        if (player != null)
        {
            player.TakeDamage(baseDamage);
        }
         
        if (!isSniperBullet)
        {
            Destroy(gameObject);
        }

        Debug.Log("penetrates: " + penetrateCount + " " + isSniperBullet);
    }
}
