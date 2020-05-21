using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    [SerializeField] private int _gold;
    [SerializeField] private float _playerHP;

    public int Gold => _gold;

    public float PlayerHP { get => _playerHP; set => _playerHP = value; }

    private void Start()
    {
        _gold = GameDataProvider.Instance.Settings.StartGold;
        _playerHP = GameDataProvider.Instance.Settings.PlayerHP;
        GameDataProvider.Instance.EventSystem.OnMobDead += AddGold;
    }

    private Mob AddGold(Mob mob)
    {
        _gold += mob.GoldReward;

        GameDataProvider.Instance.EventSystem.UpdateUI();

        return null;
    }

    public void RemoveGold(int value)
    {
        if (value > _gold) return;

        _gold -= value;
        if (_gold < 0)
            _gold = 0;
    }

    //test method
    [ContextMenu("Kill")]
    public void KillPlayer()
    {
        Damage(111111);
    }

    public void Damage(float value)
    {
        _playerHP -= value;
        if(_playerHP <= 0)
        {
            _playerHP = 0;
            GameDataProvider.Instance.EventSystem.PlayerDead();
        }
    }
}
