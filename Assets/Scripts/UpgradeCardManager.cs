using System.Collections.Generic;
using UnityEngine;

public class UpgradeCardManager : MonoBehaviour
{
    public List<UpgradeCard> upgradeCardPrefabs; // Массив с префабами карточек улучшений
    public Transform cardSpawnPoint; // Точка, где будут появляться карточки
    public GameObject upgradePanel; // Ссылка на панель улучшений
    public Transform cardContainer; // Ссылка на контейнер для карточек

    private List<int> selectedCardIndices = new List<int>(); // Список индексов уже выбранных карточек
    private List<UpgradeCard> displayedCards = new List<UpgradeCard>(); // Список отображаемых карточек

    public void DisplayRandomCards()
    {
        // Деактивируем и скрываем старые карточки
        foreach (UpgradeCard card in displayedCards)
        {
            card.gameObject.SetActive(false);
        }

        // Очищаем списки
        selectedCardIndices.Clear();
        displayedCards.Clear();

        for (int i = 0; i < 3; i++) // Отображаем три рандомные карточки
        {
            int randomIndex;

            // Пока не найдем индекс, который еще не был выбран
            do
            {
                randomIndex = Random.Range(0, upgradeCardPrefabs.Count);
            }
            while (selectedCardIndices.Contains(randomIndex));

            selectedCardIndices.Add(randomIndex); // Добавляем индекс в список выбранных карточек

            UpgradeCard selectedCardPrefab = upgradeCardPrefabs[randomIndex];

            // Создаем экземпляр карточки из префаба
            UpgradeCard spawnedCard = Instantiate(selectedCardPrefab, cardSpawnPoint.position, Quaternion.identity);

            // Устанавливаем родителя для карточки, чтобы она оставалась внутри панели улучшений
            spawnedCard.transform.SetParent(cardSpawnPoint);

            // Позиционируем карточку на экране
            Vector3 cardPosition = cardSpawnPoint.position + new Vector3(i * 420, 0, 0); // Измените значения сдвига, чтобы установить правильное расположение
            spawnedCard.transform.position = cardPosition;

            // Активируем карточку
            spawnedCard.gameObject.SetActive(true);

            // Добавляем карточку в список отображаемых
            displayedCards.Add(spawnedCard);
        }
    }

    public void RemoveGunModeCards()
    {
        upgradeCardPrefabs.RemoveRange(0, 2);
    }
}
