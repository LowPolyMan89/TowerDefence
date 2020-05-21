using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class Mob : MonoBehaviour
{
   [SerializeField] private int _mobLevel;
   [SerializeField] private float _health;
   [SerializeField] private int _goldReward;
   [SerializeField] private float _moveSpeed;
   [SerializeField] private float _damage;

    private MobNavigation _mobNavigation;
    private bool isEvolute = false;
    private bool isDead = false;
    public int GoldReward { get => _goldReward; set => _goldReward = value; }

    private void Awake()
    {
        _mobNavigation = new MobNavigation(gameObject.GetComponent<NavMeshAgent>(), GameDataProvider.Instance.Base);
    }
    private void Start()
    {

        _health = 100;
        _mobLevel = GameDataProvider.Instance.WaveSpawner.WaveNumber;
        _moveSpeed = GameDataProvider.Instance.Settings.StartBotSpeed;

        _mobNavigation.SetNavigationTarget();
        _mobNavigation.SetSpeed(_moveSpeed);
        RandomBotStatsModifier(GameDataProvider.Instance.Settings.BotStatsModificator, GameDataProvider.Instance.WaveSpawner.botParam, GameDataProvider.Instance.WaveSpawner.WaveNumber);
        GameDataProvider.Instance.AddTargetToList(new Target(0, this));
    }

    public void Attack()
    {
        GameDataProvider.Instance.Player.Damage(_damage);
        Dead();
    }

    public void SetDamage(float value, Tower tower)
    {
        _health = _health - value;

        if (_health <= 0)
        {
            Dead();
   
        }

    }

    public Mob RandomBotStatsModifier(float modifierValue, BotParam botParam, int waveCount)
    {
        if (isEvolute) return null;
        _goldReward = _mobLevel * 25;
        _health = _health * _mobLevel / 2;
        _damage = 10 * _mobLevel;

        switch (botParam)
        {
            case BotParam.Health:
                _health = _health + (modifierValue);
                gameObject.GetComponent<Renderer>().material.color = Color.black;
                break;
            case BotParam.Damage:
                _damage = _damage + (modifierValue);
                gameObject.GetComponent<Renderer>().material.color = Color.red;
                break;
            case BotParam.Speed:
                _mobNavigation.SetSpeed(5f);
                gameObject.GetComponent<Renderer>().material.color = Color.yellow;
                break;
        }

        isEvolute = true;

        return this;
    }


    private void Dead()
    {
        isDead = true;
        GameDataProvider.Instance.EventSystem.MobDead(this);
        GameDataProvider.Instance.RemoveTargetFromList(new Target(0, this));
        Destroy(this.gameObject, 0.2f);
    }

    private void Update()
    {
        if (isDead) return;

        if (_mobNavigation.GetDistance() < 1.5f)
        {
            Attack();
        }
    }

    private class MobNavigation
    {
        private NavMeshAgent _agent;
        private Base _navigationTarget;

        public void SetSpeed(float value)
        {
            _agent.speed = value;
        }
        public float GetDistance()
        {
            return Vector3.Distance(_agent.transform.position, _navigationTarget.transform.position);
        }
        public MobNavigation(NavMeshAgent navMeshAgent, Base target)
        {
            _agent = navMeshAgent;
            _navigationTarget = target;
        }

        public void SetNavigationTarget()
        {
            _agent.destination = _navigationTarget.transform.position;
        }
        
    }
}

public enum BotParam
{
    Health,
    Damage,
    Speed
}

