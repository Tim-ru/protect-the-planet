using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 0.6f;
    public int damageAmount = 10; // ����, ��������� ������
    public float maxHealth = 100;
    public float currentHealth;
    private PlanetController planet;
    private PlayerController player;
    private Renderer enemyRenderer; // ������ �� ��������� Renderer ������
    private GameManager GameManager;

    public Material maxHealtMaterial; // �������� ��� ��������� ���������
    public Material damagedMaterial; // �������� ��� ��������� ����������
    public Material dyingMaterial;   // �������� ��� ��������� ���������

    public AudioClip hitSound;
    public AudioClip dieSound;
    private AudioSource audioSource;

    private Collider2D enemyCollider;

    private bool isDead = false;


    // Start is called before the first frame update
    void Start()
    {
        planet = FindObjectOfType<PlanetController>();
        player = FindObjectOfType<PlayerController>();
        currentHealth = maxHealth;
        GameManager = FindObjectOfType<GameManager>();

        enemyRenderer = GetComponent<Renderer>();
        enemyRenderer.material = maxHealtMaterial;
        enemyCollider = GetComponent<Collider2D>();
        audioSource = GetComponent<AudioSource>();
    }


    private void Update()
    {
        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane));
        targetPosition.z = transform.position.z; // ���������, ��� z-���������� ���������

        Vector3 directionToTarget = targetPosition - transform.position;
        Vector3 normalizedDirection = directionToTarget.normalized;
        Vector3 movement = normalizedDirection * moveSpeed * Time.deltaTime;

        transform.position += movement;
        LookInCenter();
    }

    void LookInCenter()
    {
        Vector3 centerScreenPosition = new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane);
        Vector3 centerWorldPosition = Camera.main.ScreenToWorldPoint(centerScreenPosition);
        centerWorldPosition.z = transform.position.z; // ���������, ��� z-���������� ���������

        Vector3 lookDirection = centerWorldPosition - transform.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg + 90f;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = targetRotation;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // ��� ������������ � ���-�� (��������, �����) ������� ���� �������
        if (collision.gameObject.CompareTag("Planet"))
        {
            planet.TakeDamage(damageAmount);
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            player.TakeDamage(damageAmount);
        }
    }

    public void TakeDamage(float damage)
    {
        if (isDead)
        {
            return;
        }

        currentHealth -= damage;


        // �������� �������� � ����������� �� �������� ��������
        if (currentHealth <= 0)
        {
            Die();
        }
        else if (currentHealth <= 30)
        {
            enemyRenderer.material = dyingMaterial; // �������� ��� ����������
        }
        else if (currentHealth <= 60)
        {
            enemyRenderer.material = damagedMaterial; // �������� ��� ����������
        }
        if (audioSource != null && hitSound != null)
        {
            audioSource.PlayOneShot(hitSound);
        }

    }

    private void Die()
    {
        isDead = true;

        // ������������� �������� ������
        // ������������� ���� ������ �� ������� ����������
        if (audioSource != null && dieSound != null)
        {
            audioSource.PlayOneShot(dieSound);
        }
        enemyRenderer.enabled = false;
        enemyCollider.enabled = false;
        GameManager.UpdateScore(1);

        StartCoroutine(DelayedDestroy());
    }

    IEnumerator DelayedDestroy()
    {
        yield return new WaitForSeconds(dieSound.length);
        Destroy(gameObject);
    }
}
