using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private Transform _spawnPosition;
    [SerializeField] private float _spawnDelay;
    private int _waveNumber = 1;
    [SerializeField] private float _waveDelay;

    GameDataProvider _dataProvider;
    
    public float WaveDelay => _waveDelay;

    public int WaveNumber => _waveNumber;

    private float _botStatsMoficatorStorage;

    public BotParam botParam = BotParam.Health;


    private void Start()
    {
        _waveDelay = GameDataProvider.Instance.Settings.WaveDelay;
        _spawnDelay = GameDataProvider.Instance.Settings.SpawnDelay;

        _botStatsMoficatorStorage = GameDataProvider.Instance.Settings.BotStatsModificator;

        StartCoroutine(MobCreator(GetBotInWaveCount()));
        StartCoroutine(WaveCreator());

        GameDataProvider.Instance.EventSystem.OnPlayerDead += StopSpawn;
    }

    private int GetBotInWaveCount()
    {
        return Random.Range(_waveNumber, _waveNumber + GameDataProvider.Instance.Settings.BotsInWaveModificator + 1);
    }

    private void StopSpawn()
    {
        StopCoroutine(WaveCreator());
        StopCoroutine(MobCreator(0));
    }

    private IEnumerator WaveCreator()
    {
        

        for (int i = 1; i < GameDataProvider.Instance.Settings.WaveCount; i++)
        {
            //new bot upgrade type to all wave
            var rnd = new System.Random();
            botParam = (BotParam)rnd.Next(System.Enum.GetNames(typeof(BotParam)).Length);
            yield return new WaitForSeconds(GameDataProvider.Instance.Settings.WaveDelay);
            StartCoroutine(MobCreator(GetBotInWaveCount()));
            _waveNumber++;
            GameDataProvider.Instance.EventSystem.UpdateUI();
            Debug.Log("Create New Wave: " + _waveNumber);
        }

    }

    private IEnumerator MobCreator(int count)
    {

        for (int i = 0; i < count; i++)
        {
            yield return new WaitForSeconds(_spawnDelay);

            GameDataProvider.Instance.EventSystem.MobCreateEvent(Instantiate(GameDataProvider.Instance.Mob, _spawnPosition.position, Quaternion.identity));
        }
      
    }

    private void OnDisable()
    {
        GameDataProvider.Instance.EventSystem.OnPlayerDead -= StopSpawn;
    }
}
