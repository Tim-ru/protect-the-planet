using UnityEngine;

public class UpgradePanel : MonoBehaviour
{
    public UpgradeCardManager cardManager;
    public GameObject upgradePanel; // ������ �� ������ ������ ���������
    public GameManager GameManager;

    private void Start()
    {
        // �������� ������ ��������� ��� ������ ����
        upgradePanel.SetActive(false);
    }

    // ������� ��� ����������� ������ ���������
    public void ShowUpgradePanel()
    {
        // ���������, ���� �� ������ �� ������ ���������� ����������
        if (cardManager != null)
        {
            //cardManager.ClearOldCards();
            // �������� ����� ����������� ��������� �������� �� ������� ���������� ����������
            cardManager.DisplayRandomCards();
        }
        upgradePanel.SetActive(true);
        GameManager.PauseGame();
    }

    // ������� ��� ������� ������ ���������
    public void HideUpgradePanel()
    {
        upgradePanel.SetActive(false);
        GameManager.ResumeGame();
    }
}
