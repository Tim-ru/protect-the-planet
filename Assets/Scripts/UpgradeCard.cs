using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeCard : MonoBehaviour
{
    public TextMeshProUGUI cardNameText; // ������ �� ��������� ��������� ��� ����� ��������
    public Image cardImage; // ������ �� ����������� ��������
    public UpgradePanel upgradePanel; // ������ ���������


    private void Start()
    {
        upgradePanel = FindObjectOfType<UpgradePanel>();

    }
    public void UpdateCard(string cardName, Sprite cardSprite)
    {
        // ������������� ��� �������� � ��������� ���������
        cardNameText.text = cardName;

        // ������������� ����������� ��������
        cardImage.sprite = cardSprite;

    }

    
}