using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI planetHPText;
    public TextMeshProUGUI playerHPText;
    public PlanetController planet;
    public PlayerController player;


    // Start is called before the first frame update
    void Start()
    {
        planet = FindObjectOfType<PlanetController>();
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdatePlanetHP()
    {
        planetHPText.text = "Planet HP: " + GetPlanetHealth();
    }

    public void UpdatePlayerHP()
    {
        playerHPText.text = "Player HP: " + GetPlayerHealth();
    }

    public int GetPlanetHealth()
    {
        if (planet != null)
        {
            return planet.GetCurrentHealth();
        }
        else
        {
            Debug.LogError("—сылка на планету не установлена в GameManager.");
            return 0;
        }
    }

    public int GetPlayerHealth()
    {
        if (player != null)
        {
            return player.GetCurrentHealth();
        }
        else
        {
            Debug.LogError("—сылка на Player не установлена в GameManager.");
            return 0;
        }
    }
}
