using UnityEngine;

public class UpgradePanel : MonoBehaviour
{
    public UpgradeCardManager cardManager;
    public GameObject upgradePanel; // Ссылка на объект панели улучшения
    public GameManager GameManager;

    private void Start()
    {
        // Скрываем панель улучшения при старте игры
        upgradePanel.SetActive(false);
    }

    // Функция для отображения панели улучшения
    public void ShowUpgradePanel()
    {
        // Проверяем, есть ли ссылка на скрипт управления карточками
        if (cardManager != null)
        {
            //cardManager.ClearOldCards();
            // Вызываем метод отображения рандомных карточек из скрипта управления карточками
            cardManager.DisplayRandomCards();
        }
        upgradePanel.SetActive(true);
        GameManager.PauseGame();
    }

    // Функция для скрытия панели улучшения
    public void HideUpgradePanel()
    {
        upgradePanel.SetActive(false);
        GameManager.ResumeGame();
    }
}
