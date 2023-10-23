using System.Security.Cryptography;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed = 1f; // �������� ����
    private Vector3 direction; // ����������� �������� ����
    private int baseDamage = 10;
    private int damageMultiplier;
    private GameManager GameManager;
    private Planet planet;
    public void SetPlanetController(Planet _planet)
    {
        planet = _planet;
    }

    // �������� ����� ��� ��������� �������� � ����������� ����
    public void SetSpeedAndDirection(float bulletSpeed, Vector3 bulletDirection)
    {
        speed = bulletSpeed;

        direction = transform.up; // ����������� �����������, ����� ������� ��� ��������� ��������
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
        // ������� ���� � ��������� ����������� � ��������� ���������
        transform.position += direction * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // ���������, �������� �� ������, � ������� ������ ����, �����������
        Enemy enemy = other.GetComponent<Enemy>();
        Planet planet = other.GetComponent<Planet>();
        PlayerController player= other.GetComponent<PlayerController>();
        Debug.Log(other);


        // ���� ��� ���������, ������� ��� ����
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

        // ���������� ���� ����� ���������
        Destroy(gameObject);
    }
}
