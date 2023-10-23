using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint; // �������, ������ ����� �������� ����
    public GameObject bulletPrefab; // ������ ����
    public GameObject shotgunBulletPrefab; // ������ ����
    public GameObject sniperBulletPrefab; // ������ ����
    public float bulletSpeed = 2f; // �������� ����
    public AudioClip shotSound;
    private AudioSource audioSource;
    private float nextFireTime = 0f; // ����� ���������� ���������� ��������
    private float fireRate = 0.5f; // ����������� �������� ��������
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
        // ������ ��������
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

            // ��������� ����� ���������� ���������� ��������
            nextFireTime = Time.time + fireRate; // ����� fireRate - ��� ������� �������� ��������
        }
    }

    void DefaultShoot()
    {
        // ������� ��������� ���� �� �������
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // �������� ��������� Bullet �� ��������� ����
        Bullet bulletScript = bullet.GetComponent<Bullet>();

        if (bulletScript != null)
        {
            // ������������� �������� � ����������� ����
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
            // ������� ��������� ���� �� �������
            GameObject bullet = Instantiate(shotgunBulletPrefab, firePoint.position, firePoint.rotation);

            // ������������ ������ ���� �� ������������ ����, ��������, 0 ��������, -15 �������� � 15 ��������
            float bulletAngle = i * 15f; // 15 �������� ����� ������
            bullet.transform.Rotate(Vector3.forward, bulletAngle);

            // �������� ��������� Bullet �� ��������� ����
            Bullet bulletScript = bullet.GetComponent<Bullet>();

            if (bulletScript != null)
            {
                // ������������� �������� � ����������� ����
                bulletScript.SetSpeedAndDirection(bulletSpeed / 2, bullet.transform.up);
            }

            audioSource.PlayOneShot(shotSound);

            Destroy(bullet, 1.5f);
        }
    }

    void SniperShoot()
    {
        // ������� ��������� ���� �� �������
        GameObject bullet = Instantiate(sniperBulletPrefab, firePoint.position, firePoint.rotation);

        // �������� ��������� Bullet �� ��������� ����
        Bullet bulletScript = bullet.GetComponent<Bullet>();

        if (bulletScript != null)
        {
            // ������������� �������� � ����������� ����
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
