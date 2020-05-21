using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Settings/GameSettings")]
public class GameSettings : ScriptableObject
{
    [Header("Delay between waves")]
    [SerializeField]private float _waveDelay;
    [Header("Delay between bots spawn")]
    [SerializeField] private float _spawnDelay;
    [Header("Maximum waves in level")]
    [SerializeField] private int _waveCount;
    [Header("Maximum bots in wave modificator (exclude speed)")]
    [SerializeField] private int _botsInWaveModificator;
    [Header("Add random bot stats in new wave")]
    [SerializeField] private float _botStatsModificator;
    [Header("Starting boot speed (bot maximus speed 5f)")]
    [SerializeField] private float _startBotSpeed;
    [Header("Count of start Gold")]
    [SerializeField] private int _startPlayerGold;
    [Header("Count of start HP")]
    [SerializeField] private int _startPlayerHP;

    public float SpawnDelay => _spawnDelay;
    public int WaveCount => _waveCount;
    public float WaveDelay => _waveDelay;
    public int BotsInWaveModificator => _botsInWaveModificator;
    public float BotStatsModificator => _botStatsModificator;
    public float StartBotSpeed => _startBotSpeed;
    public int StartGold => _startPlayerGold;
    public float PlayerHP => _startPlayerHP;

    private void OnValidate()
    {
        if (_waveDelay < 0)
            _waveDelay = 0;
        if (_waveCount < 0)
            _waveCount = 0;
        if (_spawnDelay < 0)
            _spawnDelay = 0;
        if (_botsInWaveModificator < 0)
            _botsInWaveModificator = 0;
        if (_startBotSpeed < 0.5f)
            _startBotSpeed = 0.5f;
        else if(_startBotSpeed > 5f)
            _startBotSpeed = 5f;
        if (_startPlayerGold < 0)
            _startPlayerGold = 0;

    }
}
