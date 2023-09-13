using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint; // Позиция, откуда будет выпущена пуля
    public GameObject bulletPrefab; // Префаб пули
    public float bulletSpeed = 2f; // Скорость пули
    public AudioClip shotSound;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        // Логика стрельбы
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Создаем экземпляр пули из префаба
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Получаем компонент Bullet из созданной пули
        Bullet bulletScript = bullet.GetComponent<Bullet>();

        if (bulletScript != null)
        {
            // Устанавливаем скорость и направление пули
            bulletScript.SetSpeedAndDirection(bulletSpeed, firePoint.up);
        }

        
        audioSource.PlayOneShot(shotSound);
        

        Destroy(bullet, 5f);
    }
}
