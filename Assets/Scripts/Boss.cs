using UnityEngine;

public class Boss : MonoBehaviour
{
    public float maxHealth = 500; // ������������ �������� �����
    public float currentHealth;
    public float attackInterval = 6f; // �������� ����� �������
    public int attackDamage = 10; // ����, ��������� ������ �����
    public float moveSpeed = 2f; // �������� �������� �����
    public Planet planet; // ������ �� ����� �������
    public Transform attackPoint; // �������, ������ ����� �������� ����
    public GameObject bulletPrefab; // ������ ���� �����
    public float maxRadius = 2f; // ������������ ������ ��������

    public AudioClip attackSound;
    private AudioSource audioSource;

    private bool isDead = false;
    private float nextAttackTime = 0f;

    void Start()
    {
        currentHealth = maxHealth;
        audioSource = GetComponent<AudioSource>();

        // ������ ������������ ���������
        InvokeRepeating("Attack", attackInterval, attackInterval);
    }

    void Update()
    {
        // ��������� ���������� �� �������
        float distanceToPlanet = Vector3.Distance(transform.position, planet.transform.position);

        // ���� �����, ��� ���������� ������, ������������ �� �������
        if (distanceToPlanet < maxRadius)
        {
            Vector3 directionToPlanet = (transform.position - planet.transform.position).normalized;
            transform.position = planet.transform.position + directionToPlanet * maxRadius;
        }

        MoveAroundPlanet();
    }

    void MoveAroundPlanet()
    {
        // ��������� ����� ������� �����, ����� �� �������� ������ �������
        Vector3 center = planet.transform.position;
        Vector3 directionToCenter = center - transform.position;
        directionToCenter.Normalize(); // �����������, ����� ��������� � ���������� ���������
        transform.Translate(directionToCenter * moveSpeed * Time.deltaTime);
    }

    void Attack()
    {
        if (isDead)
        {
            return;
        }

        // ���������, ������ �� ���������� ������� ��� ��������� �����
        if (Time.time >= nextAttackTime)
        {
            // ������� ��������� ���� �� �������
            GameObject bullet = Instantiate(bulletPrefab, attackPoint.position, attackPoint.rotation);

            // �������� ��������� Bullet �� ��������� ����
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            bulletScript.SetPlanetController(planet); // �������� ������ �� PlanetController

            if (bulletScript != null)
            {
                bulletScript.SetBulletDamage(attackDamage);
                // ������������� �������� � ����������� ����
                bulletScript.SetSpeedAndDirection(3f, attackPoint.up);
            }


            audioSource.PlayOneShot(attackSound);


            Destroy(bullet, 5f);
        }
    }

    public void TakeDamage(float damage)
    {
        if (isDead)
        {
            return;
        }

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        // ���������� ������ ������ �����, ��������, �������������� �������� � �����

        // ���������� ����� �����
        CancelInvoke("Attack");

        // ���������� ������ �����
        Destroy(gameObject);
    }
}
