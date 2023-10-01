using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    private GameManager GameManager;
    public UpgradeType upgradeType; // ������������ ��� ������ ����� ���������
    public UpgradePanel upgradePanel;

    public void Start()
    {
        GameManager = FindObjectOfType<GameManager>();
        upgradePanel= FindObjectOfType<UpgradePanel>();
    }


    public void Upgrade()
    {
        // ���������� ��� ������� ������ ��������
        switch (upgradeType)
        {
            case UpgradeType.Damage:
                GameManager.UpgradeDamageLevel();
                break;
            case UpgradeType.HP:
                GameManager.UpgradeHPLevel();
                break;
            case UpgradeType.Speed:
                GameManager.UpgradeSpeedLevel();
                break;
            case UpgradeType.FireRate:
                GameManager.UpgradeFireRateLevel();
                break;
            case UpgradeType.ShotgunMode:
                GameManager.UpdradeGunMode("shotgun");
                break;
            case UpgradeType.SniperMode:
                GameManager.UpdradeGunMode("sniper");
                break;
        }
        GameManager.upgradePoints--;
        if (upgradePanel!= null)
        {
            upgradePanel.HideUpgradePanel();
        }
    }
}

// ������������ ��� ������ ����� ���������
public enum UpgradeType
{
    Damage,
    HP,
    Speed,
    FireRate,
    ShotgunMode,
    SniperMode
    // �������� �������������� ���� ���������, ���� ����������
}
