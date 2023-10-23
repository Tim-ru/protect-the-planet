using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint; // Позиция, откуда будет выпущена пуля
    public GameObject bulletPrefab; // Префаб пули
    public GameObject shotgunBulletPrefab; // Префаб пули
    public GameObject sniperBulletPrefab; // Префаб пули
    public float bulletSpeed = 2f; // Скорость пули
    public AudioClip shotSound;
    private AudioSource audioSource;
    private float nextFireTime = 0f; // Время следующего возможного выстрела
    private float fireRate = 0.5f; // Стандартная скорость стрельбы
    public GameManager GameManager;
    private string currentGunMode;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        GameManager = FindObjectOfType<GameManager>();
        currentGunMode = GameManager.GunMode;
    }
    void Update()
    {
        // Логика стрельбы
        if (Input.GetButton("Fire1") && Time.time > nextFireTime)
        {
            switch(GameManager.GetCurrentGunMode())
            {
                case "default":
                    DefaultShoot();
                    break;
                case "shotgun":
                    ShotgunShoot();
                    break;
                case "sniper":
                    SniperShoot();
                    break;
            }

            // Обновляем время следующего возможного выстрела
            nextFireTime = Time.time + fireRate; // Здесь fireRate - это текущая скорость стрельбы
        }
    }

    void DefaultShoot()
    {
        // Создаем экземпляр пули из префаба
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Получаем компонент Bullet из созданной пули
        Bullet bulletScript = bullet.GetComponent<Bullet>();

        if (bulletScript != null)
        {
            // Устанавливаем скорость и направление пули
            bulletScript.SetSpeedAndDirection(bulletSpeed, firePoint.up);
            bulletScript.SetBulletDamage(15);
        }

        
        audioSource.PlayOneShot(shotSound);
        

        Destroy(bullet, 5f);
    }

    void ShotgunShoot()
    {
        for (int i = 0; i < 3; i++)
        {
            // Создаем экземпляр пули из префаба
            GameObject bullet = Instantiate(shotgunBulletPrefab, firePoint.position, firePoint.rotation);

            // Поворачиваем каждую пулю на определенный угол, например, 0 градусов, -15 градусов и 15 градусов
            float bulletAngle = i * 15f; // 15 градусов между пулями
            bullet.transform.Rotate(Vector3.forward, bulletAngle);

            // Получаем компонент Bullet из созданной пули
            Bullet bulletScript = bullet.GetComponent<Bullet>();

            if (bulletScript != null)
            {
                // Устанавливаем скорость и направление пули
                bulletScript.SetSpeedAndDirection(bulletSpeed / 2, bullet.transform.up);
            }

            audioSource.PlayOneShot(shotSound);

            Destroy(bullet, 1.5f);
        }
    }

    void SniperShoot()
    {
        // Создаем экземпляр пули из префаба
        GameObject bullet = Instantiate(sniperBulletPrefab, firePoint.position, firePoint.rotation);

        // Получаем компонент Bullet из созданной пули
        Bullet bulletScript = bullet.GetComponent<Bullet>();

        if (bulletScript != null)
        {
            // Устанавливаем скорость и направление пули
            bulletScript.SetSpeedAndDirection(bulletSpeed * 1.75f, firePoint.up);
        }


        audioSource.PlayOneShot(shotSound);


        Destroy(bullet, 5f);
    }

    public void SetFireRate(float newFireRate)
    {
        Debug.Log(newFireRate);
        fireRate = newFireRate;
    }
}
