using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public bool GameIsPlaying { get; private set; } = true;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    private void Start()
    {
        MainHero.OnCastleDestroyed += Lose;

    }

    void Lose()
    {
        GameIsPlaying = false;
        StartCoroutine(LoseCo());
    }

    public void SwitchScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void PauseGame(bool pauseState)
    {
        // GameIsPlaying = !pauseState;
        if (pauseState)
        {
            Time.timeScale = 0;

        }
        else
        {
            Time.timeScale = 1;
        }
    }

    IEnumerator LoseCo()
    {
        yield return new WaitForSeconds(3);
        UIManager.Instance.ShowDefeatScreen();
        PauseGame(true);
    }
}
