using System;
using System.Collections;
using Items;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    public Action OnTimeOut;

    [SerializeField] private int _timerSeconds = 60;

    [SerializeField] private int _score = 0;

    private IEnumerator _timerCoroutine;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        StartGame();
    }



    private void StartGame()
    {
        _score = 0;

        _timerSeconds = 60;

        Time.timeScale = 1;

        _timerCoroutine = StartTimer();

        StartCoroutine(_timerCoroutine);

        OnTimeOut = TimeOut;

        HudManager.instance.SetTimerSeconds(_timerSeconds);
    }

    private void TimeOut()
    {
        StopTimer();

        EndGame();
    }


    private IEnumerator StartTimer()
    {
        while (0 < _timerSeconds)
        {
            yield return new WaitForSecondsRealtime(1);

            _timerSeconds--;

            HudManager.instance.SetTimerSeconds(_timerSeconds);
        }

        OnTimeOut?.Invoke();
    }

    public void StopTimer()
    {
        if (_timerCoroutine == null) return;

        StopCoroutine(_timerCoroutine);

        _timerCoroutine = null;

        OnTimeOut = null;
    }


    private void EndGame()
    {
        HudManager.instance.ShowGameOverScreen();

        Time.timeScale = 0;

        AudioManager.instance.Stop();

        AudioManager.instance.PlayGameOver();
    }

    public void CloseGame()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        StartGame();
    }

    public void AddScore(DeliveryItemData item)
    {
        _score += item.health;

        _timerSeconds += item.deliveryTime;

        HudManager.instance.SetScore(_score + " $");

    }

}
