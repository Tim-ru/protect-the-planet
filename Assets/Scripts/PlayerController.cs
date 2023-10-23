using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform planetCenter; // ������ �� ����� �������
    public float defaultMoveSpeed = 3.0f;
    public float rotationSpeed = 100.0f; // �������� �������� ������
    public float orbitRadius = 10.0f;
    private int currentHealth;
    private int defaultMaxHealth = 30;
    private int HPUpgradeLevel;
    private GameManager GameManager;
    float defaultFireRate = 0.5f; // ����� �������� �������� ����� ���������
    public ParticleSystem explosionEffect; // ������ �� ������� ������ ������

    private void Start()
    {
        GameManager = FindObjectOfType<GameManager>();
        HPUpgradeLevel = GameManager.healthPointsUpgradeLevel;
        currentHealth = defaultMaxHealth > 0 ? defaultMaxHealth : 30;
    }

    public void UpdateMaxHealth()
    {
        defaultMaxHealth = defaultMaxHealth + GameManager.healthPointsUpgradeLevel * 10;
        currentHealth = defaultMaxHealth;
        GameManager.UpdatePlayerHPText();
    }

    public void UpdateFireRate()
    {
        GetComponent<Shooting>().SetFireRate(defaultFireRate - 0.05f * GameManager.fireRateLevel); // ��������� ����� �������� ��������
    }

    public void UpdateSpeed()
    {
        defaultMoveSpeed += 0.3f * GameManager.speedUpgradeLevel;
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
        if (distanceToCursor > 0.5f)
        {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg + 90;

            // ��������� �������� ��������
            float step = rotationSpeed * Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, angle), step);
        }

        // ���������� ������ � ����������� ���� � ������ ��������
        transform.position += moveDirection * defaultMoveSpeed * Time.deltaTime;

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
        GameManager.UpdatePlayerHPText();
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }
}
