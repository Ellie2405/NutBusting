using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    private const string GearsTextFormat = "Gears:{0}";
    private const string HPTextFormat = "Lives:{0}";

    [SerializeField] GameObject DefeatUI;
    [SerializeField] GameObject MainMenu;

    [Header("In Game")]
    [SerializeField] GameObject InGameUI;
    [SerializeField] GameObject PauseMenu;
    [SerializeField] GameObject VolumeOn;
    [SerializeField] GameObject VolumeOff;

    GameObject HealthBar;
    [SerializeField] GameObject FullHealthBar;
    [SerializeField] GameObject HighHealthBar;
    [SerializeField] GameObject HalfHealthBar;
    [SerializeField] GameObject LowHealthBar;
    [SerializeField] GameObject EmptyHealthBar;

    GameObject TurretBar;
    [SerializeField] GameObject FullTurretBar;
    [SerializeField] GameObject HighTurretBar;
    [SerializeField] GameObject HalfTurretBar;
    [SerializeField] GameObject LowTurretBar;
    [SerializeField] GameObject EmptyTurretBar;
    [SerializeField] Image TurretImage;

    [SerializeField] Image WaveImage;
    GameObject currentWaveNumber;
    [SerializeField] GameObject[] WaveNumbers;
    float wavePortion;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }
    private void Start()
    {
        HealthBar = FullHealthBar;
        TurretBar = FullTurretBar;
        currentWaveNumber = WaveNumbers[0];
        SceneManager.sceneLoaded += SceneLoaded;
        Inventory.OnCurrencyChanged += UpdateCurrency;
        MainHero.OnHeroDamaged += UpdateHeroHP;
        EnemyManager.OnWaveStart += UpdateWaveBarPortion;
        EnemyBasic.OnEnemyDeath += WaveFiller;
    }

    void SceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.buildIndex)
        {
            case 0:
                HideGameUI();
                ShowMainMenu();
                //Inventory.OnCurrencyChanged -= UpdateCurrency;
                //MainHero.OnHeroDamaged -= UpdateHeroHP;
                //EnemyManager.OnWaveStart -= UpdateWaveBarPortion;
                //EnemyBasic.OnEnemyDeath -= WaveFiller;
                break;
            case 1:
                GameManager.Instance.PauseGame(false);
                ShowGameUI();
                HideMainMenu();
                break;
            default:
                break;
        }
    }

    public void ChangeScene(int scene)
    {
        GameManager.Instance.SwitchScene(scene);

    }

    public void ToggleSound(bool toggle)
    {
        SoundManager.Instance.ToggleSound(toggle);
    }

    #region ingameUI

    void ShowGameUI()
    {
        InGameUI.SetActive(true);
        DefeatUI.SetActive(false);
        PauseMenu.SetActive(false);
    }

    void HideGameUI()
    {
        InGameUI.SetActive(false);
        DefeatUI.SetActive(false);
        PauseMenu.SetActive(false);
    }

    void UpdateHeroHP(int HP)
    {
        HealthBar.SetActive(false);
        switch (HP)
        {
            case < 1:
                EmptyHealthBar.SetActive(true);
                HealthBar = EmptyHealthBar;
                break;
            case 1:
                LowHealthBar.SetActive(true);
                HealthBar = LowHealthBar;
                break;
            case 2:
                HalfHealthBar.SetActive(true);
                HealthBar = HalfHealthBar;
                break;
            case 3:
                HighHealthBar.SetActive(true);
                HealthBar = HighHealthBar;
                break;
            case 4:
                FullHealthBar.SetActive(true);
                HealthBar = FullHealthBar;
                break;
            default:
                break;
        }
    }

    void UpdateCurrency(int gears)
    {
        TurretImage.fillAmount = TurretFiller(gears);
        TurretBar.SetActive(false);
        switch (gears)
        {
            case < 25:
                EmptyTurretBar.SetActive(true);
                TurretBar = EmptyTurretBar;
                break;
            case < 50:
                LowTurretBar.SetActive(true);
                TurretBar = LowTurretBar;
                break;
            case < 75:
                HalfTurretBar.SetActive(true);
                TurretBar = HalfTurretBar;
                break;
            case < 100:
                HighTurretBar.SetActive(true);
                TurretBar = HighTurretBar;
                break;
            case 100:
                FullTurretBar.SetActive(true);
                TurretBar = FullTurretBar;
                break;
            default:
                break;
        }
    }

    float TurretFiller(int gears)
    {
        return (float)gears % 25 / 25;
    }

    public void UpdateWaveNumber(int num)
    {
        currentWaveNumber.SetActive(false);
        WaveNumbers[num].SetActive(true);
        currentWaveNumber = WaveNumbers[num];
    }

    void WaveFiller()
    {
        WaveImage.fillAmount += wavePortion;
    }

    void UpdateWaveBarPortion(float waveSize)
    {
        WaveImage.fillAmount = 0;
        wavePortion = 1f / waveSize;
        Debug.Log(wavePortion);
    }

    #endregion

    void ShowMainMenu()
    {
        MainMenu.SetActive(true);
    }

    void HideMainMenu()
    {
        MainMenu.SetActive(false);
    }

    public void ShowDefeatScreen()
    {
        DefeatUI.SetActive(true);
    }
}
