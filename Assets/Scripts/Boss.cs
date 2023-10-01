using UnityEngine;

public class Boss : MonoBehaviour
{
    public float maxHealth = 200; // Максимальное здоровье босса
    public float currentHealth;
    public float attackInterval = 3f; // Интервал между атаками
    public float attackDamage = 20; // Урон, наносимый атакой босса
    public Transform attackTarget; // Цель для атаки (игрок или планета)

    public AudioClip attackSound;
    private AudioSource audioSource;

    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;
        audioSource = GetComponent<AudioSource>();

        // Начать периодически атаковать
        InvokeRepeating("Attack", attackInterval, attackInterval);
    }

    void Attack()
    {
        if (isDead || attackTarget == null)
        {
            return;
        }

        // Реализуйте логику атаки с учетом текущей цели (attackTarget)
        // Например, создайте снаряды и отправьте их в сторону цели
        // Можете использовать Instantiate и скрипт для снаряда, как в вашем скрипте для обычных противников



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
        // Реализуйте логику смерти босса, например, воспроизведите анимацию и звуки

        // Остановите атаки босса
        CancelInvoke("Attack");

        // Уничтожьте объект босса
        Destroy(gameObject);
    }
}
