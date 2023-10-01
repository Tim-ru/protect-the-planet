using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{

    public TextMeshProUGUI gameOverText; 
    public void Setup(int score)
    {
        Debug.Log("game over");
        gameObject.SetActive(true);
    }
}
