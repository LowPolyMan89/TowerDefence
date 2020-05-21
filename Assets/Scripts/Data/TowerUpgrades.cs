using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Settings/TowerUpgrades")]
public class TowerUpgrades : ScriptableObject
{
    public List<TowerUpgradeData> TurretUpgradeDatas = new List<TowerUpgradeData>();
}

[System.Serializable]
public class TowerUpgradeData
{
    public int UpgradeLevel;
    public int UpgradeCost;
    public float DamageValue;
    public float ReloadSpeedValue;
    public float TowerRadius;
    public string TowerName;
}
