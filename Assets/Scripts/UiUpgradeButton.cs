using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiUpgradeButton : MonoBehaviour
{
    private Tower _tower;
    [SerializeField]private Text _costtext;
    [SerializeField]private Button _button;
    [SerializeField] private Player _player;
    
    private void Start()
    {
        _player = GameDataProvider.Instance.Player;
        GameDataProvider.Instance.EventSystem.OnPlayerDead += Hide;
    }

    public void ResetPosition()
    {
        transform.position = Camera.main.WorldToScreenPoint(_tower.transform.position);
    }

    public void Constructor(Tower tower)
    {
        _tower = tower;
        _button.onClick.AddListener(_tower.UpgradeTower);
        _button.onClick.AddListener(Click);
        _costtext.text = GameDataProvider.Instance.TurretUpgrades.TurretUpgradeDatas[_tower.TowerLevel].UpgradeCost.ToString();
    }

    private void Click()
    {
        if (GameDataProvider.Instance.TurretUpgrades.TurretUpgradeDatas.Count > _tower.TowerLevel)
        {
            _costtext.text = GameDataProvider.Instance.TurretUpgrades.TurretUpgradeDatas[_tower.TowerLevel].UpgradeCost.ToString();
           
        }

        else
        {
            _costtext.text = "9999999";
            _button.interactable = false;
        }
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        int c = Int32.Parse(_costtext.text);

        if (_player.Gold < c)
        {
            _button.interactable = false;
        }
        else
        {
            _button.interactable = true;
        }

        ResetPosition();
    }

    private void OnDisable()
    {
        GameDataProvider.Instance.EventSystem.OnPlayerDead -= Hide;
    }
}
