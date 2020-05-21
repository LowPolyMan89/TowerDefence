using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataProvider : MonoBehaviour
{
    [SerializeField] private List<Target> _targets = new List<Target>();


    [SerializeField] private Base _base;
    [SerializeField] private Mob _mob;
    [SerializeField] private List<Transform> _towerPlatforms = new List<Transform>();
    [SerializeField] private GameObject _towerPrefab;
    [SerializeField] private TowerUpgrades _turretUpgrades;
    GameEventSystem _eventSystem;
    [SerializeField] private GameSettings _settings;
    [SerializeField] private WaveSpawner _waveSpawner;
    [SerializeField] private TowerTargetSelector targetSelector;
    [SerializeField] private Ui _ui;
    [SerializeField] private Player _player;
    [SerializeField] private GameLogic _gameLogic;

    public static GameDataProvider Instance;

    private void Awake()
    {
        Instance = this;
        _eventSystem = new GameEventSystem();
    }

    public List<Target> Targets => _targets;

    public Base Base { get => _base; set => _base = value; }
    public Mob Mob { get => _mob; set => _mob = value; }
    public List<Transform> TowerPlatforms { get => _towerPlatforms; set => _towerPlatforms = value; }
    public GameObject TowerPrefab { get => _towerPrefab; set => _towerPrefab = value; }
    public TowerUpgrades TurretUpgrades { get => _turretUpgrades; set => _turretUpgrades = value; }
    public GameEventSystem EventSystem { get => _eventSystem; set => _eventSystem = value; }
    public GameSettings Settings { get => _settings; set => _settings = value; }
    public WaveSpawner WaveSpawner { get => _waveSpawner; set => _waveSpawner = value; }
    public TowerTargetSelector TargetSelector { get => targetSelector; set => targetSelector = value; }
    public Ui Ui { get => _ui; set => _ui = value; }
    public Player Player { get => _player; set => _player = value; }
    public GameLogic GameLogic { get => _gameLogic; set => _gameLogic = value; }

    public void AddTargetToList(Target target)
    {
        _targets.Add(target);
    }
    public void RemoveTargetFromList(Target target)
    {
        List<Target> templist = new List<Target>();

        templist.AddRange(_targets);

        _targets.ForEach(x => {

            if (target.Mob == x.Mob)
            {
                templist.Remove(x);
                return;
            }
        });

        _targets.Clear();
        _targets.AddRange(templist);
    }

}

[System.Serializable]
public class Target
{
    public float Distance;
    public Mob Mob;

    public Target(float distance, Mob mob)
    {
        Distance = distance;
        Mob = mob;
    }
}
