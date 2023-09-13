using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform planetCenter; // —сылка на центр планеты
    public float moveSpeed = 5.0f;
    public float rotationSpeed = 100.0f; // —корость вращени€ игрока
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
        if (distanceToCursor > 0.1f)
        {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg + 90;

            // ”читываем скорость вращени€
            float step = rotationSpeed * Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, angle), step);
        }

        // ѕеремещаем игрока в направлении мыши с учетом скорости
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

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
        GameManager.UpdatePlayerHP();
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

}
