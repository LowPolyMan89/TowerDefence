using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ui : MonoBehaviour
{
    [SerializeField] private GameObject _upgradeButtonPrefab;
    [SerializeField] private Text _playerGoldText;
    [SerializeField] private Text _waveNumber;
    [SerializeField] private Image _hpBar;
    [SerializeField] private Text _scoreText;
    [SerializeField] private GameObject _scorePanel;

    private void Start()
    {
        GameDataProvider.Instance.EventSystem.OnUpdateUI += UpdateUI;
        GameDataProvider.Instance.EventSystem.OnPlayerDead += ShowScore;
        
    }

    public void CreateTowerButton(Tower tower)
    {
        UiUpgradeButton uiUpgradeButton = Instantiate(_upgradeButtonPrefab, transform).GetComponent<UiUpgradeButton>();
        uiUpgradeButton.Constructor(tower);
        uiUpgradeButton.transform.position = Camera.main.WorldToScreenPoint(tower.transform.position);
        

    }



    public void ShowScore()
    {
        _scorePanel.SetActive(true);
        _scoreText.text = string.Format("Total mob kll: {0} \nTotal Gold:  {1}", GameDataProvider.Instance.GameLogic.KilledMobsCount, GameDataProvider.Instance.GameLogic.TotalGold);
    }

    private void UpdateUI()
    {
        _waveNumber.text = GameDataProvider.Instance.WaveSpawner.WaveNumber.ToString();
        _playerGoldText.text = string.Format("Gold: {0}", GameDataProvider.Instance.Player.Gold);
        _hpBar.fillAmount = GameDataProvider.Instance.Player.PlayerHP / GameDataProvider.Instance.Settings.PlayerHP;
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
