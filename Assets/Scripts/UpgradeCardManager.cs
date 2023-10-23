using System.Collections.Generic;
using UnityEngine;

public class UpgradeCardManager : MonoBehaviour
{
    public List<UpgradeCard> upgradeCardPrefabs; // ������ � ��������� �������� ���������
    public Transform cardSpawnPoint; // �����, ��� ����� ���������� ��������
    public GameObject upgradePanel; // ������ �� ������ ���������
    public Transform cardContainer; // ������ �� ��������� ��� ��������

    private List<int> selectedCardIndices = new List<int>(); // ������ �������� ��� ��������� ��������
    private List<UpgradeCard> displayedCards = new List<UpgradeCard>(); // ������ ������������ ��������

    public void DisplayRandomCards()
    {
        // ������������ � �������� ������ ��������
        foreach (UpgradeCard card in displayedCards)
        {
            card.gameObject.SetActive(false);
        }

        // ������� ������
        selectedCardIndices.Clear();
        displayedCards.Clear();

        for (int i = 0; i < 3; i++) // ���������� ��� ��������� ��������
        {
            int randomIndex;

            // ���� �� ������ ������, ������� ��� �� ��� ������
            do
            {
                randomIndex = Random.Range(0, upgradeCardPrefabs.Count);
            }
            while (selectedCardIndices.Contains(randomIndex));

            selectedCardIndices.Add(randomIndex); // ��������� ������ � ������ ��������� ��������

            UpgradeCard selectedCardPrefab = upgradeCardPrefabs[randomIndex];

            // ������� ��������� �������� �� �������
            UpgradeCard spawnedCard = Instantiate(selectedCardPrefab, cardSpawnPoint.position, Quaternion.identity);

            // ������������� �������� ��� ��������, ����� ��� ���������� ������ ������ ���������
            spawnedCard.transform.SetParent(cardSpawnPoint);

            // ������������� �������� �� ������
            Vector3 cardPosition = cardSpawnPoint.position + new Vector3(i * 420, 0, 0); // �������� �������� ������, ����� ���������� ���������� ������������
            spawnedCard.transform.position = cardPosition;

            // ���������� ��������
            spawnedCard.gameObject.SetActive(true);

            // ��������� �������� � ������ ������������
            displayedCards.Add(spawnedCard);
        }
    }

    public void RemoveGunModeCards()
    {
        upgradeCardPrefabs.RemoveRange(0, 2);
    }
}
