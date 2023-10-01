using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeCard : MonoBehaviour
{
    public TextMeshProUGUI cardNameText; // Ссылка на текстовый компонент для имени карточки
    public Image cardImage; // Ссылка на изображение карточки
    public UpgradePanel upgradePanel; // Панель улучшений


    private void Start()
    {
        upgradePanel = FindObjectOfType<UpgradePanel>();

    }
    public void UpdateCard(string cardName, Sprite cardSprite)
    {
        // Устанавливаем имя карточки в текстовый компонент
        cardNameText.text = cardName;

        // Устанавливаем изображение карточки
        cardImage.sprite = cardSprite;

    }

    
}