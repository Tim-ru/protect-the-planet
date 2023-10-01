using UnityEngine;

public class Boss : MonoBehaviour
{
    public float maxHealth = 200; // ������������ �������� �����
    public float currentHealth;
    public float attackInterval = 3f; // �������� ����� �������
    public float attackDamage = 20; // ����, ��������� ������ �����
    public Transform attackTarget; // ���� ��� ����� (����� ��� �������)

    public AudioClip attackSound;
    private AudioSource audioSource;

    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;
        audioSource = GetComponent<AudioSource>();

        // ������ ������������ ���������
        InvokeRepeating("Attack", attackInterval, attackInterval);
    }

    void Attack()
    {
        if (isDead || attackTarget == null)
        {
            return;
        }

        // ���������� ������ ����� � ������ ������� ���� (attackTarget)
        // ��������, �������� ������� � ��������� �� � ������� ����
        // ������ ������������ Instantiate � ������ ��� �������, ��� � ����� ������� ��� ������� �����������



        if (audioSource != null && attackSound != null)
        {
            audioSource.PlayOneShot(attackSound);
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
