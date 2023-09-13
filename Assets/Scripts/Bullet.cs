using System.Security.Cryptography;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed = 1f; // �������� ����
    private Vector3 direction; // ����������� �������� ����
    // �������� ����� ��� ��������� �������� � ����������� ����
    public void SetSpeedAndDirection(float bulletSpeed, Vector3 bulletDirection)
    {
        speed = bulletSpeed;
        direction = transform.up; // ����������� �����������, ����� ������� ��� ��������� ��������
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
            enemy.TakeDamage(10);
        }

        // ���������� ���� ����� ���������
        Destroy(gameObject);
    }
}
