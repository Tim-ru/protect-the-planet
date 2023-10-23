using UnityEngine;

public class Boss : MonoBehaviour
{
    public float maxHealth = 500; // Максимальное здоровье босса
    public float currentHealth;
    public float attackInterval = 6f; // Интервал между атаками
    public int attackDamage = 10; // Урон, наносимый атакой босса
    public float moveSpeed = 2f; // Скорость движения босса
    public Planet planet; // Ссылка на центр планеты
    public Transform attackPoint; // Позиция, откуда будет выпущена пуля
    public GameObject bulletPrefab; // Префаб пули босса
    public float maxRadius = 2f; // Максимальный радиус движения

    public AudioClip attackSound;
    private AudioSource audioSource;

    private bool isDead = false;
    private float nextAttackTime = 0f;

    void Start()
    {
        currentHealth = maxHealth;
        audioSource = GetComponent<AudioSource>();

        // Начать периодически атаковать
        InvokeRepeating("Attack", attackInterval, attackInterval);
    }

    void Update()
    {
        // Вычисляем расстояние до планеты
        float distanceToPlanet = Vector3.Distance(transform.position, planet.transform.position);

        // Если ближе, чем допустимый радиус, перемещаемся от планеты
        if (distanceToPlanet < maxRadius)
        {
            Vector3 directionToPlanet = (transform.position - planet.transform.position).normalized;
            transform.position = planet.transform.position + directionToPlanet * maxRadius;
        }

        MoveAroundPlanet();
    }

    void MoveAroundPlanet()
    {
        // Вычисляем новую позицию босса, чтобы он двигался вокруг планеты
        Vector3 center = planet.transform.position;
        Vector3 directionToCenter = center - transform.position;
        directionToCenter.Normalize(); // Нормализуем, чтобы двигаться с постоянной скоростью
        transform.Translate(directionToCenter * moveSpeed * Time.deltaTime);
    }

    void Attack()
    {
        if (isDead)
        {
            return;
        }

        // Проверяем, прошло ли достаточно времени для следующей атаки
        if (Time.time >= nextAttackTime)
        {
            // Создаем экземпляр пули из префаба
            GameObject bullet = Instantiate(bulletPrefab, attackPoint.position, attackPoint.rotation);

            // Получаем компонент Bullet из созданной пули
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            bulletScript.SetPlanetController(planet); // Передача ссылки на PlanetController

            if (bulletScript != null)
            {
                bulletScript.SetBulletDamage(attackDamage);
                // Устанавливаем скорость и направление пули
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
        // Реализуйте логику смерти босса, например, воспроизведите анимацию и звуки

        // Остановите атаки босса
        CancelInvoke("Attack");

        // Уничтожьте объект босса
        Destroy(gameObject);
    }
}
