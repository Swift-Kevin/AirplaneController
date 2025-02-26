using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private SettingsSO settingsObj;
    [SerializeField] private PlayerBase playerScript;
    [SerializeField] private CustomTimer gameTimer;

    public static float SafeZoneDistance = 300f;
    public SettingsSO SettingsObj => settingsObj;
    public PlayerBase PlayerScript => playerScript;
    public Vector3 PlayerPos => playerScript.transform.position;

    private float storedTimeScale;

    public void QuitApp()
    {
        Application.Quit();
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        storedTimeScale = Time.timeScale;

        gameTimer.OnEnd += GameOver;
        gameTimer.OnTick += GameTimerTick;
    }

    private void GameTimerTick()
    {
        UIManager.Instance.UpdateGameTimer((int)gameTimer.RemainingTime);
    }

    public void MouseUnlockShow()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void MouseLockHide()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ReturnToMainMenu()
    {
        UIManager.Instance.DisplayMainMenu();
        AudioManager.Instance.PlayMainMenuMusic();

        var go = FindObjectsByType(typeof(GameObject), FindObjectsInactive.Include, FindObjectsSortMode.None);

        foreach (var obj in go)
        {
            if (obj.GameObject().CompareTag("Enemy"))
            {
                Destroy(obj);
            }
        }

        SwarmManager.Instance.swarms = new List<GameObject>();
    }

    public void GameOver()
    {
        MouseUnlockShow();
        UIManager.Instance.GameLostMenu();
    }

    public void GameWon()
    {
        MouseUnlockShow();
        UIManager.Instance.GameWonMenu();
    }

    public void PlayGame()
    {
        UIManager.Instance.DisplayPlayerUI();
        InputManager.Instance.Actions.Enable();
        SwarmManager.Instance.StartSwarm();
        AudioManager.Instance.PlayBattleMusic();
        playerScript.ResetStats();

        gameTimer.StartTimer();
    }

    public void UpdateMouseSens(float sens)
    {
        settingsObj.mouseSensitivity = sens;
    }
}
