using System.Security.Cryptography;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed = 1f; // �������� ����
    private Vector3 direction; // ����������� �������� ����
    private int baseDamage = 10;
    private int damageMultiplier;
    private GameManager GameManager;

    // �������� ����� ��� ��������� �������� � ����������� ����
    public void SetSpeedAndDirection(float bulletSpeed, Vector3 bulletDirection)
    {
        speed = bulletSpeed;
        direction = transform.up; // ����������� �����������, ����� ������� ��� ��������� ��������
        GameManager = FindObjectOfType<GameManager>();
        damageMultiplier = GameManager.damageUpgradeLevel;
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

        // ���� ��� ���������, ������� ��� ����
        if (enemy != null)
        {
            enemy.TakeDamage(baseDamage + damageMultiplier * 10);
        }

        // ���������� ���� ����� ���������
        Destroy(gameObject);
    }
}
