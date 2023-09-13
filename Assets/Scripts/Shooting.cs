using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint; // �������, ������ ����� �������� ����
    public GameObject bulletPrefab; // ������ ����
    public float bulletSpeed = 2f; // �������� ����
    public AudioClip shotSound;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        // ������ ��������
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // ������� ��������� ���� �� �������
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // �������� ��������� Bullet �� ��������� ����
        Bullet bulletScript = bullet.GetComponent<Bullet>();

        if (bulletScript != null)
        {
            // ������������� �������� � ����������� ����
            bulletScript.SetSpeedAndDirection(bulletSpeed, firePoint.up);
        }

        
        audioSource.PlayOneShot(shotSound);
        

        Destroy(bullet, 5f);
    }
}
