using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform planetCenter; // ������ �� ����� �������
    public float moveSpeed = 5.0f;
    public float rotationSpeed = 100.0f; // �������� �������� ������
    public float orbitRadius = 10.0f;
    public int currentHealth;
    public int maxHealth = 100;
    private GameManager GameManager;


    private void Start()
    {
        GameManager = FindObjectOfType<GameManager>();
        currentHealth = maxHealth;
    }

    private void Update()
    {
        // �������� ������� ���� � ������� �����������
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // ���������� ������������ ���������� Z
        mousePosition.z = 0;

        // ��������� ����������� �� ������ � ������� ����
        Vector3 moveDirection = mousePosition - transform.position;

        // ����������� ������ �����������, ����� �������� ������ ����������� ��� ��������
        moveDirection.Normalize();

        // ��������� ���������� ����� ������� � ��������
        float distanceToCursor = moveDirection.magnitude;

        // ���� ���������� ������ ���������� ������ (��������, 0.1f), ������������� ����������� ������� ������
        if (distanceToCursor > 0.1f)
        {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg + 90;

            // ��������� �������� ��������
            float step = rotationSpeed * Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, angle), step);
        }

        // ���������� ������ � ����������� ���� � ������ ��������
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        // ������������ ���������� ������ �� ������ �������
        Vector3 offsetFromCenter = transform.position - planetCenter.position;
        if (offsetFromCenter.magnitude > orbitRadius)
        {
            // ���� ����� ������� �� ������� �������, ������������� ��� �� �������� ����������
            offsetFromCenter.Normalize();
            transform.position = planetCenter.position + offsetFromCenter * orbitRadius;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        GameManager.UpdatePlayerHP();
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

}
