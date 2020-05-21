using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameLogic : MonoBehaviour
{

    private int _KilledMobsCount = 0;
    private int _totalGold = 0;

    public int KilledMobsCount { get => _KilledMobsCount; set => _KilledMobsCount = value; }
    public int TotalGold { get => _totalGold; set => _totalGold = value; }

    private void Start()
    {
        Time.timeScale = 1;
        _totalGold += GameDataProvider.Instance.Settings.StartGold;
        GameDataProvider.Instance.EventSystem.UpdateUI();
        GameDataProvider.Instance.EventSystem.OnPlayerDead += PlayerDead;
        GameDataProvider.Instance.EventSystem.OnMobDead += AddScore;
        foreach (var t in GameDataProvider.Instance.TowerPlatforms)
        {
            var turret = Instantiate(GameDataProvider.Instance.TowerPrefab, t.position, Quaternion.identity);
            turret.transform.SetParent(t);
            GameDataProvider.Instance.Ui.CreateTowerButton(turret.GetComponent<Tower>());
        }
    }

    private void PlayerDead()
    {
        Time.timeScale = 0;
    }

    private Mob AddScore(Mob mob)
    {
        _KilledMobsCount++;
        _totalGold += mob.GoldReward;
        return null;
    }


    private void OnDisable()
    {
        GameDataProvider.Instance.EventSystem.OnPlayerDead -= PlayerDead;
    }

}
