using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform planetCenter; // —сылка на центр планеты
    public float defaultMoveSpeed = 3.0f;
    public float rotationSpeed = 100.0f; // —корость вращени€ игрока
    public float orbitRadius = 10.0f;
    private int currentHealth;
    private int defaultMaxHealth = 30;
    private int HPUpgradeLevel;
    private GameManager GameManager;
    float defaultFireRate = 0.5f; // Ќова€ скорость стрельбы после улучшени€
    public ParticleSystem explosionEffect; // —сылка на систему частиц взрыва

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
        GetComponent<Shooting>().SetFireRate(defaultFireRate - 0.05f * GameManager.fireRateLevel); // ѕримен€ем новую скорость стрельбы
    }

    public void UpdateSpeed()
    {
        defaultMoveSpeed += 0.3f * GameManager.speedUpgradeLevel;
    }



    private void Update()
    {
        // ѕолучаем позицию мыши в мировых координатах
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // »гнорируем вертикальную компоненту Z
        mousePosition.z = 0;

        // ¬ычисл€ем направление от игрока к позиции мыши
        Vector3 moveDirection = mousePosition - transform.position;

        // Ќормализуем вектор направлени€, чтобы получить только направление без скорости
        moveDirection.Normalize();

        // ѕровер€ем рассто€ние между игроком и курсором
        float distanceToCursor = moveDirection.magnitude;

        // ≈сли рассто€ние больше некоторого порога (например, 0.1f), устанавливаем направление взгл€да игрока
        if (distanceToCursor > 0.5f)
        {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg + 90;

            // ”читываем скорость вращени€
            float step = rotationSpeed * Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, angle), step);
        }

        // ѕеремещаем игрока в направлении мыши с учетом скорости
        transform.position += moveDirection * defaultMoveSpeed * Time.deltaTime;

        // ќграничиваем рассто€ние игрока от центра планеты
        Vector3 offsetFromCenter = transform.position - planetCenter.position;
        if (offsetFromCenter.magnitude > orbitRadius)
        {
            // ≈сли игрок выходит за пределы радиуса, устанавливаем его на круговую траекторию
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
