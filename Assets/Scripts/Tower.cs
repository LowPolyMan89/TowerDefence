using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Tower : MonoBehaviour
{

    [SerializeField] private int _towerLevel;
    private LineRenderer _lineRenderer;
    [SerializeField] private bool _customLevel; //level set from inspector
    public int TowerLevel { get => _towerLevel; set => _towerLevel = value; }

    private void Start()
    {
        SetTowerStats(_customLevel ? _towerLevel : 1);
        StartCoroutine(ReloadUpdate());
        gameObject.name = "Tower: " + UnityEngine.Random.Range(1, 100).ToString("00");
        _lineRenderer = gameObject.AddComponent<LineRenderer>();
        _lineRenderer.widthMultiplier = 0.2f;
    }

    [ContextMenu("Upgrade")]
    public void UpgradeTower()
    {
        if (GameDataProvider.Instance.TurretUpgrades.TurretUpgradeDatas.Count <= _towerLevel) return;
        _towerLevel++;
        SetTowerStats(_towerLevel);
        GameDataProvider.Instance.Player.RemoveGold(GameDataProvider.Instance.TurretUpgrades.TurretUpgradeDatas[_towerLevel-1].UpgradeCost);
        GameDataProvider.Instance.EventSystem.UpdateUI();
    }

    public void Attack(Target target)
    {
        if(target != null)
        {
            target.Mob.SetDamage(GameDataProvider.Instance.TurretUpgrades.TurretUpgradeDatas[_towerLevel-1].DamageValue, this);
            StartCoroutine(DrawLaser(target.Mob.transform.position));
        }
           
    }

    private IEnumerator DrawLaser(Vector3 point)
    {
        _lineRenderer.enabled = true;
        _lineRenderer.SetPosition(0, transform.position);
        _lineRenderer.SetPosition(1, point);
        yield return new WaitForSeconds(0.1f);
        _lineRenderer.enabled = false;
    }

    private IEnumerator ReloadUpdate()
    {
        while (true)
        {
            yield return new WaitForSeconds(GameDataProvider.Instance.TurretUpgrades.TurretUpgradeDatas[_towerLevel - 1].ReloadSpeedValue);
            
            Attack(GameDataProvider.Instance.TargetSelector.GetClosesTarget(GameDataProvider.Instance.TurretUpgrades.TurretUpgradeDatas[_towerLevel - 1].TowerRadius, GameDataProvider.Instance.Targets, transform));
        }
    }

    private void SetTowerStats(int towerLevel)
    {
        if (GameDataProvider.Instance.TurretUpgrades.TurretUpgradeDatas.Count < towerLevel) return;

        var towerUpgradeData = GameDataProvider.Instance.TurretUpgrades.TurretUpgradeDatas[towerLevel - 1];
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y + towerLevel, transform.localScale.z);
        _towerLevel = towerUpgradeData.UpgradeLevel;
    }

}



