using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    private GameManager GameManager;
    public UpgradeType upgradeType; // Перечисление для разных типов апгрейдов
    public UpgradePanel upgradePanel;
    private UpgradeCardManager UpgradeCardManager;


    public void Start()
    {
        GameManager = FindObjectOfType<GameManager>();
        upgradePanel = FindObjectOfType<UpgradePanel>();
        UpgradeCardManager = FindObjectOfType<UpgradeCardManager>();
    }


    public void Upgrade()
    {
        // Вызывается при нажатии кнопки апгрейда
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
                UpgradeCardManager.RemoveGunModeCards();
                break;
            case UpgradeType.SniperMode:
                GameManager.UpdradeGunMode("sniper");
                UpgradeCardManager.RemoveGunModeCards();
                break;
        }

        GameManager.upgradePoints--;

        if (upgradePanel!= null)
        {
            upgradePanel.HideUpgradePanel();
        }
    }
}

// Перечисление для разных типов апгрейдов
public enum UpgradeType
{
    Damage,
    HP,
    Speed,
    FireRate,
    ShotgunMode,
    SniperMode
    // Добавьте дополнительные типы апгрейдов, если необходимо
}
