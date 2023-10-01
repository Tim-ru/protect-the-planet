using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetController : MonoBehaviour
{
    public int maxHealth = 100; // ������������ �������� �������
    public int currentHealth; // ������� �������� �������
    private GameManager GameManager;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        GameManager = FindObjectOfType<GameManager>();
        GameManager.UpdatePlanetHPText();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // ������������� �������� ������

        // ������� ������ �������
        // Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        GameManager.UpdatePlanetHPText();
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }
}
